using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new() {
                        FullName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new() {
                        FullName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    },
                    new() {
                        FullName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

            }
            if (!context.Categories.Any() && !context.SubCategories.Any() && !context.Locations.Any())
            {
                var categories = new List<Category>();
                var subCategories = new List<SubCategory>();
                var locations = new List<Location>();

                for (int i = 1; i <= 10; i++)
                {
                    var name = "Category" + i.ToString();
                    categories.Add(new Category
                    {
                        Name = name.ToUpper(),
                    });
                }
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();

                foreach (var category in categories)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        var name = category.Name + "SubCategory" + i.ToString();
                        subCategories.Add(new SubCategory
                        {
                            Name = name.ToUpper(),
                            Category = category   
                        });
                    }

                }
                for (int i = 1; i <= 10; i++)
                {
                    var name = "Location" + i.ToString();
                    locations.Add(new Location
                    {
                        Name = name.ToUpper(),
                    });
                }

                context.SubCategories.AddRange(subCategories);
                context.Locations.AddRange(locations);
                await context.SaveChangesAsync();
            }
        }
    }
}
