using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleRentalCLI
{
    //-------------------------------------------------
    //This class will add Rentals to a List where the conditions 
    //are "CheckInWorkerID = 0" (rentals not returned)
    //-------------------------------------------------
    class RentalCollection : Persistable
    {
        private List<Rental> rentalsOut = new List<Rental>();

        public RentalCollection() : base ()
        {
            //databse connection
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                            @"Data Source = C:\Users\Casse_000\Documents\GitHub\csharpclass" +
                            @"\BicycleRental.accdb.accdb";
        }

        //population of the list
        public void populateWithRentedOutBikes()
        {
            string query = "SELECT ID FROM Rental WHERE (CheckInWorkerID = 0)";
            List<Object> results = getValues(query);

            if (results != null)
            {
                foreach (object o in results)
                {
                    IEnumerable<Object> row = o as IEnumerable<Object>;

                    int ID = Convert.ToInt32(row.ElementAt(0));
                    Rental rentalObject = new Rental();
                    rentalObject.populate(ID);
                    rentalsOut.Add(rentalObject);
                }
            }
        }

        //convert to a string
        public override string ToString()
        { 
            string theString = "";
            for(int i = 0; i < rentalsOut.Count; i++)
            {
                theString = theString + rentalsOut.ElementAt(i).ID + " ";
            }
            return theString;
        }
    }
}
