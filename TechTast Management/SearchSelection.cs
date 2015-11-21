using System.ComponentModel.DataAnnotations;

namespace TechTast_Management
{
    public enum SearchSelection
    {
        ActivityID,
        Priority,
        [Display(Name = "Serial Number")]
        SerialNumber,
        [Display(Name = "Date Received")]
        DateReceived,
        Age,
        [Display(Name = "Current Status")]
        CurrentStatus,
        [Display(Name = "Tech Alias")]
        TechAlias,
        Type
    }

}