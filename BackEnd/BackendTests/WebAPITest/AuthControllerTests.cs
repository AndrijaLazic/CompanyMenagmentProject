using BackendTests.Abstractions;
using DOMAIN.Models.DTO;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BackendTests.WebAPITest
{
    public class AuthControllerTests: BaseFunctionalTest
    {
        public AuthControllerTests(TestWebAppFactory testWebAppFactory) : base(testWebAppFactory)
        {
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
        public async void LoginSuccess()
        {
            LoginDTO loginDTO = new LoginDTO()
            {
                Email = "andrija@gmail.com",
                Password = "string"
            };
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("Auth/Login", loginDTO);
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
