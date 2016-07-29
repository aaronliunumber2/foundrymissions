using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using FoundryMissionsCom.Models.FoundryMissionModels;

namespace FoundryMissionsCom.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        #region Custom Properties

        [Display(Name ="Cryptic Tag")]
        [Required]
        [RegularExpression("^[a-zA-Z0-9_.#-]*$", ErrorMessage = "Cryptic Tag can only have alphanumeric characters")]
        public string CrypticTag { get; set; }

        [Display(Name = "Join Date")]
        [DataType(DataType.Date)]
        public DateTime JoinDate { get; set; }

        [Display(Name = "Twitter Username")]
        [RegularExpression("^[a-zA-Z0-9_]*$", ErrorMessage = "Twitter usernames can only have alphanumeric characters")]
        [MaxLength(15, ErrorMessage ="Twitter usernames cannot be longer than 15 characters.")]
        public string TwitterUsername { get; set; }

        [DisplayName("Auto Approval")]
        public bool AutoApproval { get; set; }

        #endregion

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public DbSet<Mission> Missions { get; set; }
        public DbSet<MissionTagType> MissionTagTypes { get; set; }
        public DbSet<YoutubeVideo> YoutubeVideos { get; set; }
        public DbSet<MissionImage> MissionImages { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Mission>()
        //        .HasRequired(c => c.Author)
        //        .WithOptional()
        //        .WillCascadeOnDelete(false);

        //    modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
        //    modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
        //    modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        //}
    }
}