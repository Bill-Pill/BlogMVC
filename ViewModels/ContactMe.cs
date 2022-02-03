using System.ComponentModel.DataAnnotations;

namespace BlogMVC.ViewModels
{
    public class ContactMe
    {
        [StringLength(80, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [EmailAddress]
        [StringLength(80, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Email { get; set; }

        [Phone]
        public string? Phone { get; set; }

        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Subject { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 2)]
        public string Message { get; set; }

    }
}
