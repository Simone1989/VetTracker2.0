using System.ComponentModel.DataAnnotations;

namespace VetTracker2.Model
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [StringLength(40)]
        public string Type { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [StringLength(40)]
        public string Illness { get; set; }

        [StringLength(40)]
        public string Owner { get; set; }
    }
}
