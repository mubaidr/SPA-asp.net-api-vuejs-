namespace MBO_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Log")]
    public partial class Log
    {
        public Log()
        {
            //Title = (Title == null || Title.Trim() == "") ? "" : Title;
            LogTime = DateTime.Now;
        }

        [Key]
        public int Log_Id { get; set; }

        //[Required]
        //[MinLength(5)]
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
        [ForeignKey("MainTask")]
        [Display(Name = "Task")]
        public int MainTask_Id { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        [Display(Name = "User")]
        [ScaffoldColumn(false)]
        public string ApplicationUser_Id { get; set; }
        
        public virtual MainTask MainTask { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
