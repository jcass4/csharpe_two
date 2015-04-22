using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleRentalCLI
{
    //----------------------------------------------------------
    //This handels the Worker table in the database, has methods
    //to fulfill SELECT (populate), INSERT, DELETE, and UPDATE
    //----------------------------------------------------------
    public class Worker : Persistable
    { 
        //creatoion of instance variables
        public int ID { get; set; }
        public string BannerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Credential { get; set; }
        public string InitialRegistrationDate { get; set; }
        public string WorkerPassword { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
        public string DateStatusUpdated { get; set; }



        public Worker()
            : base()
        {
            //database connection
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                            @"Data Source = C:\Users\Casse_000\Documents\GitHub\csharpclass" +
                            @"\BicycleRental.accdb.accdb";
        }

        //worker object constructor
        public Worker(string b, string f, string l, string pn,
                string ea, string cr, string ird, string wp, string n, string s, string dsu)
        {

            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" +
                            @"Data Source = C:\Users\Casse_000\Documents\GitHub\csharpclass" +
                            @"\BicycleRental.accdb.accdb";

            BannerId = b;
            FirstName = f;
            LastName = l;
            PhoneNumber = pn;
            EmailAddress = ea;
            Credential = cr;
            InitialRegistrationDate = ird;
            WorkerPassword = wp;
            Notes = n;
            Status = "Active";
            DateStatusUpdated = DateTime.Now.ToString("yyyy-MM-dd");

        }

        //population od a worker selection
        public void populateHelper(List<Object> workerResult)
        {
            if (workerResult != null)
            {
                foreach (object result in workerResult)
                {
                    IEnumerable<Object> row = result as IEnumerable<Object>;
                    int count = 0;
                    foreach (object rowValue in row)
                    {
                        // DEBUG Console.WriteLine(rowValue);
                        if (count == 0)
                            this.ID = Convert.ToInt32(rowValue);
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
                            Credential = Convert.ToString(rowValue);
                        else if (count == 7)
                            InitialRegistrationDate = Convert.ToString(rowValue);
                        else if (count == 8)
                            WorkerPassword = Convert.ToString(rowValue);
                        else if (count == 9)
                            Notes = Convert.ToString(rowValue);
                        else if (count == 10)
                            Status = Convert.ToString(rowValue);
                        else if (count == 11)
                            DateStatusUpdated = Convert.ToString(rowValue);
                        count = count + 1;
                    }   //end of foreach
                }   //end of foreach
            }   //end of if
        }   //end of populate()

        //population of a user selection
        public void populate(int givenID)
        {
            string queryString = "SELECT * FROM Worker WHERE (ID = " + givenID + ")";
            List<Object> results = getValues(queryString);
            populateHelper(results);
        }

        //populate with banner id instead of db internal id
        public void populateWithBannerId(int givenID)
        {
            string queryString = "SELECT * FROM Worker WHERE (BannerId = '" + givenID + "')";
            List<Object> results = getValues(queryString);
            populateHelper(results);
        }

        //insert statement of a rental
        public void insert()
        {
            string insertQuery =
            "INSERT INTO Worker (BannerId, FirstName, LastName, PhoneNumber, EmailAddress," +
            " Credential, InitialRegistrationDate, WorkerPassword, Notes, Status, DateStatusUpdated)" +
            "VALUES (" + "'" + BannerId + "', '" + FirstName + "', '" + LastName + "', '" +
            PhoneNumber + "', '" + EmailAddress + "', '" + Credential + "', '" +
            InitialRegistrationDate + "', '" + WorkerPassword + "', '" + Notes + "', '" +
            Status + "', '" + DateStatusUpdated + "')";

            int returnCode = modifyDatabase(insertQuery);
            if (returnCode != 0)
            {
                Console.WriteLine("Error in inserting User object into database");
            }
            else
            {
                Console.WriteLine("User object successfully inserted");
                string idQueryString = "SELECT MAX(ID) FROM Worker";
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
                        }   //end of foreach
                    }   //end of foreach
                }   //end of if
            }   //end of else
        }   //end of insert()

        //update statement of a worker
        public void update()
        {
            string updateQuery = "UPDATE Worker SET " +
                " BannerId = '" + BannerId + "'," +
                " FirstName = '" + FirstName + "'," +
                " LastName = '" + LastName + "'," +
                " PhoneNumber = '" + PhoneNumber + "'," +
                " EmailAddress = '" + EmailAddress + "'," +
                " Credential = '" + Credential + "'," +
                " InitialRegistrationDate = '" + InitialRegistrationDate + "'," +
                " WorkerPassword = '" + WorkerPassword + "'," +
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
        }   //end of update()

        //delete statement of a worker
        public void delete()
        {
            string deleteQuery = "DELETE FROM Worker WHERE " +
                " ID = " + ID;
            Console.WriteLine(deleteQuery);
            int returnCode = modifyDatabase(deleteQuery);
            if (returnCode != 0)
                Console.WriteLine("Error in deleting User object from database");
            else
                Console.WriteLine("User object successfully deleted");
        }   //end of delete()

    }
}