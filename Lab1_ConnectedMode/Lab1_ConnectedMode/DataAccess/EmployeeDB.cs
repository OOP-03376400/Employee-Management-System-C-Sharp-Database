using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;//required for SqlServer Database
using System.Windows.Forms;
using Lab1_ConnectedMode.Business;

namespace Lab1_ConnectedMode.DataAccess
{
   public static class EmployeeDB
    {

        //public static bool IsUniqueId(int tempId)
        //{

        //    //connect the database
        //    SqlConnection connDB = UtilityDB.ConnectDB();
        //    //create and customize an object of type SqlCommand
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = connDB;
        //    cmd.CommandText = "SELECT * FROM Employees ";
        //    SqlDataReader dReader = cmd.ExecuteReader();
        //    if (dReader.HasRows)
        //    {
        //        while (dReader.Read())
        //        {
        //            //Duplicate 
        //            if ((Convert.ToInt32(dReader["EmployeeId"])) == tempId)
        //                return false;
        //        }
        //    }

        //    // true if unique 
        //    return true;

        //}

        public static bool IsUniqueId(int tempId)
        {

            //connect the database
            SqlConnection connDB = UtilityDB.ConnectDB();
            //create and customize an object of type SqlCommand
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = "SELECT * FROM Employees " +
                                " WHERE EmployeeId= " + tempId;
            int id = Convert.ToInt32(cmd.ExecuteScalar());
            MessageBox.Show(id.ToString()); // debug code
            if (id != 0)
            {
                return false;
            }

            // true if unique 
            return true;

        }
        //version 1: Not good
        //public static void SaveRecord(Employee emp)
        //{
        //    SqlConnection connDB = UtilityDB.ConnectDB();
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = connDB;
        //    cmd.CommandText = "INSERT INTO Employees " +
        //                      "VALUES(" + emp.EmployeeId + "," +
        //                      "'" + emp.FirstName + "'," +
        //                      "'" + emp.LastName + "'," +
        //                      "'" + emp.JobTitle + "')";
        //    cmd.ExecuteNonQuery();
        //    connDB.Close();
            
        //}

        public static void SaveRecord(Employee emp)
        {
            SqlConnection connDB = UtilityDB.ConnectDB();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = "INSERT INTO Employees(EmployeeId,FirstName,LastName,JobTitle) " +
                              " VALUES(@EmployeeId,@FirstName,@LastName,@JobTitle) ";
            cmd.Parameters.AddWithValue("@EmployeeId",emp.EmployeeId);
            cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmd.Parameters.AddWithValue("@LastName", emp.LastName);
            cmd.Parameters.AddWithValue("@JobTitle", emp.JobTitle);
            cmd.ExecuteNonQuery();
            connDB.Close();

        }



        //SEARCH 
        public static Employee SearchRecord(int empId)
        {
            Employee emp = new Employee();
            //Connect DB
            SqlConnection connDB = UtilityDB.ConnectDB();
            //SqlCommand object
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connDB;
            cmd.CommandText = "SELECT * FROM Employees " +
                              "WHERE EmployeeId = @EmployeeId " ;
            cmd.Parameters.AddWithValue("@EmployeeId",empId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                emp.FirstName = reader["FirstName"].ToString();
                emp.LastName = reader["LastName"].ToString();
                emp.JobTitle = reader["JobTitle"].ToString();
            }
            else
            {
                emp = null;
            }
            connDB.Close();
            //Close DB

            return emp;
        }

        public static  List<Employee> SearchRecord(int option,string name)
        {
            List<Employee> listEmp = new List<Employee>();
            using (SqlConnection connDB= UtilityDB.ConnectDB())
            {
                switch (option)
                {
                    case 1: // search by FirstName
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = connDB;
                        cmd.CommandText = "SELECT * FROM Employees " +
                                          "WHERE FirstName = @FirstName ";

                        cmd.Parameters.AddWithValue("@FirstName", name);
                        SqlDataReader reader = cmd.ExecuteReader();
                        Employee emp;
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                emp = new Employee();
                                emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                                emp.FirstName = reader["FirstName"].ToString();
                                emp.LastName = reader["LastName"].ToString();
                                emp.JobTitle = reader["JobTitle"].ToString();
                                listEmp.Add(emp);
                            }

                        }
                        else
                        {
                            listEmp = null;
                        }
                        break;
                    case 2: // search by LastName
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = connDB;
                        cmd2.CommandText = "SELECT * FROM Employees " +
                                          "WHERE LastName = @LastName ";

                        cmd2.Parameters.AddWithValue("@LastName", name);
                        SqlDataReader reader2 = cmd2.ExecuteReader();
                        Employee emp2;
                        if (reader2.HasRows)
                        {
                            while (reader2.Read())
                            {
                                emp2 = new Employee();
                                emp2.EmployeeId = Convert.ToInt32(reader2["EmployeeId"]);
                                emp2.FirstName = reader2["FirstName"].ToString();
                                emp2.LastName = reader2["LastName"].ToString();
                                emp2.JobTitle = reader2["JobTitle"].ToString();
                                listEmp.Add(emp2);
                            }

                        }
                        else
                        {
                            listEmp = null;
                        }
                        break;
                    default:
                        break;
                }
            }

            return listEmp;
        }

            public static List<Employee> ListRecord()
            {
                List<Employee> listEmp = new List<Employee>();

                SqlConnection connDB = UtilityDB.ConnectDB();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Employees",connDB);
                SqlDataReader reader = cmd.ExecuteReader();
                Employee emp;
                while (reader.Read())
                {
                    emp = new Employee();
                    emp.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                    emp.FirstName = reader["FirstName"].ToString();
                    emp.LastName = reader["LastName"].ToString();
                    emp.JobTitle = reader["JobTitle"].ToString();
                    listEmp.Add(emp);              
                }
                return listEmp;
            }

        public static void UpdateRecord(Employee emp)
        {
            using (SqlConnection connDB = UtilityDB.ConnectDB())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connDB;
                cmd.CommandText = "UPDATE Employees " +
                                  " set FirstName = @FirstName, " +
                                    " LastName = @LastName, " +
                                    " JobTitle = @JobTitle " +
                                    " WHERE EmployeeId = @EmployeeId ";
                cmd.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                cmd.Parameters.AddWithValue("@FirstName",emp.FirstName);
                cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                cmd.Parameters.AddWithValue("@JobTitle", emp.JobTitle);
                cmd.ExecuteNonQuery();               
            }

        }
        public static void DeleteRecord(int empId)
        {

        }
    }
}
