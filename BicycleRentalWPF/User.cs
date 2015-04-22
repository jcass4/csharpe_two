using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleRentalCLI
{

    //----------------------------------------------------------
    //This handels the User table in the database, has methods
    //to fulfill SELECT (populate), INSERT, DELETE, and UPDATE
    //----------------------------------------------------------
    public class User : Persistable
    { 
        //creaation of instance variables
        public int ID { get; set; }
        public string BannerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string UserType { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public string DateStatusUpdated { get; set; }

        public User() : base ()
        {
            //database connection
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                            @"Data Source = C:\Users\Casse_000\Documents\GitHub\csharpclass" +
                            @"\BicycleRental.accdb.accdb";
        }

        //user object constructor
        public User(string bi, string f, string l, string pn,
                string ea, string ut, string n)
        {
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                            @"Data Source = C:\Users\Casse_000\Documents\GitHub\csharpclass" +
                            @"\BicycleRental.accdb.accdb";

            BannerId = bi;
            FirstName = f;
            LastName = l;
            PhoneNumber = pn;
            EmailAddress = ea;
            UserType = ut;
            Notes = n;
            Status = "Active";
            DateStatusUpdated = DateTime.Now.ToString("yyy-MM-dd");
        }

        // populate helper method
        private void populateHelper(List<Object> results)
        {
            if (results != null)
            {
                foreach (object result in results)
                {
                    IEnumerable<Object> row = result as IEnumerable<Object>;
                    int count = 0;
                    foreach (object rowValue in row)
                    {
                        // DEBUG Console.WriteLine(rowValue);
                        if (count == 0)
                            ID = Convert.ToInt32(rowValue);
                        else if (count == 1)
                            BannerId = Convert.ToString(rowValue);
                        else if (count == 2)
                            FirstName = Convert.ToString(rowValue);
                        else if (count == 3)
                            LastName = Convert.ToString(rowValue);
                        else if (count == 4)
                            PhoneNumber = Convert.ToString(rowValue);
                        else if (count == 5)
                            EmailAddress = Convert.ToString(rowValue);
                        else if (count == 6)
                            UserType = Convert.ToString(rowValue);
                        else if (count == 7)
                            Notes = Convert.ToString(rowValue);
                        else if (count == 8)
                            Status = Convert.ToString(rowValue);
                        else if (count == 9)
                            DateStatusUpdated = Convert.ToString(rowValue);
                        count = count + 1;
                    }
                }
            }
        }

        //population of a user selection
        public void populate(int givenID)
        {
            string queryString = "SELECT * FROM [User] WHERE (ID = " + givenID + ")";
            List<Object> results = getValues(queryString);
            populateHelper(results);
        }

        //populate with banner id instead of db internal id
        public void populateWithBannerId(int givenID)
        {
            string queryString = "SELECT * FROM [User] WHERE (BannerId = '" + givenID + "')";
            List<Object> results = getValues(queryString);
            populateHelper(results);
        }

        //insert statement of a rental
        public void insert()
        { 
            string insertQuery =
            "INSERT INTO [User] (BannerId, FirstName, LastName, PhoneNumber, EmailAddress, UserType," +
            " Notes, Status, DateStatusUpdated)"  +
            "VALUES (" + "'" + BannerId + "', '" + FirstName + "', '" + LastName + "', '" + PhoneNumber + "', '" +
            EmailAddress + "', '" + UserType + "', '" + Notes + "', '" + Status + "', '" + DateStatusUpdated + "')";

            int returnCode = modifyDatabase(insertQuery);
            if (returnCode != 0)
            {
                Console.WriteLine("Error in inserting User object into database");
            }
            else
            {
                Console.WriteLine("User object successfully inserted");
                string idQueryString = "SELECT MAX(ID) FROM [User]";
                List<Object> results = getValues(idQueryString);
                if (results != null)
                {
                    // DEBUG Console.WriteLine("Got an id from id query");
                    foreach (object result in results)
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

        //update statement of a user
        public void update()
        {
            string updateQuery = "UPDATE [User] SET " +
                " BannerId = '" + BannerId + "' ," +
                " FirstName = '" + FirstName + "' ," +
                " LastName = '" + LastName + "' ," +
                " PhoneNumber = '" + PhoneNumber + "' ," +
                " EmailAddress = '" + EmailAddress + "' ," +
                " UserType = '" + UserType +"' ," +
                " Notes = '" + Notes + "', " +
                " Status = '" + Status + "', " +
                " DateStatusUpdated = '" + DateStatusUpdated + "' " +
                " WHERE " +
                " ID = " + ID;
            Console.WriteLine(updateQuery);
            int returnCode = modifyDatabase(updateQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in updating User object into database");
            else
                Console.WriteLine("User object successfully updated");
        }

        //delete statement of a user
        public void delete()
        {
            string deleteQuery = "DELETE FROM [User] WHERE " +
                " ID = " + this.ID;
            Console.WriteLine(deleteQuery);
            int returnCode = modifyDatabase(deleteQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in deleting User object from database");
            else
                Console.WriteLine("User object successfully deleted");
        }
    }
}
