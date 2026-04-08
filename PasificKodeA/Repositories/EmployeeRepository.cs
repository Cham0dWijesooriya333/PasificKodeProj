using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using PasificKodeA.Models;
using Microsoft.Extensions.Configuration;

namespace PasificKodeA.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        // -------------------- CREATE --------------------
        public void Add(Employee emp)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "INSERT INTO Employees (FirstName, LastName, Email, DateOfBirth, Salary, DepartmentId) " +
                "VALUES (@FirstName, @LastName, @Email, @DOB, @Salary, @DeptId)", con);

            cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmd.Parameters.AddWithValue("@LastName", emp.LastName);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@DOB", emp.DateOfBirth);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@DeptId", emp.DepartmentId);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // -------------------- READ --------------------
        public List<Employee> GetAll()
        {
            var list = new List<Employee>();
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM Employees", con);
            con.Open();
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var emp = MapReaderToEmployee(reader);
                list.Add(emp);
            }

            return list;
        }

        public Employee GetById(int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM Employees WHERE EmployeeId=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
                return MapReaderToEmployee(reader);

            return null;
        }

        // -------------------- UPDATE --------------------
        public void Update(Employee emp)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand(
                "UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, Email=@Email, " +
                "DateOfBirth=@DOB, Salary=@Salary, DepartmentId=@DeptId WHERE EmployeeId=@Id", con);

            cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
            cmd.Parameters.AddWithValue("@LastName", emp.LastName);
            cmd.Parameters.AddWithValue("@Email", emp.Email);
            cmd.Parameters.AddWithValue("@DOB", emp.DateOfBirth);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@DeptId", emp.DepartmentId);
            cmd.Parameters.AddWithValue("@Id", emp.EmployeeId);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // -------------------- DELETE --------------------
        public void Delete(int id)
        {
            using var con = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("DELETE FROM Employees WHERE EmployeeId=@Id", con);
            cmd.Parameters.AddWithValue("@Id", id);

            con.Open();
            cmd.ExecuteNonQuery();
        }

        // -------------------- HELPER: Map SqlDataReader to Employee --------------------
        private Employee MapReaderToEmployee(SqlDataReader reader)
        {
            var emp = new Employee
            {
                EmployeeId = (int)reader["EmployeeId"],
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString(),
                Email = reader["Email"].ToString(),
                DateOfBirth = (DateTime)reader["DateOfBirth"],
                Salary = (decimal)reader["Salary"],
                DepartmentId = (int)reader["DepartmentId"]
            };

            // Age is computed by the Employee model property, no need to set here
            return emp;
        }
    }
}