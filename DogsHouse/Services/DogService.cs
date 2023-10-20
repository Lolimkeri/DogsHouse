using DogsHouse.Data;
using DogsHouse.Data.Models;
using DogsHouse.Interfaces;
using System.Linq.Expressions;

namespace DogsHouse.Services
{
    public class DogService : IDogService
    {
        private readonly DogRepository _dogRepository;

        public DogService(DogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        public List<Dog> GetDogRecords(string SortColumn = null, string SortOrder = null, int? PageNumber = null, int? PageSize = null)
        {
            var dogRecords = _dogRepository.GetAll();


            if (SortOrder?.ToLower() == "desc")
            {
                dogRecords = dogRecords.OrderByDescending(GetSortProperty(SortColumn));
            }
            else
            {
                dogRecords = dogRecords.OrderBy(GetSortProperty(SortColumn));
            }

            if (PageNumber is not null)
            {
                dogRecords = dogRecords
                    .Skip((PageNumber.Value - 1) * PageSize.Value)
                    .Take(PageSize.Value);
            }

            return dogRecords.ToList();
        }

        public Dog GetDogRecord(int id)
        {
            return _dogRepository.GetById(id);
        }

        public void AddDogRecord(string name, string color, double tailLength, double weight)
        {
            var dogRecord = new Dog()
            {
                Name = name,
                Color = color,
                TailLength = tailLength,
                Weight = weight
            };

            if (!IsValidDog(dogRecord, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            if (_dogRepository.GetAll().Any(x => x.Name == dogRecord.Name))
            {
                throw new ArgumentException("Dog with this name already exists");
            }

            _dogRepository.Insert(dogRecord);
            _dogRepository.Save();
        }

        public void UpdateDogRecord(int id, string name, string color, double tailLength, double weight)
        {
            var dogRecord = new Dog()
            {
                Id = id,
                Name = name,
                Color = color,
                TailLength = tailLength,
                Weight = weight
            };

            if (!IsValidDog(dogRecord, out var errorMessage))
            {
                throw new ArgumentException(errorMessage);
            }

            _dogRepository.Update(dogRecord);
            _dogRepository.Save();
        }

        public void DeleteDogRecord(int id)
        {
            _dogRepository.Delete(id);
            _dogRepository.Save();
        }

        private bool IsValidDog(Dog dog, out string errorMessage)
        {
            if (string.IsNullOrEmpty(dog.Name) || string.IsNullOrEmpty(dog.Color))
            {
                errorMessage = "Name or Color values are invalid";
                return false;
            }

            if (dog.TailLength <= 0 || dog.Weight <= 0)
            {
                errorMessage = "Tail length or Weight values are invalid";
                return false;
            }

            errorMessage = "";
            return true;
        }

        private static Expression<Func<Dog, object>> GetSortProperty(string SortColumn)
        {
            return SortColumn?.ToLower() switch
            {
                "name" => dog => dog.Name,
                "color" => dog => dog.Color,
                "taillength" => dog => dog.TailLength,
                "weight" => dog => dog.Weight,
                _ => dog => dog.Id
            };
        }
    }
}
