using Library.Clinic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Clinic.Services
{
    public static class PhysicianServiceProxy
    {
        public static List<Physician> Physicians { get; private set; } = new List<Physician>();

        public static int LastKey
        {
            get
            {
                if (Physicians.Any())
                {
                    return Physicians.Max(x => x.Id);
                }
                return 0;
            }
        }


        public static void AddPhysician(Physician physician)
        {
            if (physician.Id <= 0)
            {
                physician.Id = LastKey + 1;
            }
            Physicians.Add(physician);
        }

        public static void DeletePhysician(int id)
        {
            var physicianToRemove = Physicians.FirstOrDefault(x => x.Id == id);
            if (physicianToRemove != null)
            {
                Physicians.Remove(physicianToRemove);
            }
        }


        //Find a physician by id
        public static Physician FindPhysician(int id)
        {
            var physicianToFind = Physicians.FirstOrDefault(x => x.Id == id);
            if (physicianToFind != null)
            {
                return physicianToFind;
            }
            return null;
        }

        public static void ListPhysicians()
        {
            foreach (var physician in Physicians)
            {
                Console.WriteLine(physician.ToString());
            }
        }
    }
}
