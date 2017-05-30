using System;
using Owin;
using ClinicaVets.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ClinicaVets
{
    public partial class Startup {
        public void Configuration(IAppBuilder app){
            ConfigureAuth(app);
            //invocação de um método que vai configurar e criar roles e os primeiros utilizadores
            iniciaAplicacao();
        }
        /// cria, caso não existam, as Roles de suporte à aplicação: Veterinario, Funcionario, Dono
        /// cria, nesse caso, também, um utilizador...
        private void iniciaAplicacao()
       {

            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // criar a Role 'Veterinario'
            if (!roleManager.RoleExists("Veterinario"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Veterinario";
                roleManager.Create(role);
            }

            // Criar a role 'Funcionario'
            if (!roleManager.RoleExists("Funcionario"))
            {
                var role = new IdentityRole();
                role.Name = "Funcionario";
                roleManager.Create(role);

                // criar um utilizador 'funcionario'
                var user = new ApplicationUser();
                user.UserName = "b@b.bb";
                user.Email = "b@b.bb";
                string userPWD = "123_Asd";
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Funcionario
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Funcionario");
                }
            }

            // Criar a role 'Dono'
            if (!roleManager.RoleExists("Dono"))
            {
                var role = new IdentityRole();
                role.Name = "Dono";
                roleManager.Create(role);

                // criar um utilizador 'Dono'
                var user = new ApplicationUser();
                user.UserName = "a@a.aa";
                user.Email = "a@a.aa";
                string userPWD = "123_Asd";
                var chkUser = userManager.Create(user, userPWD);

                //Adicionar o Utilizador à respetiva Role-Dono-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(user.Id, "Dono");
                }
            }

            // https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
        }

    }
}
