using System.Collections.Generic;
using System.Linq;

namespace NewsFetcherAPI.Data
{
    public static class DbInitializer
    {
        public static void Initialize(NfDbContext context)
        {
            context.Database.EnsureCreated();

             if (context.Categories.Any())
                 return;
             
             var categories = new List<Category>
            {
                new Category { Name = "Technology", Value = "technology" },
                new Category { Name = "Business", Value = "business" },
                new Category { Name = "Entertainment", Value = "entertainment" },
                new Category { Name = "Miscellaneous", Value = "general" }
            };

            context.AddRange(categories);
            context.SaveChanges();
        }
    }
}