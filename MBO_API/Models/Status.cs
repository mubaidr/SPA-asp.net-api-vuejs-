namespace MBO_API.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Status")]
    public partial class Status
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Status()
        {
            MainTask = new HashSet<MainTask>();
        }

        [Key]
        public int StatusID { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        [Range(0, 12)]
        public int Level { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MainTask> MainTask { get; set; }
    }
}
