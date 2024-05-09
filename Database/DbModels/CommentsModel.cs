using CodeFirstDatabase.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeFirstDatabase.Database.DbModels
{
    public class CommentsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        public int BlogId { get;set; }
        [ForeignKey("BlogId")]
        public BlogsModel BlogsModel { get; set; }
        [Required]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
