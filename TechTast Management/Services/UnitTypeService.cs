using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TechTast_Management.ViewModels;

namespace TechTast_Management.Services
{
    public static class UnitTypeService
    {
        public static ObservableCollection<UnitTypeViewModel> GetUnitTypeCollection()
        {
            ObservableCollection<UnitTypeViewModel> typeList = new ObservableCollection<UnitTypeViewModel>();
            using (var db = new RepairCenterModelFirstContainer())
            {
                var typeQuery = from a in db.UnitTypes
                    select new UnitTypeViewModel
                    {
                        TypeID = a.TypeID,
                        Model = a.Model,
                        Description = a.Description
                    };
                typeList = new ObservableCollection<UnitTypeViewModel>(typeQuery);
            }
            return typeList;
        }

        public static bool TryDeleteUnitType(UnitTypeViewModel thisModel)
        {
            bool deleteSuccess = false;
            if (thisModel == null) return deleteSuccess;
            using (var db = new RepairCenterModelFirstContainer())
            {
                var activitiesQuery = db.Activities.FirstOrDefault(x => x.UnitTypeTypeID == thisModel.TypeID);
                if (activitiesQuery == null)
                {
                    var trueType = db.UnitTypes.FirstOrDefault(z => z.TypeID == thisModel.TypeID);
                    db.UnitTypes.Remove(trueType);
                    db.SaveChanges();
                    deleteSuccess = true;
                }
                else
                {
                    System.Windows.MessageBox.Show("Type could not be removed. Activities of that type exist.");
                }
            }
            return deleteSuccess;
        }

        public static bool TryCreateUnitType(string model, string description)
        {
            bool isCreated = false;
            if (model == "" || description == "") return isCreated;
            using (var db = new RepairCenterModelFirstContainer())
            {
                UnitType newType = new UnitType() { Model = model, Description = description };
                db.UnitTypes.Add(newType);
                db.SaveChanges();
                isCreated = true;
            }

            return isCreated;
        }
        public static List<string> GetTypeIDModelList()
        {
            List<string> listOfTypes = new List<string>();
            using (var db = new RepairCenterModelFirstContainer())
            {
                foreach (var type in db.UnitTypes)
                {
                    string thisType = type.TypeID + " " + type.Model;
                    listOfTypes.Add(thisType);
                }
            }
            return listOfTypes;
        }
    }
}