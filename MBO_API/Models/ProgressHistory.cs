using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace MBO_API.Models
{
    public class ProgressHistory
    {
        public ProgressHistory()
        {
            Progress = 0;
            UpdateDate = DateTime.Now;
        }

        [Key]
        public int ProgressHistoryID { get; set; }

        [Required]
        public int MainTaskID { get; set; }

        public int Progress { get; set; }

        public DateTime UpdateDate { get; set; }

        [JsonIgnore]
        public virtual MainTask MainTask { get; set; }
    }
}