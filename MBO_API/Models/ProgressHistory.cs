using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MBO_API.Models
{
    public class ProgressHistory
    {
        [Key]
        public int ProgressHistory_Id { get; set; }

        public int MainTask_Id { get; set; }

        public int Progress { get; set; }

        public DateTime UpdateDate { get; set; }

        [ForeignKey("MainTask_Id")]
        public virtual MainTask MainTask { get; set; }
    }
}