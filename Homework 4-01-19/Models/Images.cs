using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Homework_4_01_19.Models
{
    public class Images
    {
        public string Password { get; set; }
        public string FileName { get; set; }
        public int id { get; set; }
        public string text { get; set; }
        public int Count;
    }

    public class ViewModel
    {
        public Images Image { get; set; }
        public bool IncorrectPassword { get; set; }
        public string Password { get; set; }   
    }

    public class PassWordManager
    {
        private string _connectionString;

        public PassWordManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public int AddImage(Images i)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "insert into images values (@filename,@password,@count) select SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@filename", i.FileName);
            cmd.Parameters.AddWithValue("@password", i.Password);
            cmd.Parameters.AddWithValue("@count", i.Count);
            conn.Open();
            return (int)(decimal)cmd.ExecuteScalar();
        } 
        
        public Images GetImage(int id, string hi)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from images where id=@id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            Images images = new Images();
            while(reader.Read())
            {
                images.FileName = (string)reader["FileName"];
                images.Password = (string)reader["Password"];
                images.id = (int)reader["id"];
                images.text = hi;
                images.Count = (int)reader["count"];
            }         
            return images;
        }

        public int Count(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select count from images where id= @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            int count = (int)cmd.ExecuteScalar();
            return count;
        }

        public int AddToCount(int count, int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            count++;
            cmd.CommandText = "update images set count=@count where id=@id";
            cmd.Parameters.AddWithValue("@id", id);   
            cmd.Parameters.AddWithValue("@count",count);
            conn.Open();
            cmd.ExecuteNonQuery();
            return count;
        }
    }
}