using InformationAPI.interfaces;
using InformationAPI.Misc;
using InformationAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace InformationAPI.Impl
{
    public class ManageConnection : IManageConnection
    {
        private readonly string _connectionString;

        public ManageConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public InformationModel? GetById(int id)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM Posts WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new InformationModel
                    {
                        Id = (int)reader["Id"],
                        UserId = (int)reader["UserId"],
                        Title = reader["Title"].ToString(),
                        Body = reader["Body"].ToString()
                    };
                }
                return null;
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Failed to fetch data from database.", ex);
            }
        }

        public List<InformationModel> GetAll()
        {
            try
            {
                var list = new List<InformationModel>();
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                var cmd = new SqlCommand("SELECT * FROM Posts", conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new InformationModel
                    {
                        Id = (int)reader["Id"],
                        UserId = (int)reader["UserId"],
                        Title = reader["Title"].ToString(),
                        Body = reader["Body"].ToString()
                    });
                }

                return list;
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Failed to fetch data from database.", ex);
            }
        }

        public void Insert(InformationModel post)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                conn.Open();

                var cmd = new SqlCommand("INSERT INTO Posts (Id, UserId, Title, Body) VALUES (@id, @userId, @title, @body)", conn);
                cmd.Parameters.AddWithValue("@id", post.Id);
                cmd.Parameters.AddWithValue("@userId", post.UserId);
                cmd.Parameters.AddWithValue("@title", post.Title ?? "");
                cmd.Parameters.AddWithValue("@body", post.Body ?? "");

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Failed to insert new data into database.", ex);
            }
        }
    }
}
