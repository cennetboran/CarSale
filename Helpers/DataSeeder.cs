using carpass.be.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using CarSale.Data.Entities;
using CarSale.Extensions;

namespace CarSale.Helpers
{
    public class DataSeeder
    {
        public static void Seed(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                try
                {
                    AppDbContext masterContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    masterContext.Database.Migrate();

                    if (!masterContext.Users.Where(x => true).Any())
                    {
                        masterContext.Users.Add(new User()
                        {
                            Name = "Serhat KAYA",
                            PasswordHash = "serhatkaya".GetMD5(),
                            Email = "ser95@w.cx"
                        });
                    }

                    masterContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine($"Inner exception: {ex.InnerException}");
                }
            }
        }
    }
}
