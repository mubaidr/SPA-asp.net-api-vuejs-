using System.ComponentModel.DataAnnotations;

namespace MBO_API.Models
{
    public class PersonRole
    {
        public PersonRole()
        {
            //ApplicationUsers = new HashSet<ApplicationUser>();
        }

        [Key]
        public int PersonRoleID { get; set; }

        [Required]
        public string Description { get; set; }

        //public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}