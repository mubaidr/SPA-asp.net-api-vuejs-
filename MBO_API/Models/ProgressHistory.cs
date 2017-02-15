using Newtonsoft.Json;
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
        public int ProgressHistoryID { get; set; }

        public int MainTaskID { get; set; }

        public int Progress { get; set; }

        public DateTime UpdateDate { get; set; }

        [JsonIgnore]
        public virtual MainTask MainTask { get; set; }
    }
}