using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace NorthPoleApp.Models
{
    public class LetterDAL
    {
        //CREATE
        //to be used for people who wish to put in a request to santa
        //by default isgood and giftid will = null as they are TBD by santa
        public void CreateLetter(Letter l) 
        {
            using (var connect = new MySqlConnection(Secret.Connection)) 
            {
                string sql = $"insert into letters values(0, '{l.Name}', {l.Age}, '{l.City}', '{l.Country}', \"{l.Note}\", null, {l.GiftId}, 0)";
                connect.Open();
                connect.Query<Letter>(sql);
                connect.Close();
            }
        }

        //UPDATE
        //to be used by SANTA to add "isGood" and assign a gift to the user
        public void UpdateLetter(Letter l) 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"update letters set `name`='{l.Name}', age={l.Age}, city='{l.City}', country='{l.Country}', note=\"{l.Note}\", giftId={l.GiftId}, isGood={l.IsGood}, isReviewed={l.IsReviewed} where letterId={l.LetterId}";
                connect.Open();
                connect.Query<Letter>(sql);
                connect.Close();
            }
        }

        //READ
        //to be used by either santa to read one letter by id
        public Letter GetLetter(int id) 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from letters where letterid={id}";
                connect.Open();
                Letter l = connect.Query<Letter>(sql).FirstOrDefault();
                connect.Close();

                return l;
            }

        }

        //READ
        //to be used by santa to see list of all letters
        public List<Letter> GetLetters() 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from letters";
                connect.Open();
                List<Letter> l = connect.Query<Letter>(sql).ToList();
                connect.Close();

                return l;
            }

        }

        //Note: DELETE functionality not needed here
        //since letterID is unique ID in Task Model will delete 
        //letters when the task is complete and deleted. 

    }
}
