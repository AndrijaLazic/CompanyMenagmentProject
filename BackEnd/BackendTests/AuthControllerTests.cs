using BackendAPI.Controllers;
using BLL.Services;
using DOMAIN.Abstractions;
using DOMAIN.Models.DTO;
using DOMAIN.Shared;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTests
{
    public class AuthControllerTests
    {
        private readonly AuthController _authController;
        private readonly Mock<IAuthService> _authService;
        private readonly Mock<ILogger<AuthController>> _logger;
        private readonly Mock<IOptions<AppConfigClass>> _appConfig;


        public AuthControllerTests()
        {
            _authService= new Mock<IAuthService>();
            _logger = new Mock<ILogger<AuthController>>();
            _appConfig = new Mock<IOptions<AppConfigClass>>();

            _authController = new AuthController(_appConfig.Object, _authService.Object, _logger.Object);
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
            LoginDTO loginDTO = new LoginDTO() { 
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



    }
}
