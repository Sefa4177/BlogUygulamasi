using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models
{
    public class PostEditViewModel
    {
        public int PostId { get; set; }

        [Required]
        [Display(Name = "Başlık")]
        public string? Title { get; set; }


        [Required]
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Required]
        [Display(Name = "İçerik")]
        public string? Content { get; set; }

        [Required]
        [Display(Name = "Url")]
        public string? Url { get; set; }
    
        [Display(Name = "Görsel")]
        public string? Image { get; set; }

        [Display(Name = "Post aktiflik durumu")]
        public bool IsActive { get; set; }

        public List<Tag> Tags { get; set; } = new();
    }
}