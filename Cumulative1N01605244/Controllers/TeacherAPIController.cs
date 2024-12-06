using Microsoft.AspNetCore.Mvc;
using Cumulative1N01605244.Models;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Cumulative1N01605244.Controllers
{
    [Route("api/Teacher")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        public TeacherAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListTeachers")]
        public List<Teacher> ListTeachers()
        {
            List<Teacher> Teachers = new List<Teacher>();
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "SELECT * FROM teachers";

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    while (ResultSet.Read())
                    {
                        Teachers.Add(new Teacher
                        {
                            Id = Convert.ToInt32(ResultSet["id"]),
                            Name = ResultSet["name"].ToString(),
                            Subject = ResultSet["subject"].ToString(),
                            HireDate = Convert.ToDateTime(ResultSet["hiredate"])
                        });
                    }
                }
            }
            return Teachers;
        }

        [HttpGet]
        [Route("FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher SelectedTeacher = null;
            using (MySqlConnection Connection = _context.AccessDatabase())
            {
                Connection.Open();
                MySqlCommand Command = Connection.CreateCommand();
                Command.CommandText = "SELECT * FROM teachers WHERE id=@id";
                Command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader ResultSet = Command.ExecuteReader())
                {
                    if (ResultSet.Read())
                    {
                        SelectedTeacher = new Teacher
                        {
                            Id = Convert.ToInt32(ResultSet["id"]),
                            Name = ResultSet["name"].ToString(),
                            Subject = ResultSet["subject"].ToString(),
                            HireDate = Convert.ToDateTime(ResultSet["hiredate"])
                        };
                    }
                }
            }
            return SelectedTeacher;
        }
    }
}

