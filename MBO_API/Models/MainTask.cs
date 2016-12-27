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
            //SubTask = new HashSet<SubTask>();            
            Status_Id = 0;
            DateAssigned = DateAssigned == null ? DateTime.Now : DateAssigned;
            DateDue = DateDue == null ? DateTime.Now.AddMonths(1) : DateDue;
            AssignedTo = new HashSet<ApplicationUser>();
            Log = new HashSet<Log>();
        }

        [Key]
        public int MainTask_Id { get; set; }

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
        [DataType(DataType.DateTime)]
        [Display(Name = "Assigned on")]
        [ScaffoldColumn(false)]
        public DateTime DateAssigned { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Due by")]
        public DateTime DateDue { get; set; }
        
        [ForeignKey("Status")]
        [Display(Name = "Status")]
        [ScaffoldColumn(false)]
        public int Status_Id { get; set; }

        [Required]
        [ForeignKey("Category")]
        [Display(Name = "Category")]
        public int Category_Id { get; set; }

        [Required]
        [ForeignKey("AssignedBy")]
        [Display(Name = "Assigned By")]
        [ScaffoldColumn(false)]
        public string ApplicationUser_Id { get; set; }

        public virtual Status Status { get; set; }

        public virtual Category Category { get; set; }
        
        public virtual ApplicationUser AssignedBy { get; set; }

        [InverseProperty("Assigned")]
        [Display(Name = "Assigned To")]
        public ICollection<ApplicationUser> AssignedTo { get; set; }
        
        [JsonIgnore]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Log> Log { get; set; }
    }
}
