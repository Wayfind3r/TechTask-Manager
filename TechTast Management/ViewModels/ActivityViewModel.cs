namespace TechTast_Management.ViewModels
{
    public class ActivityViewModel
    {
        public int ActivityID { get; set; }
        public int Priority { get; set; }
        public string SerialNumber { get; set; }
        public System.DateTime DateReceived { get; set; }
        public int Age { get; set; }
        public int CurrentStatus { get; set; }
        public string TechAlias { get; set; }
        public string Type { get; set; }
    }
}