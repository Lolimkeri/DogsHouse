namespace DogsHouse.Models
{
    public class GetDogsDTO
    {      
        public string SortColumn { get; set; }

        public string SortOrder { get; set; }

        public int? PageNumber { get; set; }

        public int? PageSize { get; set; }
    }
}
