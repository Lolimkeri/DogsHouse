using DogsHouse.Data.Models;

namespace DogsHouse.Interfaces
{
    public interface IDogService
    {
        List<Dog> GetDogRecords(string SortColumn = null, string SortOrder = null, int? PageNumber = null, int? PageSize = null);

        Dog GetDogRecord(int id);

        void AddDogRecord(string name, string color, double tailLength, double weight);

        void UpdateDogRecord(int id, string name, string color, double tailLength, double weight);

        void DeleteDogRecord(int id);
    }
}
