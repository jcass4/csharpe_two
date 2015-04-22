using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleRentalCLI
{

    //----------------------------------------------------------
    //This handels the Vehicle table in the database, has methods
    //to fulfill SELECT (populate), INSERT, DELETE, and UPDATE
    //----------------------------------------------------------
    class Vehicle : Persistable
    { 
        //creation of the instance variable
        public int ID { get; set; }
        public string BikeMake { get; set; }
        public string ModelNumber { get; set; }
        public string SerialNumber { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string PhysicalCondition { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public string DateStatusUpdated { get; set; }

        public Vehicle() : base ()
        {
            //databse connection
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                            @"Data Source = C:\Users\Casse_000\Documents\GitHub\csharpclass" +
                            @"\BicycleRental.accdb.accdb";
        }

        //vehice object constructor
        public Vehicle(string bm, string mn, string sn, string c,
                string d, string l, string pc, string n, string s, string dsu)
        {
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                            @"Data Source = C:\Users\Casse_000\Documents\GitHub\csharpclass" +
                            @"\BicycleRental.accdb.accdb";

            BikeMake = bm;
            ModelNumber = mn;
            SerialNumber = sn;
            Color = c;
            Description = d;
            Location = l;
            PhysicalCondition = pc;
            Notes = n;
            Status = "Active";
            DateStatusUpdated = DateTime.Now.ToString("yyy-MM-dd");
        }

        //population of a vehicle selection
        public void populate(int ID)
        {
            string queryString = "SELECT * FROM VEHICLE WHERE (ID = " + ID + ")";
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
                            BikeMake = Convert.ToString(rowValue);
                        else if (count == 2)
                            ModelNumber = Convert.ToString(rowValue);
                        else if (count == 3)
                            SerialNumber = Convert.ToString(rowValue);
                        else if (count == 4)
                            Color = Convert.ToString(rowValue);
                        else if (count == 5)
                            Description = Convert.ToString(rowValue);
                        else if (count == 6)
                            Location = Convert.ToString(rowValue);
                        else if (count == 7)
                            PhysicalCondition = Convert.ToString(rowValue);
                        else if (count == 8)
                            Notes = Convert.ToString(rowValue);
                        else if (count == 9)
                            Status = Convert.ToString(rowValue);
                        else if (count == 10)
                            DateStatusUpdated = Convert.ToString(rowValue);
                        count = count + 1;
                    }
                }
            }
        }

        //insesrt statement of a vehicle
        public void insert()
        {

            string insertQuery =
            "INSERT INTO Vehicle (BikeMake, ModelNumber, SerialNumber, Color, Description, Location," +
            " PhysicalCondition, Notes, Status, DateStatusUpdated)"  +
            "VALUES (" + "'" + BikeMake + "', '" + ModelNumber + "', '" + SerialNumber + "', '" + Color + "', '" +
            Description + "', '" + Location +  "', '" + PhysicalCondition + "', '"
            + Notes + "', '" + Status + "', '" + DateStatusUpdated + "')";

            int returnCode = modifyDatabase(insertQuery);
            if (returnCode != 0)
            {
                Console.WriteLine("Error in inserting Vehicle object into database");
            }
            else
            {
                Console.WriteLine("Vehicle object successfully inserted");
                string idQueryString = "SELECT MAX(ID) FROM Vehicle";
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

        //update statement of a vehicle
        public void update()
        {
            string updateQuery = "UPDATE Vehicle SET " +
                " BikeMake = '" + BikeMake + "'," +
                " ModelNumber = '" + ModelNumber + "'," +
                " SerialNumber = '" + SerialNumber + "'," +
                " Color = '" + Color + "'," +
                " Description = '" + Description + "'," +
                " Location = '" + Location + "', " +
                " PhysicalCondition = '" + PhysicalCondition + "', " +
                " Notes = '" + Notes + "', " +
                " Status = '" + Status + "', " +
                " DateStatusUpdated = '" + DateStatusUpdated + "' " +
                " WHERE " +
                " ID = " + ID;
            Console.WriteLine(updateQuery);
            int returnCode = modifyDatabase(updateQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in updating Vehicle object into database");
            else
                Console.WriteLine("Vehicle object successfully updated");
        }

        //delete state of a vehicle
        public void delete()
        {
            string deleteQuery = "DELETE FROM Vehicle WHERE " +
                " ID = " + ID;
            Console.WriteLine(deleteQuery);
            int returnCode = modifyDatabase(deleteQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in deleting Vehicle object from database");
            else
                Console.WriteLine("Vehicle object successfully deleted");
        } 
    }
}
