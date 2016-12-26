using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MBO_API.Models
{
    public class MBO_API_Context : DbContext
    {
        public MBO_API_Context()
            : base("MBO")
        {
        }
        
        public static MBO_API_Context Create()
        {
            return new MBO_API_Context();
        }
        
        public virtual DbSet<MainTask> MainTask { get; set; }        
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<Status> Status { get; set; }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Entity<IdentityUserClaim>().ToTable("IdentityUserClaims").HasKey(u => u.Id);
            modelBuilder.Entity<IdentityUserLogin>().ToTable("IdentityUserLogins").HasKey(l => new { l.UserId, l.ProviderKey });
            modelBuilder.Entity<IdentityRole>().ToTable("IdentityRoles").HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().ToTable("IdentityUserRoles").HasKey(r => new { r.RoleId, r.UserId });
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers").HasKey(u => u.Id);            
        }
        
    }
}