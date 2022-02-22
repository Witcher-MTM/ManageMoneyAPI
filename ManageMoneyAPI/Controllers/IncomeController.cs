using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ManageMoneyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncomeController : ControllerBase
    {
        private readonly ILogger<IncomeController> _logger;

        private SqlConnection conn = ConnectDB.Connect();

        public IncomeController(ILogger<IncomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet("Get all Incomes")]
        public string AllIncomes()
        {
            string Info = String.Empty;
            using (SqlCommand comm = new SqlCommand("SELECT * FROM [Money_Income]", conn))
            {
                using (SqlDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Info += $"ID - {reader["ID"]}\nCategory_ID - {reader["Category_ID"]}\n" +
                            $"Income - {reader["Income"]}\nDate - {reader["Date"]}";
                    }
                }
            }
            return Info;
        }
        [HttpPost("Add income")]
        public StatusCodeResult AddIncome(int category_ID,float income , DateTime date)
        {
            if(category_ID>-1 && income>0 && date != DateTime.MinValue)
            {
                using (SqlCommand comm = new SqlCommand($"INSERT INTO [Money_Income]VALUES({category_ID},{income},'{date}')", conn))
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
        [HttpDelete("Delete Incomes")]
        public StatusCodeResult DeleteIncomeById(int id)
        {
            if (id > -1)
            {
                using (SqlCommand comm = new SqlCommand($"DELETE FROM [Money_Income]WHERE [ID]={id}", conn))
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
        [HttpDelete("Delete income by Category ID")]
        public StatusCodeResult DeleteIncomeByCategoryId(int category_id)
        {
            if (category_id > -1)
            {
                using (SqlCommand comm = new SqlCommand($"DELETE FROM [Money_Income]WHERE [Category_ID]={category_id}", conn))
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
        [HttpDelete("Delete income by Income")]
        public StatusCodeResult DeleteIncomeByIncome(float income)
        {
            if (income > -1)
            {
                using (SqlCommand comm = new SqlCommand($"DELETE FROM [Money_Income]WHERE [Income]={income}", conn))
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
        [HttpDelete("Delete income by Date")]
        public StatusCodeResult DeleteIncomeByDate(DateTime date)
        {
            if (date!=DateTime.MinValue)
            {
                using (SqlCommand comm = new SqlCommand($"DELETE FROM [Money_Income]WHERE [Date]={date}", conn))
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
