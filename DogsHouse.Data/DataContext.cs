using DogsHouse.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DogsHouse.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();

            if (!Dogs.Any())
            {
                AddMockedData();
            }
        }

        private void AddMockedData()
        {
            Dogs.Add(new Dog()
            {
                Id = 1,
                Name = "Neo",
                Color = "Red & Amber",
                TailLength = 22,
                Weight = 32
            });

            Dogs.Add(new Dog()
            {
                Id = 2,
                Name = "Jessy",
                Color = "Black & White",
                TailLength = 7,
                Weight = 14
            });

            Dogs.Add(new Dog()
            {
                Id = 3,
                Name = "Max",
                Color = "Blue",
                TailLength = 10,
                Weight = 20
            });

            Dogs.Add(new Dog()
            {
                Id = 4,
                Name = "Sebas",
                Color = "Yellow",
                TailLength = 7,
                Weight = 10
            });

            Dogs.Add(new Dog()
            {
                Id = 5,
                Name = "Bern",
                Color = "Yellow",
                TailLength = 17,
                Weight = 3
            });

            SaveChanges();
        }
    }
}
