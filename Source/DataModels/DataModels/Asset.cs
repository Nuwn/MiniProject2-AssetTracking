using System.ComponentModel.DataAnnotations.Schema;

namespace DataModels
{
    public sealed class Asset
    {
        public int Id { get; set; }

        public DateTime PurchaseDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int DeviceId { get; set; }
        public Device Device { get; set; }

        public int OfficeId { get; set; }
        public Office Office { get; set; }
    }
}