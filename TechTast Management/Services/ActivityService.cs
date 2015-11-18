using System.Collections.ObjectModel;
using System.Linq;
using TechTast_Management.ViewModels;

namespace TechTast_Management.Services
{
    public static class ActivityService
    {

        public static ObservableCollection<ActivityViewModel> GetActivityViewModel(ActivityStatus thisStatus)
        {
            int statusValue = (int)thisStatus;
            ObservableCollection<ActivityViewModel> activityList;
            using (var db = new RepairCenterModelFirstContainer())
            {
                var activitiesQuery = from a in db.Activities
                    where a.CurrentStatus == statusValue
                    join t in db.Technicians on a.TechnicianTechID equals t.TechID
                    join m in db.UnitTypes on a.UnitTypeTypeID equals m.TypeID
                    select new ActivityViewModel
                    {
                        ActivityID = a.ActivityID,
                        Priority = a.Priority,
                        SerialNumber = a.SerialNumber,
                        DateReceived = a.DateReceived,
                        Age = a.Age,
                        CurrentStatus = a.CurrentStatus,
                        TechAlias = t.Alias,
                        Type = m.Model
                    };
                activityList = new ObservableCollection<ActivityViewModel>(activitiesQuery);
            }
            return activityList;
        }
    }
}