using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace VetTracker2.Model
{
    [DataContract]
    public class Pet
    {
        [DataMember]
        [Key]
        public int Id { get; set; }

        [DataMember]
        [StringLength(40)]
        public string Type { get; set; }

        [DataMember]
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [DataMember]
        [StringLength(40)]
        public string Illness { get; set; }

        [DataMember]
        [StringLength(40)]
        public string Owner { get; set; }
    }
}
