using System.ComponentModel.DataAnnotations;

namespace Jewellery_Models
{
    public class MaterialType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
