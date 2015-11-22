using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace EventiWebAPI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(100)]
        public string Cognome { get; set; }

    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new DBInitializer());

        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<EventiWebAPI.Models.Location> Locations { get; set; }

        public System.Data.Entity.DbSet<EventiWebAPI.Models.Evento> Eventi { get; set; }

        public System.Data.Entity.DbSet<EventiWebAPI.Models.Registrazione> Registrazioni { get; set; }
    }

    public class DBInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {

            var ir = new IdentityRole { Name = "admin" };
            context.Roles.Add(ir);
            context.SaveChanges();

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var userToInsert = new ApplicationUser { UserName = "admin", Nome = "Admin", Cognome = "Eventi", Email = "admin@eventi.me", EmailConfirmed = true };
            userManager.Create(userToInsert, "Password@123");
            userManager.AddToRole(userToInsert.Id, "admin");
            base.Seed(context);

        }
    }


}