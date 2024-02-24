//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using SmartGallery.Server.Data;
//using SmartGallery.Server.Models;

//namespace SmartGallery.Server.Utilities
//{
//	public class DataSeeder
//	{
//            public static async Task Initialize(IServiceProvider serviceProvider)
//            {
//                AppDbContext context = serviceProvider.GetService<AppDbContext>();

//                string[] roles = new string[] { "Admin", "User" };

//                foreach (string role in roles)
//                {
//                    var roleStore = new RoleStore<IdentityRole>(context);

//                    if (!context.Roles.Any(r => r.Name == role))
//                    {
//                        await roleStore.CreateAsync(new IdentityRole(role));
//                    }
//                }


//                Customer user = new Customer
//                {
//                    Email = "admin@gmail.com",
//                    NormalizedEmail = "ADMIN@GMAIL.COM",
//                    UserName = "admin@gmail.com",
//                    NormalizedUserName = "ADMIN@GMAIL.COM",
//                    PhoneNumber = "01018004723",
//                    EmailConfirmed = true,
//                    PhoneNumberConfirmed = true,
//                    SecurityStamp = Guid.NewGuid().ToString("D")
//                };


//                if (!context!.Users.Any(u => u.UserName == user.UserName))
//                {
//                    var password = new PasswordHasher<Customer>();
//                    var hashed = password.HashPassword(user, "Admin123!");
//                    user.PasswordHash = hashed;

//                    var userStore = new UserStore<Customer>(context);
//                    var result = userStore.CreateAsync(user);

//                }

//                 await AssignRoles(serviceProvider, user.Email, roles);

//                 await context.SaveChangesAsync();
//            }

//            public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
//            {
//                UserManager<Customer> _userManager = services!.GetService<UserManager<Customer>>();
//                Customer user = await _userManager.FindByEmailAsync(email);
//                var result = await _userManager.AddToRolesAsync(user, roles);

//                return result;
//            }

//        }
//}

