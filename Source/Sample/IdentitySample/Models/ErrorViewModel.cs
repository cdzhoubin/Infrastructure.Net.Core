using System;
using System.ComponentModel.DataAnnotations;
using Zhoubin.Infrastructure.Common.Identity.MongoDb;

namespace IdentitySample.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
    public class ApplicationUser : MongodbIdentityUser<ApplicationRole>
    {

    }

    public class ApplicationRole : MongodbIdentityRole
    {

    }

    public class CreateUserModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }
    }
}