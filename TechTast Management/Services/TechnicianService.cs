using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TechTast_Management.ViewModels;

namespace TechTast_Management.Services
{
    public static class TechnicianService
    {
        public static ObservableCollection<TechnicianViewModel> GetTechnicianViewModelCollection()
        {
            ObservableCollection<TechnicianViewModel> techList = new ObservableCollection<TechnicianViewModel>();
            using (var db = new RepairCenterModelFirstContainer())
            {
                var techQuery = from a in db.Technicians
                    select new TechnicianViewModel
                    {
                        TechID = a.TechID,
                        Alias = a.Alias,
                        FullName = a.FullName,
                        Status = a.Status,
                        TelephoneNumber = a.TelephoneNumber
                    };
                techList = new ObservableCollection<TechnicianViewModel>(techQuery);
            }
            return techList;
        }
        public static bool TryDeleteTechnician(TechnicianViewModel thisModel)
        {
            bool deleteSuccess = false;
            if (thisModel == null) return deleteSuccess;
            using (var db = new RepairCenterModelFirstContainer())
            {
                var techQuery = db.Technicians.FirstOrDefault(x => x.TechID == thisModel.TechID);
                if (techQuery != null)
                {
                    if (techQuery.Status != TechnicianStatus.Busy)
                    {
                        db.Technicians.Remove(techQuery);
                        db.SaveChanges();
                        deleteSuccess = true;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Technician is currently busy.");
                    }
                }
            }
            return deleteSuccess;
        }

        
        public static bool TryCreateTechnician(string alias, string fullName, string telNumber)
        {
            bool isCreated = false;
            if (alias == "" || fullName == "" || telNumber == "") return isCreated;
            using (var db = new RepairCenterModelFirstContainer())
            {
                Technician newTech = new Technician() { Alias = alias, FullName = fullName, TelephoneNumber = telNumber, Status = TechnicianStatus.Free };
                db.Technicians.Add(newTech);
                db.SaveChanges();
                isCreated = true;
                //\(?\d{3}\)?-? *\d{3}-? *-?\d{4}
            }

            return isCreated;
        }

        public static List<string> GetTechnicianIDAliasList()
        {
            List<string> listOfTech = new List<string>();
            using (var db = new RepairCenterModelFirstContainer())
            {
                foreach (var technician in db.Technicians)
                {
                    string thisTech = technician.TechID + " " + technician.Alias;
                    listOfTech.Add(thisTech);
                }
            }
            return listOfTech;
        }

    }
}