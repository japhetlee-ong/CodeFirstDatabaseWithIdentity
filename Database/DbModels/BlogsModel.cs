using CodeFirstDatabase.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstDatabase.Database.DbModels
{
    public class BlogsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string BlogName { get; set; }
        [Required]
        public string BlogSlug { get; set; }
        [Required]
        public string BlogImage { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        [Required]
        public bool IsPublished { get; set; }

        [Required]
        public string AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
