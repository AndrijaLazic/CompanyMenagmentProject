using BackendAPI.Controllers;
using BackendTests.Brokers;
using BLL.Services;
using DOMAIN.Abstractions;
using DOMAIN.Enums;
using DOMAIN.Models.Database;
using DOMAIN.Models.DTO;
using DOMAIN.Models.DTR;
using DOMAIN.Shared;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using Xunit.Sdk;



namespace BackendTests.E2ETests
{
    public class AuthControllerTests
    {
        private readonly HttpClient _httpClient;
        private readonly AppConfigClass _appConfig;
        private string _validJWT;
        private TestConfig _testConfig;
        private User adminUser = new User()
        {
            Email = "Admin1@gmail.com",
            PasswordHash = Encoding.UTF8.GetBytes("string"),
            PasswordSalt = Encoding.UTF8.GetBytes("string"),
            Id = 12,
            Lastname = "Admin1",
            Name = "Admin1",
            PhoneNumber = "1234567890",
            WorkerType = 0
        };

        public AuthControllerTests()
        {

            _httpClient = WebAppBroker.GetHttpClient();

            _appConfig = WebAppBroker.GetWebAppInstance().Services.GetService<IOptions<AppConfigClass>>().Value;
            _validJWT = JWToken.CreateToken(adminUser, _appConfig.JWTSettings);
            _testConfig = TestConfigProvider.GetTestConfig();

            

        }

        private bool ValidateDto(object dto, out List<ValidationResult> results)
        {
            var context = new ValidationContext(dto, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(dto, context, results, true);
        }

        [Fact]
        public async void LoginInvalidDTO()
        {
            LoginDTO loginDTO = new LoginDTO()
            {
                Email = "andrija@",
                Password = "stringstringstringstringstringstringstring"
            };
            var isValid = ValidateDto(loginDTO, out var results);

            // Assert
            isValid.Should().Be(false);
            results.Count.Should().Be(2);
            results[0].ErrorMessage.Should().Be("EmailNotValid");
            results[1].ErrorMessage.Should().Be("MaxPassword20");
        }

        [Fact]
        public async void RegistrationInvalidDTO()
        {
            RegistrationDTO registrationDTO = new RegistrationDTO()
            {
                Email = "andrija",
                Lastname = "andrija",
                Name = "Name",
                Password = "ne",
                PhoneNumber = "1",
                WorkerType = 0
            };
            var isValid = ValidateDto(registrationDTO, out var results);

            // Assert
            isValid.Should().Be(false);
            results.Count.Should().Be(2);
            results[0].ErrorMessage.Should().Be("EmailNotValid");
            results[1].ErrorMessage.Should().Be("MinPassword6");
        }
        [Fact]
        public async void RegistrationUnauthorized()
        {
            RegistrationDTO registrationDTO = new RegistrationDTO()
            {
                Email = adminUser.Email,
                Lastname = adminUser.Lastname,
                Name = adminUser.Name,
                Password = "string",
                PhoneNumber = adminUser.PhoneNumber,
                WorkerType = adminUser.WorkerType
            };

            var response = await _httpClient.PostAsJsonAsync("/Auth/Register", registrationDTO);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async void TC1_RegistrationSuccess()
        {
            //removing data from DB if exists
            try
            {
                var dbService = WebAppBroker.GetWebAppInstance().Services.GetService<IUserDataDB>();
                var pomUser = dbService.GetUser(adminUser.Email);
                if (pomUser != null)
                    dbService.RemoveUser(pomUser.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            RegistrationDTO registrationDTO = new RegistrationDTO()
            {
                Email = adminUser.Email,
                Lastname = adminUser.Lastname,
                Name = adminUser.Name,
                Password = "string",
                PhoneNumber = adminUser.PhoneNumber,
                WorkerType = adminUser.WorkerType
            };

            var request = new HttpRequestMessage(HttpMethod.Post, _testConfig.BaseApiURL + "/Auth/Register")
            {
                Content = new StringContent(
                    JsonConvert.SerializeObject(registrationDTO),
                    Encoding.UTF8,
                    "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _validJWT);

            // Make the request

            var response = await _httpClient.SendAsync(request);
            var responseData = response.Content.ReadFromJsonAsync<ServiceResponse<bool>>().Result;

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            
        }

        [Fact]
        public async Task TC2_LoginSuccess()
        {

            LoginDTO loginDTO = new LoginDTO()
            {
                Email = adminUser.Email,
                Password = "string"
            };

            var response = await _httpClient.PostAsJsonAsync("/Auth/Login", loginDTO);
            var responseData = response.Content.ReadFromJsonAsync<ServiceResponse<string>>().Result;


            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            responseData.Data.Should().NotBeEmpty();
        }

        [Fact]
        public async void TC3_RemoveUser()
        {
            User user = WebAppBroker.GetWebAppInstance().Services.GetService<IUserDataDB>().GetUser(adminUser.Email);


            var request = new HttpRequestMessage(HttpMethod.Delete, _testConfig.BaseApiURL + "/Auth/RemoveUser/"+ user.Id.ToString())
            {
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _validJWT);

            // Make the request

            var response = await _httpClient.SendAsync(request);
            var responseData = response.Content.ReadFromJsonAsync<ServiceResponse<bool>>().Result;

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        }
    }
}
