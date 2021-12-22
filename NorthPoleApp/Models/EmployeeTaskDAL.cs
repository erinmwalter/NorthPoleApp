using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace NorthPoleApp.Models
{
    public class EmployeeTaskDAL
    {
        //will be used to assign tasks to employees
        //and vice versa
        //CREATE
        public void CreateEmployeeTask(int employeeID, int taskID) 
        {
            using (var connect = new MySqlConnection(Secret.Connection)) 
            {
                string sql = $"insert into employeestasks values(0, {employeeID}, {taskID})";
                connect.Open();
                connect.Query<EmployeeTask>(sql);
                connect.Close();

            
            }

        }

        //used to delete employee task
        //either when reassigning or task no longer needed
        public void DeleteEmployeeTask(int id) 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"delete from employeestasks where id={id}";
                connect.Open();
                connect.Query<EmployeeTask>(sql);
                connect.Close();


            }
        }

        public void DeleteByTaskId(int taskId) 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"delete from employeestasks where taskid={taskId}";
                connect.Open();
                connect.Query<EmployeeTask>(sql);
                connect.Close();


            }

        }

        //not sure if I need a READ method
        //I believe that READ will be more userfull in employee and task DALs and pull a whole list not just one task/employee combo
        //TBD i guess
    }
}
