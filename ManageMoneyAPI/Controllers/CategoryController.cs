using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ManageMoneyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;

        private SqlConnection conn = ConnectDB.Connect();

        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
        }
        [HttpGet("GetAllCategories")]
        public string GetCategories()
        {
            string Info = String.Empty;
            using (SqlCommand comm = new SqlCommand("SELECT * FROM [Category_Manager]", conn))
            {
                using (SqlDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Info += $"ID - {reader["ID"]}\nCategory_ID - {reader["Name"]}\n";
                    }
                }
            }
            return Info;
        }
        [HttpPost("Add Category")]
        public StatusCodeResult AddCategory(string name)
        {
            if (name.Length > 3)
            {
                using (SqlCommand comm = new SqlCommand($"INSERT INTO [Category_Manager]VALUES('{name}')", conn))
                {
                    if (comm.ExecuteNonQuery() > 0)
                    {
                        return StatusCode(200);
                    }
                    else
                    {
                        return StatusCode(400);
                    }
                }
            }
            else
            {
                return StatusCode(400);
            }
        }
        [HttpDelete("Delete category")]
        public StatusCodeResult DeleteCategoryByID(int id)
        {
            if (id > -1)
            {
                using (SqlCommand comm = new SqlCommand($"DELETE FROM [Category_Manager]WHERE [ID]={id}", conn))
                {
                    if (comm.ExecuteNonQuery() != 0)
                    {
                        return StatusCode(200);
                    }
                    else
                    {
                        return StatusCode(400);
                    }
                }
            }
            else
            {
                return StatusCode(400);
            }
        }
        [HttpDelete("Delete category by Name")]
        public StatusCodeResult DeleteCategoryByName(string name)
        {
            if (name.Length > 3)
            {
                using (SqlCommand comm = new SqlCommand($"DELETE FROM [Category_Manager]WHERE [Name]={name}", conn))
                {
                    if (comm.ExecuteNonQuery() != 0)
                    {
                        return StatusCode(200);
                    }
                    else
                    {
                        return StatusCode(400);
                    }
                }
            }
            else
            {
                return StatusCode(400);
            }
        }
    }

}
