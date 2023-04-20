using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDI_Feather_Tracking_API.Models
{
    public class InventoryRecords
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(SkuType))]
        public int SkuTypeId { get; set; }

        [Required]
        public string BatchNo { get; set; }

        [Required]
        public decimal GrossWeight { get; set; }

        [Required]
        public decimal TareWeight { get; set; }

        [Required]
        public decimal NettWeight { get; set; }

        [Required]
        public DateTime IncomingDateTime { get; set; }

        public DateTime OutgoingDateTime { get; set; }

        public string? OutgoingContainer { get; set; }

        public int OutgoingPic { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public int UpdatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Comment("Cancel : 1, Not cancel : 0")]
        [DefaultValue(0)]
        public int CancelStatus { get; set; }

        public SkuType SkuType { get; set; }
    }
}
