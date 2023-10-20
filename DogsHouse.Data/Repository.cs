using DogsHouse.Data.Models;

namespace DogsHouse.Data
{
    public class DogRepository
    {
        protected readonly DataContext Context;

        public DogRepository(DataContext context)
        {
            Context = context;
        }

        public IQueryable<Dog> GetAll()
        {
            return Context.Dogs.AsQueryable();
        }

        public Dog GetById(int id)
        {
            return Context.Dogs.Find(id);
        }

        public void Insert(Dog newDog)
        {
            if (newDog.Id != default)
            {
                throw new ArgumentException("Use default value for id property");
            }

            Context.Dogs.Add(newDog);
        }

        public void Update(Dog newDog)
        {
            if (newDog.Id == default)
            {
                throw new ArgumentException("Can not use default id value");
            }

            var oldDog = GetById(newDog.Id);

            Context.Entry(oldDog).CurrentValues.SetValues(newDog);
        }

        public void Delete(int id)
        {
            var dog = GetById(id);

            if (dog == null)
            {
                throw new ArgumentException("Model with this id was not found");
            }

            Context.Dogs.Remove(dog);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}

