using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDI_Feather_Tracking_API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{ get; set; }

        [Required]
        public string Username{ get; set; }

        [Required]
        public string Password{ get; set; }

        [Required]
        public string EmployeeNo{ get; set; }

        [Required]
        [ForeignKey(nameof(UserLevel))]
        public int UserLevelId{ get; set; }

        [Required]
        [DefaultValue(true)]
        public bool Status{ get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public int UpdatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }



        public UserLevel UserLevel { get; set; }
    }
}
