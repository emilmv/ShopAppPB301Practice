namespace ShopAppMVC.ViewModels
{
    public class GroupReturnVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Limit { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public List<StudentInGroupReturnVM> Students { get; set; }
        public int StudentsCount { get; set; }
        public string Image { get; set; }
    }
    public class StudentInGroupReturnVM
    {
        public string Name { get; set; }
        public double Point { get; set; }
    }

}
