using ShopAppPB301Practice.Entities;

namespace ShopAppPB301Practice.DTOs.StudentDTOs
{
    public class StudentReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Point { get; set; }
        public int GroupId { get; set; }
        public GroupInStudentReturnDTO GroupName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime DeleteDate { get; set; }
    }
    public class GroupInStudentReturnDTO
    {
        public string Name { get; set; }
        public int StudentCount { get; set; }
    }
}
