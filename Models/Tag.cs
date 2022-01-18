using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogMVC.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string AuthorId { get; set; } = string.Empty;

        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Text { get; set; } = string.Empty;

        // Navigation props
        public virtual Post? Post { get; set; }
        public virtual BlogUser? Author { get; set; }
    }
}
