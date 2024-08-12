using ShopAppPB301Practice.Entities;

namespace ShopAppPB301Practice.DTOs.StudentDTOs
{
    public class StudentListDTO
    {
        public StudentListDTO()
        {
            Students = new();
        }

        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public List<StudentReturnDTO> Students { get; set; }
    }
}
