namespace MBO_API.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MainTask")]
    public partial class MainTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MainTask()
        {
            IsDeleted = false;
            Progress = 0;
            DateAssigned = DateAssigned == null ? DateTime.Now : DateAssigned;
            AssignedTo = new HashSet<ApplicationUser>();
            Log = new HashSet<Log>();
        }

        [Key]
        public int MainTaskID { get; set; }
        
        public string Title { get; set; }
        
        [MaxLength(250)]
        [Required]
        public string Description { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public int Progress { get; set; }
        
        public DateTime DateAssigned { get; set; }
        
        public DateTime DateDue { get; set; }
        
        public int StatusID { get; set; }
        
        public int CategoryID { get; set; }
        
        public string AssignedByID { get; set; }

        public virtual Status Status { get; set; }

        public virtual Category Category { get; set; }
        
        public virtual ApplicationUser AssignedBy { get; set; }
        
        [InverseProperty("Assigned")]
        [Display(Name = "Assigned To")]
        public ICollection<ApplicationUser> AssignedTo { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgressHistory> ProgressHistory { get; set; }

        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Log { get; set; }
    }
}
