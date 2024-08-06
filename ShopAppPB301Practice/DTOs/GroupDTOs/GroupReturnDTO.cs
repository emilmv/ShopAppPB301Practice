namespace ShopAppPB301Practice.DTOs.GroupDTOs
{
    public class GroupReturnDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<StudentInGroupReturnDTO>Students { get; set; }
        public int StudentsCount { get; set; }
        public string Image { get; set; }
    }
    public class StudentInGroupReturnDTO
    {
        public string Name { get; set; }
        public double Point { get; set; }
    }
}
