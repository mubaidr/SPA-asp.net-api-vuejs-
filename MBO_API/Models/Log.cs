namespace MBO_API.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Log")]
    public partial class Log
    {
        public Log()
        {
            LogTime = DateTime.Now;
        }

        [Key]
        public int LogID { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(25)]
        public string Title { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [ScaffoldColumn(false)]
        public DateTime LogTime { get; set; }
        
        [Required]
        [Display(Name = "Task")]
        public int MainTaskID { get; set; }

        [Required]
        [Display(Name = "User")]
        [ScaffoldColumn(false)]
        public string ApplicationUserID { get; set; }
        
        public virtual MainTask MainTask { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
