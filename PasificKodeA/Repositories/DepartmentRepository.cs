using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PasificKodeA.Models;

namespace PasificKodeA.Repositories
{
    public class DepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public List<Department> GetAll()
        {
            var list = new List<Department>();
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM Departments", con);
            con.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Department
                {
                    DepartmentId = (int)reader["DepartmentId"],
                    DepartmentCode = reader["DepartmentCode"].ToString(),
                    DepartmentName = reader["DepartmentName"].ToString()
                });
            }

            return list;
        }

        public void Add(Department dept)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "INSERT INTO Departments (DepartmentCode, DepartmentName) VALUES (@Code, @Name)", con);
            cmd.Parameters.AddWithValue("@Code", dept.DepartmentCode);
            cmd.Parameters.AddWithValue("@Name", dept.DepartmentName);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void Update(Department dept)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "UPDATE Departments SET DepartmentCode=@Code, DepartmentName=@Name WHERE DepartmentId=@Id", con);
            cmd.Parameters.AddWithValue("@Code", dept.DepartmentCode);
            cmd.Parameters.AddWithValue("@Name", dept.DepartmentName);
            cmd.Parameters.AddWithValue("@Id", dept.DepartmentId);
            con.Open();
            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("DELETE FROM Departments WHERE DepartmentId=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
