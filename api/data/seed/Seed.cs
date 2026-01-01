namespace fotoservice.data;

public class Seed
{
    public static async Task SeedUsers(
        UserManager<AppUser> manager,
        RoleManager<AppRole> roleManager
    )
    {
        if (await manager.Users.AnyAsync())
            return;
        var userData = await System.IO.File.ReadAllTextAsync("data/seed/UserSeedData.json");
        var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
        if (users == null)
            return;

        var roles = new List<AppRole>
        {
            new AppRole { Name = "Surgery" },
            new AppRole { Name = "Moderator" },
            new AppRole { Name = "Sponsor" },
            new AppRole { Name = "Refcard" },
            new AppRole { Name = "Admin" },
            new AppRole { Name = "Cardiologist" },
            new AppRole { Name = "Chef" }
        };
        foreach (var role in roles)
        {
            await roleManager.CreateAsync(role);
        }
        foreach (AppUser ap in users)
        {
            ap.UserName = ap.UserName.ToLower();
            await manager.CreateAsync(ap, "Pa$$w0rd");
            await manager.AddToRoleAsync(ap, "Surgery");
        }

        var admin = new AppUser
        {
            UserName = "admin@gfancy.nl",
            Email = "admin@gfancy.nl",
            Gender = "male",
            PaidTill = new DateTime().AddYears(2250)
        };
        await manager.CreateAsync(admin, "Pa$$w0rd");
        await manager.AddToRoleAsync(admin, "Admin");
    }

 
    public static async Task SeedCategories(ApplicationDbContext context)
    {
        if (await context.Categories.AnyAsync())
            return;
        int counter = 0;
        var catData = await System.IO.File.ReadAllTextAsync("data/seed/CategoryData.json");
        var categories = JsonSerializer.Deserialize<List<Category>>(catData);

        if (categories != null)
        {
            // ORDER BY NAME
            categories = categories.OrderBy(c => c.Name).ToList();
            foreach (Category im in categories)
            {
                // MAKE FIRST CHARACTER A CAPITAL LETTER
                im.Name = char.ToUpper(im.Name[0]) + im.Name.Substring(1);
                if (im.Id == 1)
                {
                    im.MainPhoto = 1; // Set MainPhoto for the first category
                    counter = im.Number_of_images;
                }
                else
                {
                    im.MainPhoto = counter;
                    counter = counter + im.Number_of_images - 1;
                }
                // save image to database
                _ = context.Categories.Add(im);
            }
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedImages(ApplicationDbContext context, IImage image)
    {
        var counter = 0;
        var catList = new List<Category>();
        ImageDto test;

        if (await context.Images.AnyAsync())
            return;

        catList = await image.getCategories();

        if (catList != null)
        {
            for (int x = 0; x < catList.Count; x++)
            {
                counter = 0;
                if (catList[x].Number_of_images != 0)
                {
                    counter += (int)catList[x].Number_of_images;

                    for (int y = 1; y < counter; y++)
                    {
                        string? url = catList[x].Name + "/" + y.ToString() + ".jpg";

                        test = new ImageDto
                        {
                            //Id = x.ToString(),
                            ImageUrl = url,
                            YearTaken = 1995,
                            Location = "",
                            Familie = "",
                            Category = catList[x].Id,
                            Series = "",
                            Spare1 = "",
                            Spare2 = "",
                            Spare3 = "",
                        };
                        await image.addImage(test);
                    }
                }

                //addImage(catList[x],counter,url,_dapper);
            }
        }
    }

   
}
