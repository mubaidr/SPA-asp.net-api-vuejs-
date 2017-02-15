namespace MBO_API.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Log")]
    public partial class Log
    {
        public Log()
        {
            Title = "";
            Description = "Task Created!";
            LogTime = DateTime.Now;
        }

        [Key]
        public int LogID { get; set; }
                
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }
        
        public DateTime LogTime { get; set; }
        
        [Required]
        public int MainTaskID { get; set; }
        
        public string ApplicationUserID { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual MainTask MainTask { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
