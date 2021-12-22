using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace NorthPoleApp.Models
{
    public class TaskDAL
    {
        //CREATE task
        //Santa will create a task once he reads a letter
        //note: when making controller/view will need to pass letter in via viewdata to populate gift ID and letter ID
        public void CreateTask(Task t) 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"insert into tasks values(0, \"{t.Name}\", \"{t.Description}\", '{t.CurrentStatus}', {t.GiftId}, {t.LetterId})";
                connect.Open();
                connect.Query<Task>(sql);
                connect.Close();
            }

        }

        //READ single task
        //mostly for elves to see details of task and update if needed
        public Task GetTask(int id) 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from tasks where id={id}";
                string employeesql = $"select * from employees inner join employeestasks on employees.id = employeestasks.employeeid where taskid = { id }";
                connect.Open();
                Task t = connect.Query<Task>(sql).First();
                t.Employees = connect.Query<Employee>(employeesql).ToList();
                connect.Close();

                return t;
            }
        }

        //READ all tasks for ALL employees or santa 
        //will be used for dashboard purposes for santa or employees
        public List<Task> GetTasks() 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = "select * from tasks";  
                connect.Open();
                List<Task> tasks = connect.Query<Task>(sql).ToList();
                foreach (Task t in tasks)
                {
                    string employeesql = $"select * from employees inner join employeestasks on employees.id = employeestasks.employeeid where taskid = {t.Id}";
                    t.Employees = connect.Query<Employee>(employeesql).ToList();
                }
                connect.Close();

                return tasks;
            }
        }

        //UPDATE TASK
        //elves will need to be able to update status of task
        public void UpdateTask(Task t) 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"update tasks set `name`=\"{t.Name}\", `description`=\"{t.Description}\", currentStatus='{t.CurrentStatus}', giftId={t.GiftId}, letterId={t.LetterId} where id={t.Id}";
                connect.Open();
                connect.Query<Task>(sql);
                connect.Close();
            }
        }

        //Delete Task
        //santa will be able to delete tasks upon delivery of items
        //unfortunately to get this to work and delete letter need to pass in task object
        //so that the letterID can be used to delete associated letter
        public void DeleteTask(Task t) 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"delete from tasks where id={t.Id}";

                //need to delete letter since letter no longer needed
                string lettersql = $"delete from letters where letterId = {t.LetterId}";
                connect.Open();
                connect.Query<Task>(sql);
                connect.Query<Letter>(lettersql);
                connect.Close();
            }
        }

        //need this in order to assigne employeestasks
        public int GetId(int letterID) 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from tasks where letterId={letterID}";

                connect.Open();
                int id = connect.Query<Task>(sql).First().Id;
                connect.Close();

                return id;
            }
        }
    }
}
