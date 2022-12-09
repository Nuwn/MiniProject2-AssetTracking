using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public sealed class Office
    {
        public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Name { get; set; }

        [StringLength(5)]
        public string Currency { get; set; }

        public IList<Asset> Assets { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}