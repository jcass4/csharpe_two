using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleRentalCLI
{
    //----------------------------------------------------------
    //This handels the Rental table in the database, has methods
    //to fulfill SELECT (populate), INSERT, DELETE, and UPDATE
    //----------------------------------------------------------
    class Rental : Persistable
    { 
        //creation of instance variables
        public int ID { get; set; }
        public int VehicleID { get; set; }
        public int RenterID { get; set; }
        public string DateRented { get; set; }
        public string TimeRented { get; set; }
        public string DateDue { get; set; }
        public string TimeDue { get; set; }
        public string DateReturned { get; set; }
        public string TimeReturned { get; set; }
        public int CheckoutWorkerID { get; set; }
        public int CheckInWorkerID { get; set; }

        public Rental()
            : base()
        {
            //database connection 
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                            @"Data Source = C:\Users\Casse_000\Documents\GitHub\csharpclass" +
                            @"\BicycleRental.accdb.accdb";
        }

        //rental object constructor 
        public Rental(int vID, int rID, string dr, string tr,
                string dd, string td, string dreturn, string treturn, int cowID, int ciwID)
        {
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                            @"Data Source = C:\Users\Casse_000\Documents\GitHub\csharpclass" +
                            @"\BicycleRental.accdb.accdb";

            VehicleID = vID;
            RenterID = rID;
            DateRented = dr;
            TimeRented = tr;
            DateDue = dd;
            TimeDue = td;
            DateReturned = dreturn;
            TimeReturned = treturn;
            CheckoutWorkerID = cowID;
            CheckInWorkerID = ciwID;
        }

        //population of a rental selection
        public void populate(int ID)
        {
            string queryString = "SELECT * FROM RENTAL WHERE (ID = " + ID + ")";
            List<Object> vehicleList = getValues(queryString);
            if (vehicleList != null)
            {
                foreach (object result in vehicleList)
                {
                    IEnumerable<Object> row = result as IEnumerable<Object>;
                    int count = 0;
                    foreach (object rowValue in row)
                    {
                        // DEBUG Console.WriteLine(rowValue);
                        if (count == 0)
                            this.ID = Convert.ToInt32(rowValue);
                        else if (count == 1)
                            VehicleID = Convert.ToInt32(rowValue);
                        else if (count == 2)
                            RenterID = Convert.ToInt32(rowValue);
                        else if (count == 3)
                            DateRented = Convert.ToString(rowValue);
                        else if (count == 4)
                            TimeRented = Convert.ToString(rowValue);
                        else if (count == 5)
                            DateDue = Convert.ToString(rowValue);
                        else if (count == 6)
                            TimeDue = Convert.ToString(rowValue);
                        else if (count == 7)
                            DateReturned = Convert.ToString(rowValue);
                        else if (count == 8)
                            TimeReturned = Convert.ToString(rowValue);
                        else if (count == 9)
                            CheckoutWorkerID = Convert.ToInt32(rowValue);
                        else if (count == 10)
                            CheckInWorkerID = Convert.ToInt32(rowValue);
                        count = count + 1;
                    }
                }
            }
        }

        //insert statement of a rental
        public void insert()
        {

            string insertQuery =
            "INSERT INTO Rental (VehicleID, RenterID, DateRented, TimeRented, DateDue, TimeDue," +
            " DateReturned, TimeReturned, CheckoutWorkerID, CheckInWorkerID)" +
            "VALUES (" + "'" + VehicleID + "', '" + RenterID + "', '" + DateRented + "', '" + TimeRented + "', '" +
            DateDue + "', '" + TimeDue + "', '" + DateReturned + "', '"
            + TimeReturned + "', '" + CheckoutWorkerID + "', '" + CheckInWorkerID + "')";

            int returnCode = modifyDatabase(insertQuery);
            if (returnCode != 0)
            {
                Console.WriteLine("Error in inserting Rental object into database");
            }
            else
            {
                Console.WriteLine("Rental object successfully inserted");
                string idQueryString = "SELECT MAX(ID) FROM Rental";
                List<Object> vehicleList = getValues(idQueryString);
                if (vehicleList != null)
                {
                    // DEBUG Console.WriteLine("Got an id from id query");
                    foreach (object result in vehicleList)
                    {
                        IEnumerable<Object> row = result as IEnumerable<Object>;
                        foreach (object rowValue in row)
                        {
                            // DEBUG Console.WriteLine("Retrieved id = " + rowValue);
                            ID = Convert.ToInt32(rowValue);
                        }
                    }
                }
            }
        }

        //update statement of a rental
        public void update()
        {
            string updateQuery = "UPDATE Rental SET " +
                " VehicleID = '" + VehicleID + "'," +
                " RenterID = '" + RenterID + "'," +
                " DateRented = '" + DateRented + "'," +
                " TimeRented = '" + TimeRented + "'," +
                " DateDue = '" + DateDue + "'," +
                " TimeDue = '" + TimeDue + "', " +
                " DateReturned = '" + DateReturned + "', " +
                " TimeReturned = '" + TimeReturned + "', " +
                " CheckoutWorkerID = '" + CheckoutWorkerID + "', " +
                " CheckInWorkerID = '" + CheckInWorkerID + "' " +
                " WHERE " +
                " ID = " + ID;
            Console.WriteLine(updateQuery);
            int returnCode = modifyDatabase(updateQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in updating Rental object into database");
            else
                Console.WriteLine("Rental object successfully updated");
        }

        //delete statement of a rental
        public void delete()
        {
            string deleteQuery = "DELETE FROM Rental WHERE " +
                " ID = " + ID;
            Console.WriteLine(deleteQuery);
            int returnCode = modifyDatabase(deleteQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in deleting Rental object from database");
            else
                Console.WriteLine("Rental object successfully deleted");
        } 
    }
}