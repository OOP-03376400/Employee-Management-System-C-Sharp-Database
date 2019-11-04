using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab1_ConnectedMode.DataAccess;
using Lab1_ConnectedMode.Validation;
using Lab1_ConnectedMode.Business;

namespace Lab1_ConnectedMode.GUI
{
    public partial class FormEmployee : Form
    {
        public FormEmployee()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            string input = "";
            input = textBoxEmpId.Text.Trim();
            if (!Validator.IsValidId(input, 4))
            {
                MessageBox.Show("Employee ID must be 4-digit number.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxEmpId.Clear();
                textBoxEmpId.Focus();
                return;

            }

            Employee emp = new Employee();
            int tempId = Convert.ToInt32(textBoxEmpId.Text.Trim());
            if (!(emp.IsUniqueEmpId(tempId)))
            {
                MessageBox.Show("Employee ID already exists.", "Duplicate EmployeeID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxEmpId.Clear();
                textBoxEmpId.Focus();
                return;
            }
           
            input = textBoxFirstName.Text.Trim();
            if (!(Validator.IsValidName(input)))
            {
                MessageBox.Show("First name must contain letters or space(s)", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFirstName.Clear();
                textBoxFirstName.Focus();
                return;
            }

            input = textBoxLastName.Text.Trim();
            if (!(Validator.IsValidName(input)))
            {
                MessageBox.Show("Last name must contain letters or space(s)", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxLastName.Clear();
                textBoxLastName.Focus();
                return;
            }

            input = textBoxJobTitle.Text.Trim();
            if (!(Validator.IsValidName(input)))
            {
                MessageBox.Show("Job Title must contain letters or space(s)", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxJobTitle.Clear();
                textBoxJobTitle.Focus();
                return;
            }

            //valid data
            emp.EmployeeId = Convert.ToInt32(textBoxEmpId.Text.Trim());
            emp.FirstName = textBoxFirstName.Text.Trim();
            emp.LastName = textBoxLastName.Text.Trim();
            emp.JobTitle = textBoxJobTitle.Text.Trim();
            emp.SaveEmployee(emp);
            MessageBox.Show("Employee record has been saved successfully.", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void comboBoxOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = comboBoxOption.SelectedIndex;
            switch (index)
            {
                case 0:
                    labelMessage.Text = "Please enter Employee ID ";
                    textBoxInput.Clear();
                    textBoxInput.Focus();
                    break;
                default:
                    break;
            }
        }



        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int selectedIndex = comboBoxOption.SelectedIndex;
            List<Employee> listEmployee = new List<Employee>();
            switch (selectedIndex)
            {
                case 0: //Search employee by EmployeeId
                    Employee emp = new Employee();
                    emp = emp.SearchEmployee(Convert.ToInt32(textBoxInput.Text));
                    if (emp !=null)
                    {
                        textBoxEmpId.Text = emp.EmployeeId.ToString();
                        textBoxFirstName.Text = emp.FirstName;
                        textBoxLastName.Text = emp.LastName;
                        textBoxJobTitle.Text = emp.JobTitle;

                    }
                    else
                    {
                        MessageBox.Show("Employee record not found!", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case 1: //Search employee by First Name
                    Employee anEmp = new Employee();
                    listEmployee = anEmp.SearchEmployee(1,textBoxInput.Text.Trim());
                    listViewEmployee.Items.Clear();
                    if (listEmployee.Count != 0)
                    {
                        foreach (Employee emp1 in listEmployee)
                        {
                            ListViewItem item = new ListViewItem(emp1.EmployeeId.ToString());
                            item.SubItems.Add(emp1.FirstName);
                            item.SubItems.Add(emp1.LastName);
                            item.SubItems.Add(emp1.JobTitle);
                            listViewEmployee.Items.Add(item);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee list is empty" + "\n" + "Please enter Employee Data", "No Employee Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    break;
                case 2: //Search employee by LastName
                    Employee anEmp2 = new Employee();
                    listEmployee = anEmp2.SearchEmployee(2, textBoxInput.Text.Trim());
                    listViewEmployee.Items.Clear();
                    if (listEmployee.Count != 0)
                    {
                        foreach (Employee emp1 in listEmployee)
                        {
                            ListViewItem item = new ListViewItem(emp1.EmployeeId.ToString());
                            item.SubItems.Add(emp1.FirstName);
                            item.SubItems.Add(emp1.LastName);
                            item.SubItems.Add(emp1.JobTitle);
                            listViewEmployee.Items.Add(item);

                        }
                    }
                    else
                    {
                        MessageBox.Show("Employee list is empty" + "\n" + "Please enter Employee Data", "No Employee Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    break;
                default:
                    break;
            }
        }


        //LIST list
        private void buttonListAll_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            List<Employee> listEmp = new List<Employee>();  
                listEmp= emp.ListEmployee();
            listViewEmployee.Items.Clear();
            if (listEmp.Count!=0)
            {
                foreach (Employee anEmp in listEmp)
                {
                    ListViewItem item = new ListViewItem(anEmp.EmployeeId.ToString());
                    item.SubItems.Add(anEmp.FirstName);
                    item.SubItems.Add(anEmp.LastName);
                    item.SubItems.Add(anEmp.JobTitle);
                    listViewEmployee.Items.Add(item);

                }
            }
            else
            {
                MessageBox.Show("Employee list is empty" + "\n" + "Please enter Employee Data", "No Employee Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FormEmployee_Load(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }
    }
}
