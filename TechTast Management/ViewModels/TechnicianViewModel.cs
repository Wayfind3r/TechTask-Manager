namespace TechTast_Management.ViewModels
{
    public class TechnicianViewModel
    {
        public int TechID { get; internal set; }
        public string Alias { get; set; }
        public string FullName { get; set; }
        public TechnicianStatus Status { get; set; }
        public string TelephoneNumber { get; set; }
    }
}