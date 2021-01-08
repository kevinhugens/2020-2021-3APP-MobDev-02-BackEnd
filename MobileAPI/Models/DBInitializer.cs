using MobileAPI.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MobileAPI.Models
{
    public class DBInitializer
    {
        public static void Initialize(BoodschapContext context)
        {
            context.Database.EnsureCreated();
            // Look for any user.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            byte[] saltAdmin = Hashing.getSalt();
            byte[] saltUser = Hashing.getSalt();
            context.Users.AddRange(
                new User { FirstName = "admin", LastName = "admin", Email = "admin@admin.be", Password = Hashing.getHash("admin", saltAdmin), HashSalt = saltAdmin },
                new User { FirstName = "user1", LastName = "user1", Email = "user1@test.be", Password = Hashing.getHash("user1", saltUser), HashSalt = saltUser }
            );
            context.SaveChanges();

            //context.Producten.AddRange(
            //    new Product { Naam = "Product 1", Prijs = 5.00, Omschrijving = "Testing product 1" },
            //    new Product { Naam = "Product 2", Prijs = 10.00, Omschrijving = "Testing product 2" },
            //    new Product { Naam = "Product 3", Prijs = 15.00, Omschrijving = "Testing product 3" });

            string json = File.ReadAllText("Models/products.json");
            var ProductList = JsonConvert.DeserializeObject<List<Product>>(json);
            context.Producten.AddRange(ProductList);
            context.SaveChanges();

            context.Boodschappen.AddRange(
                new Boodschap { UserID = 1, Status = "gepland" },
                new Boodschap { UserID = 1, Status = "uitgevoerd" }
                );
            context.SaveChanges();

            context.BoodschapRows.AddRange(
                new BoodschapRow { BoodschapID = 1, ProductID = 1, Aantal = 2 },
                new BoodschapRow { BoodschapID = 1, ProductID = 2, Aantal = 5 },
                new BoodschapRow { BoodschapID = 1, ProductID = 3, Aantal = 3 },
                new BoodschapRow { BoodschapID = 2, ProductID = 4, Aantal = 8 },
                new BoodschapRow { BoodschapID = 2, ProductID = 5, Aantal = 4 }
                );
            context.SaveChanges();

        }
    }
}
