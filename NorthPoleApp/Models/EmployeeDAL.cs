using Dapper;
using MySqlConnector;
using System.Collections.Generic;
using System.Linq;

namespace NorthPoleApp.Models
{
    public class EmployeeDAL
    {
        //when program is booted up will have no one logged in
        public static int CurrentEmployeeId = -1;

        //setting up a bool to track if santa the "admin" is logged in
        //just to make readability better in some spots in controller
        public static bool IsAdmin => (CurrentEmployeeId == 1);

        //LOGIN METHOD
        public bool isSuccess(Employee e)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from employees where username='{e.UserName}' and password='{e.Password}'";

                connect.Open();
                Employee user = connect.Query<Employee>(sql).FirstOrDefault();
                connect.Close();
                if (user == null)
                {
                    return false;
                }
                else
                {
                    CurrentEmployeeId = user.Id;
                    return true;
                }

            }

        }

        //LOGOUT Method
        public void Logout()
        {
            CurrentEmployeeId = -1;
        }

        //CREATE new employee
        //admin functionality
        public void CreateEmployee(Employee e)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"insert into employees values(0, '{e.FullName}', '{e.Title}', '{e.UserName}', '{e.Password}'";
                connect.Open();
                connect.Query<Employee>(sql);
                connect.Close();

            }
        }

        //READ list of employees and their tasks
        public List<Employee> GetEmployees()
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from employees";
                connect.Open();
                List<Employee> employees = connect.Query<Employee>(sql).ToList();
                foreach (Employee e in employees) 
                {
                    string tasksql = $"select * from tasks inner join employeestasks on tasks.id = employeestasks.taskid where employeeid={e.Id}";
                    e.Tasks = connect.Query<Task>(tasksql).ToList();
                }
                connect.Close();

                return employees;
            }

        }

        //read single employee by ID
        //populates tasks as well. 
        public Employee GetEmployee(int id)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from employees where id = {id}";
                string tasksql = $"select * from tasks inner join employeestasks on tasks.id = employeestasks.taskid where employeeid={id}";
                connect.Open();
                Employee e = connect.Query<Employee>(sql).First();
                e.Tasks = connect.Query<Task>(sql).ToList();
                connect.Close();

                return e;
            }
        }

        //update employee
        public void UpdateEmployee(Employee e)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"update employees set fullname='{e.FullName}', title='{e.Title}', username='{e.UserName}', password='{e.Password}' where id={e.Id}"; 
                connect.Open();
                connect.Query<Employee>(sql);
                connect.Close();
            }
        }


        public void DeleteEmployee(int id)
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"delete from employees where id={id}";
                string ETsql = $"delete from employeestasks where employeeid={id}";
                connect.Open();
                //the e-t table must be scrubbed of employee id before employee can be deleted
                connect.Query<EmployeeTask>(ETsql);
                connect.Query<Employee>(sql);
                connect.Close();

            }
        }
    }
}

