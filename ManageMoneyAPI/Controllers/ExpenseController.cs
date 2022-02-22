using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
namespace ManageMoneyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;

        private SqlConnection conn = ConnectDB.Connect();

        public ExpenseController(ILogger<ExpenseController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllExpense")]
        public string Expenses()
        {
            string Info = String.Empty;
            using (SqlCommand comm = new SqlCommand("SELECT * FROM [Money_Less]", conn))
            {
                using (SqlDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Info += $"ID - {reader["ID"]}\nCategory_ID - {reader["Category_ID"]}\n" +
                            $"Expense - {reader["Expense"]}\nDate - {reader["Date"]}";
                    }
                }
            }
            return Info;
        }
        [HttpPost("AddExpense")]
        public StatusCodeResult AddExpense(int Category, float expense_money, DateTime date)
        {
            if (Category != 0 && expense_money != 0 && date != DateTime.MinValue)
            {
                using (SqlCommand comm = new SqlCommand($"INSERT INTO [Money_Less]VALUES({Category},{expense_money},'{date.ToString("yyyy-MM-dd")}')", conn))
                {
                    try
                    {
                        comm.ExecuteNonQuery();
                        return StatusCode(200);
                    }
                    catch (Exception)
                    {

                        return StatusCode(500);
                    }
                }
            }
            else
            {
                return StatusCode(400);
            }
        }
        [HttpDelete("Delete expense by ID")]
        public StatusCodeResult DeleteExpenseByID(int id)
        {
            if (id > -1)
            {
                using (SqlCommand comm = new SqlCommand($"DELETE FROM [Money_Less]WHERE [ID]={id}", conn))
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
        [HttpDelete("Delete expense by Category")]
        public StatusCodeResult DeleteExpenseByCategory(int categoryID)
        {
            if (categoryID > -1)
            {
                using (SqlCommand comm = new SqlCommand($"DELETE FROM [Money_Less]WHERE [Category_ID]={categoryID}", conn))
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
        [HttpDelete("Delete expense by Expense")]
        public StatusCodeResult DeleteExpenseByExpense(float expense)
        {
            if (expense > 0)
            {
                using (SqlCommand comm = new SqlCommand($"DELETE FROM [Money_Less]WHERE [Expense]={expense}", conn))
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
        [HttpDelete("Delete expense by Date")]
        public StatusCodeResult DeleteExpenseByDate(DateTime date)
        {
            if (date != DateTime.MinValue)
            {
                using (SqlCommand comm = new SqlCommand($"DELETE FROM [Money_Less]WHERE [Date]={date}", conn))
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
