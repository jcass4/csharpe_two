using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleRentalCLI
{
    //-------------------------------------------------
    //This class will add Vehicles to a List where the conditions 
    //are "(PhysicalCondition = 'Good' AND Status = 'Active')"
    //-------------------------------------------------
    class VehicleCollection : Persistable
    {
        private List<Vehicle> bikes = new List<Vehicle>();

        public VehicleCollection() : base ()
        {
            //database connection
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                            @"Data Source = C:\Users\Casse_000\Documents\GitHub\csharpclass" +
                            @"\BicycleRental.accdb.accdb";
        }

        //population of the list
        public void populateWithGoodAndAvailableBikes()
        {
            string query = "SELECT ID FROM Vehicle WHERE (PhysicalCondition = 'Good' AND Status = 'Active')";
            List<Object> results = getValues(query);

            if (results != null)
            {
                foreach (object o in results)
                {
                    IEnumerable<Object> row = o as IEnumerable<Object>;

                    int ID = Convert.ToInt32(row.ElementAt(0));
                    Vehicle vehicleObject = new Vehicle();
                    vehicleObject.populate(ID);
                    bikes.Add(vehicleObject);
                }
            }
        }

        //convert to a string
        public override string ToString()
        {
            string theString = "";
            for (int i = 0; i < bikes.Count; i++)
            {
                theString = theString + bikes.ElementAt(i).ID + " ";
            }
            return theString;
        }
    }
}
