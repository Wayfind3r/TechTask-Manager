
namespace TechTast_Management.ViewModels
{
    public class ActivityViewModel
    {
        public int ActivityID { get; set; }
        public int Priority { get; set; }
        public string SerialNumber { get; set; }
        public System.DateTime DateReceived { get; set; }
        public ActivityStatus CurrentStatus { get; set; }
        public int TechID { get; set; }
        public string TechAlias { get; set; }
        public string Type { get; set; }
    }
}