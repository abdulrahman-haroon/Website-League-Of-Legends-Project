using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using Final.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(Final.Startup))]

namespace Final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
            CreateRoles2();
            CreateRoles3();//Assigning same role to a user by remove not sign but after assigned i put the 
                            //sign back
        }
        private void CreateRoles()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                if (!roleManager.RoleExists("SuperAdmin"))
                {
                    role.Name = "SuperAdmin";
                    roleManager.Create(role);
                    AddUsers(role.Name);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        private void AddUsers(string roleName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();

            user.UserName = "AbdulRahmanHaroon";
            user.Email = "bse173030@gmail.com";
            string password = "Bse173030##";

            var status = UserManager.Create(user, password);

            if (status.Succeeded)
            {
                UserManager.AddToRole(user.Id, roleName);
            }
        }
        private void CreateRoles2()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                if (!roleManager.RoleExists("Accountant"))
                {
                    role.Name = "Accountant";
             
                    AddUsers2(role.Name);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        private void AddUsers2(string roleName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();

            user.UserName = "AbdulMoeed";
            user.Email = "bse173015@gmail.com";
            string password = "Bse173015##_";

            var status = UserManager.Create(user, password);

            if (status.Succeeded)
            {
                UserManager.AddToRole(user.Id, roleName);
            }
        }
        private void CreateRoles3()
        {
            try
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                if (!roleManager.RoleExists("Accountant"))
                {
                    role.Name = "Accountant";
                    roleManager.Create(role);
                    AddUsers3(role.Name);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        private void AddUsers3(string roleName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser();

            user.UserName = "FaisalAbbasKazmi";
            user.Email = "bse173033@gmail.com";
            string password = "Bse173033#_";

            var status = UserManager.Create(user, password);

            if (status.Succeeded)
            {
                UserManager.AddToRole(user.Id, roleName);
            }
        }
    }
}
