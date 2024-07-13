using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN.Models.DTO
{
    public record RegistrationDTO
    {
        [Required(ErrorMessage = "EmailRequired")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "EmailNotValid")]
        [StringLength(maximumLength: 20, ErrorMessage = "MaxEmail30")]
        public string Email { get; init; }

        [Required(ErrorMessage = "NameRequired")]
        [StringLength(maximumLength: 20, ErrorMessage = "MaxName20")]
        [MinLength(3, ErrorMessage = "MinName3")]
        public string Name { get; init; }

        [Required(ErrorMessage = "LastnameRequired")]
        [StringLength(maximumLength: 20, ErrorMessage = "MaxName20")]
        [MinLength(3, ErrorMessage = "MinName3")]
        public string Lastname { get; init; }

        [Required(ErrorMessage = "PasswordRequired")]
        [StringLength(maximumLength: 20, ErrorMessage = "MaxPassword20")]
        [MinLength(6, ErrorMessage = "MinPassword6")]
        public string Password { get; init; }

        [Required(ErrorMessage = "PhoneNumberRequired")]
        [StringLength(maximumLength: 20, ErrorMessage = "MaxPhoneNumber20")]
        public string PhoneNumber { get; init; }

        [Required(ErrorMessage = "WorkerTypeRequired")]
        public byte WorkerType { get; init; }
    }
}
