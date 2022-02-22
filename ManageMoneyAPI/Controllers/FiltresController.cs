using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ManageMoneyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FiltresController : ControllerBase
    {
        private readonly ILogger<FiltresController> _logger;

        private SqlConnection conn = ConnectDB.Connect();

        public FiltresController(ILogger<FiltresController> logger)
        {
            _logger = logger;
        }
        [HttpGet("Filter by Date")]
        public string GetTransactionByDate(DateTime date)
        {
            string Info = String.Empty;
            if (date != DateTime.MinValue)
            {
                using (SqlCommand comm = new SqlCommand($"SELECT * FROM [Money_Less]WHERE [Date]={date}", conn))
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
                using (SqlCommand comm = new SqlCommand($"SELECT * FROM [Money_Income]WHERE [Date]={date}", conn))
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
            }
            return Info;
        }
        [HttpGet("Filter by expenses")]
        public string GetTransactionsByExpenses(float min,float max)
        {
            string Info = String.Empty;
            if (min>0 && max>0)
            {
                using (SqlCommand comm = new SqlCommand($"SELECT * FROM [Money_Less]WHERE [Expense]>{min} AND [Expense]<{max}", conn))
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
                using (SqlCommand comm = new SqlCommand($"SELECT * FROM [Money_Income]WHERE [Income]>{min} AND [Income]<{max}", conn))
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
            }
            return Info;
        }
        [HttpGet("Filter by Category ID")]
        public string GetTransactionsByCategory_ID(int category_ID)
        {
            string Info = String.Empty;
            if (category_ID>-1)
            {
                using (SqlCommand comm = new SqlCommand($"SELECT * FROM [Money_Less]WHERE [Category_ID]={category_ID}", conn))
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
                using (SqlCommand comm = new SqlCommand($"SELECT * FROM [Money_Income]WHERE [Category_ID]={category_ID}", conn))
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
            }
            return Info;
        }
    }
}
