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

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Title { get; set; }
        
        [MaxLength(250)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; }

        [ScaffoldColumn(false)]
        public int Progress { get; set; }

        //[Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Assigned on")]
        [ScaffoldColumn(false)]
        public DateTime DateAssigned { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm A}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due Date")]
        public DateTime DateDue { get; set; }
        
        [Display(Name = "Status")]
        [ScaffoldColumn(false)]
        public int StatusID { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryID { get; set; }

        [Required]
        
        [Display(Name = "Assigned By")]
        [ScaffoldColumn(false)]
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
