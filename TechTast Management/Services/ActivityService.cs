using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using TechTast_Management.ViewModels;

namespace TechTast_Management.Services
{
    public static class ActivityService
    {

        public static ObservableCollection<ActivityViewModel> GetActivityViewModelCollection(ActivityStatus thisStatus)
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
                        CurrentStatus = thisStatus,
                        TechID = t.TechID,
                        TechAlias = t.Alias,
                        Type = m.Model
                    };
                activityList = new ObservableCollection<ActivityViewModel>(activitiesQuery);
            }
            return activityList;
        }

        public static void DeleteActivityFromDB(ActivityViewModel thisActivity)
        {
            if (thisActivity == null) return;
            using (var db = new RepairCenterModelFirstContainer())
            {
                var activityToDelete =
                    db.Activities.FirstOrDefault(x => x.ActivityID == thisActivity.ActivityID);
                var techID = activityToDelete.TechnicianTechID;
                var techniciansQuery = db.Technicians.FirstOrDefault(x => x.TechID == techID);
                if (techniciansQuery != null)
                {
                    techniciansQuery.Status = TechnicianStatus.Free;
                }
                db.Activities.Remove(activityToDelete);
                db.SaveChanges();
            }
        }

        public static void QATryReturnToTechnician(ActivityViewModel thisActivity)
        {
            if (thisActivity == null) return;
            using (var db = new RepairCenterModelFirstContainer())
            {
                var activityToMove = db.Activities.FirstOrDefault(x => x.ActivityID == thisActivity.ActivityID);
                var techID = activityToMove.TechnicianTechID;
                var techniciansQuery = db.Technicians.FirstOrDefault(x => x.TechID == techID);
                bool techIsBusyOrNA = true;
                if (techniciansQuery != null)
                {
                    if (techniciansQuery.Status == TechnicianStatus.Free)
                    {
                        techIsBusyOrNA = false;
                        techniciansQuery.Status =TechnicianStatus.Busy;
                        activityToMove.CurrentStatus = (int)ActivityStatus.Pending;
                        db.SaveChanges();
                    }
                }
                if (techIsBusyOrNA)
                {
                    System.Windows.MessageBox.Show("Technician is currently not available.");
                }
            }
        }

        public static bool TryMovePendingActivityToQA(ActivityViewModel thisActivityViewModel)
        {
            bool success = false;
            if (thisActivityViewModel == null) return success;
            using (var db = new RepairCenterModelFirstContainer())
            {
                var activityToMove = db.Activities.FirstOrDefault(x => x.ActivityID == thisActivityViewModel.ActivityID);
                if (activityToMove != null)
                {
                    var currentTechnician =
                        db.Technicians.FirstOrDefault(t => t.TechID == activityToMove.TechnicianTechID);
                    currentTechnician.Status = TechnicianStatus.Free;
                    activityToMove.CurrentStatus = (int)ActivityStatus.QA;
                    db.SaveChanges();
                    success = true;
                }
            }
            return success;
        }

        public static bool TryUpdateActivity(ActivityViewModel thisActivityViewModel, int priority, string technicianIDALIAS, string serialNumber, string date)
        {
            bool isUpdated = false;
            DateTime trueDate;
            if(thisActivityViewModel == null || technicianIDALIAS == null
                || serialNumber==null || date ==null || !DateTime.TryParse(date, out trueDate)) return isUpdated;

            using (var db = new RepairCenterModelFirstContainer())
            {
                var activityToUpdate = db.Activities.FirstOrDefault(x => x.ActivityID == thisActivityViewModel.ActivityID);
                if (activityToUpdate != null)
                {
                    string[] tech = technicianIDALIAS.Split(new char[] {' '});
                    int techID = int.Parse(tech[0]);
                    if (techID != thisActivityViewModel.TechID)
                    {
                        var currentTechnician =
                            db.Technicians.FirstOrDefault(t => t.TechID == activityToUpdate.TechnicianTechID);
                        currentTechnician.Status = TechnicianStatus.Free;
                        var technician =
                            db.Technicians.FirstOrDefault(t => t.TechID == techID);
                        technician.Status = TechnicianStatus.Busy;
                    }
                    activityToUpdate.DateReceived = trueDate;
                    activityToUpdate.Priority = priority+2;
                    activityToUpdate.TechnicianTechID = techID;
                    activityToUpdate.SerialNumber = serialNumber;
                    db.SaveChanges();
                    isUpdated = true;
                }
            }
            return isUpdated;
        }

        public static bool TryCreateActivity(int priority, string type, string technicianIDALIAS, string serialNumber,
            string date)
        {
            bool isCreated = false;
            DateTime trueDate;
            if (technicianIDALIAS == null
                || serialNumber == null || date == null || !DateTime.TryParse(date, out trueDate))
                return isCreated;
            using (var db = new RepairCenterModelFirstContainer())
            {
                string[] tech = technicianIDALIAS.Split(new char[] { ' ' });
                int techID = int.Parse(tech[0]);
                string[] typeSplit = type.Split(new char[] {' '});
                    var currentTechnician =
                        db.Technicians.FirstOrDefault(t => t.TechID == techID);
                if (currentTechnician.Status == TechnicianStatus.Free)
                {
                    currentTechnician.Status = TechnicianStatus.Busy;
                    Activity newActivity = new Activity()
                    {
                        CurrentStatus = 1, DateReceived = trueDate, Priority = priority,
                        SerialNumber = serialNumber, TechnicianTechID = techID, UnitTypeTypeID = int.Parse(typeSplit[0])
                    };
                    db.Activities.Add(newActivity);
                    db.SaveChanges();
                    isCreated = true;
                }
            }
            return isCreated;
        }

        public static ObservableCollection<ActivityViewModel> SearchByActivityValueAndStatus(string valueType, string value,
            DataGrid thisGrid)
        {
            string commandFragmet = valueType;
            ObservableCollection<ActivityViewModel> activityList = (ObservableCollection<ActivityViewModel>)thisGrid.ItemsSource;
            switch (valueType)
            {
                case "ActivityID":
                    int id;
                    if (int.TryParse(value, out id))
                    {
                        thisGrid.ItemsSource = from a in activityList where a.ActivityID == id select a;
                    }
                    break;
                case "Priority":
                    int priority;
                    if (int.TryParse(value, out priority))
                    {
                        thisGrid.ItemsSource = from a in activityList where a.Priority == priority select a;
                    }
                    break;
                case "SerialNumber":
                    thisGrid.ItemsSource = from a in activityList where a.SerialNumber == value select a;
                    break;
                case "TechAlias":
                    thisGrid.ItemsSource = from a in activityList where a.TechAlias == value select a;
                    break;
                case "Type":
                    thisGrid.ItemsSource = from a in activityList where a.Type == value select a;
                    break;
            }
            return activityList;
        }

        public static string GetTechnicianIDAlias(ActivityViewModel thisActivity)
        {
            return thisActivity.TechID+" "+thisActivity.TechAlias;
        }
    }
}