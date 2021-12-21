using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace NorthPoleApp.Models
{
    public class GiftDAL
    {
        //we do not need an update gift option.
        //gift only has a name, no special features.
        //ONLY santa will be able to create/delete gifts
        //otherwise, gifts will be read only for populating lists

        //CREATE
        public void CreateGift(Gift g) 
        {
            using (var connect = new MySqlConnection(Secret.Connection)) 
            {
                string sql = $"insert into gifts values(0, '{g.GiftName}')";
                connect.Open();
                connect.Query<Gift>(sql);
                connect.Close();
            }
        
        }

        //DELETE
        public void DeleteGift(int id) 
        {
            using (var connect = new MySqlConnection(Secret.Connection)) 
            {
                string sql = $"delete from gifts where giftid={id}";
                connect.Open();
                connect.Query<Gift>(sql);
                connect.Close();
            }
        }

        //READ LIST<GIFT>
        public List<Gift> GetGifts() 
        {
            using (var connect = new MySqlConnection(Secret.Connection))
            {
                string sql = $"select * from gifts";
                connect.Open();
                List<Gift> gifts = connect.Query<Gift>(sql).ToList();
                connect.Close();

                return gifts;
            }
        }
    }
}
