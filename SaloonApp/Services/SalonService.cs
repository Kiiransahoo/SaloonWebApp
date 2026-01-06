using SaloonApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace SaloonApp.Services
{
    public class SalonService
    {
        private readonly string _connectionString;

        public SalonService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void AddSalon(Salon salon)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("AddSalon", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Name", salon.Name);
            cmd.Parameters.AddWithValue("@SalonType", salon.SalonType);   // NEW

            cmd.Parameters.AddWithValue("@Address", salon.Address);
            cmd.Parameters.AddWithValue("@PinCode", salon.PinCode);
            cmd.Parameters.AddWithValue("@Phone", salon.Phone);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        public List<Salon> GetSalonsByPin(string pin)
        {
            var salons = new List<Salon>();

            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("GetSalonsByPin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PinCode", pin);

            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                salons.Add(new Salon
                {
                    SalonId = (int)reader["SalonId"],
                    Name = reader["Name"].ToString(),
                    Address = reader["Address"].ToString(),
                    PinCode = reader["PinCode"].ToString(),
                    Phone = reader["Phone"].ToString()
                });
            }

            return salons;
        }
    }
}

