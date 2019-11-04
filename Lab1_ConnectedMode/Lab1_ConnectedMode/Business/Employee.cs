using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1_ConnectedMode.DataAccess;

namespace Lab1_ConnectedMode.Business
{
    public class Employee 
    {
        private int employeeId;
        private string firstName;
        private string lastName;
        private string jobTitle;
        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
        public bool IsUniqueEmpId (int id)
        {
            return (EmployeeDB.IsUniqueId(id));
        }
        public void SaveEmployee(Employee emp)
        {
            EmployeeDB.SaveRecord(emp);
        }



        public Employee SearchEmployee(int empId)
        {
            return (EmployeeDB.SearchRecord(empId));
        }
       
        public List<Employee> SearchEmployee(int option,string input)
        {
            return (EmployeeDB.SearchRecord(option,input));
        }


        public List<Employee> ListEmployee()
        {
            return (EmployeeDB.ListRecord());
        }
        public void UpdateEmployee(Employee emp)
        {
            EmployeeDB.UpdateRecord(emp);
        }
    }
}
