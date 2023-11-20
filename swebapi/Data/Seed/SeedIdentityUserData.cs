using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using swebapi.Models;

namespace swebapi.Data.Seed
{
    public static class SeedIdentityUserData
    {
        public static void SeedUserIdentityData(this ModelBuilder modelBuilder)
        {
            // Agregar el rol "Administrador" a la tabla AspNetRoles
            string AdministradorGeneralRoleId = Guid.NewGuid().ToString();
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = AdministradorGeneralRoleId,
                Name = "Administrador",
                NormalizedName = "Administrador".ToUpper()
            });

            // Agregamos un usuario a la tabla AspNetUsers
            var UsuarioId = Guid.NewGuid().ToString();
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = UsuarioId, // primary key
                    UserName = "gvera@uv.mx",
                    Email = "gvera@uv.mx",
                    NormalizedEmail = "gvera@uv.mx".ToUpper(),
                    Nombre = "Guillermo Humberto Vera Amaro",
                    NormalizedUserName = "gvera@uv.mx".ToUpper(),
                    PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "gverapwd")
                }
            );

            // Aplicamos la relación entre el usuario y el rol en la tabla AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdministradorGeneralRoleId,
                    UserId = UsuarioId
                }
            );

            // Agregamos un usuario a la tabla AspNetUsers
            UsuarioId = Guid.NewGuid().ToString();
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = UsuarioId, // primary key
                    UserName = "sperez@uv.mx",
                    Email = "sperez@uv.mx",
                    NormalizedEmail = "sperez@uv.mx".ToUpper(),
                    Nombre = "Saúl Pérez García",
                    NormalizedUserName = "sperez@uv.mx".ToUpper(),
                    PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "saulpwd")
                }
            );

            // Aplicamos la relación entre el usuario y el rol en la tabla AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdministradorGeneralRoleId,
                    UserId = UsuarioId
                }
            );

            // Agregamos un usuario a la tabla AspNetUsers
            UsuarioId = Guid.NewGuid().ToString();
            modelBuilder.Entity<CustomIdentityUser>().HasData(
                new CustomIdentityUser
                {
                    Id = UsuarioId, // primary key
                    UserName = "gochoa@gmail.com",
                    Email = "gochoa@gmail.com",
                    NormalizedEmail = "gochoa@gmail.com".ToUpper(),
                    Nombre = "Gerardo Ochoa Martíniez",
                    NormalizedUserName = "gochoa@gmail.com".ToUpper(),
                    PasswordHash = new PasswordHasher<CustomIdentityUser>().HashPassword(null, "gerapwd")
                }
            );

            // Aplicamos la relación entre el usuario y el rol en la tabla AspNetUserRoles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = AdministradorGeneralRoleId,
                    UserId = UsuarioId
                }
            );
        }
    }
}
