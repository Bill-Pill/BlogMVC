using BlogMVC.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogMVC.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public string BlogUserId { get; set; } = String.Empty;

        [StringLength(75, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Title { get; set; } = String.Empty;
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and no more than {1} characters long.", MinimumLength = 2)]
        public string Abstract { get; set; } = String.Empty;

        public string Content { get; set; } = String.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Created Date")]
        public DateTime Created { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Updated Date")]
        public DateTime? Updated { get; set; }

        public ReadyStatus ReadyStatus { get; set; }

        public string? Slug { get; set; }

        [Display(Name = "Blog Image")]
        public byte[] ImageData { get; set; } = Array.Empty<byte>();
        [Display(Name = "Image Type")]
        public string ContentType { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile? Image { get; set; }

        // Navigation Props
        public virtual Blog? Blog { get; set; }
        public virtual BlogUser? BlogUser { get; set; }
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
