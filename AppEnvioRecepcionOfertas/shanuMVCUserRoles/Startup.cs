using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using shanuMVCUserRoles.Models;
using Owin;
using System.Security.Claims;

[assembly: OwinStartupAttribute(typeof(shanuMVCUserRoles.Startup))]
namespace shanuMVCUserRoles
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
			createRolesandUsers();
		}


		// In this method we will create default User roles and Admin user for login
		private void createRolesandUsers()
		{
			ApplicationDbContext context = new ApplicationDbContext();

			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
			var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


			// In Startup iam creating first Admin Role and creating a default Admin User 
			if (!roleManager.RoleExists("Admin"))
			{

				// first we create Admin rool
				var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
				role.Name = "Admin";
				roleManager.Create(role);

				//Creacion del superadmin				

				var user = new ApplicationUser();
				user.UserName = "admin";
				user.Email = "admin@admin.com";

				string userPWD = "123456";

				var chkUser = UserManager.Create(user, userPWD);

				//Rol por defecto que es Admin
				if (chkUser.Succeeded)
				{
					var result1 = UserManager.AddToRole(user.Id, "Admin");

				}
			}

            // Creacion de nuevos roles 
            if (!roleManager.RoleExists("Cliente"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Cliente";
                roleManager.Create(role);

            }

        }
	}
}
