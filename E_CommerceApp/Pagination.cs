using E_CommerceApp.Format;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace E_CommerceApp
{
    public class Pagination : IPagination
    {
        //private static int _instance = 0;
        private static int _page = 0;

        private readonly DataOperations _operations = new DataOperations();

        public List<Product> PageData { get; set; }

        /// <summary>
        /// Handles the pagination to the next page of data table on the Cart page
        /// </summary>
        /// <param name="range">Range of amount of rows to show</param>
        public void NextPage(int range)
        {
            if (_page > 0)
            {
                if (PageData.Count <= 0)
                {
                    return;
                }
            }

            _page++;

            var start = (_page - 1) * range;
            List<Product> theData = new List<Product>();
            _operations.Conn.Open();

            var sqlString = "SELECT * FROM [dbo].[Product] ORDER BY ProductId OFFSET " + start + " ROWS FETCH NEXT " + range + " ROWS ONLY";

            SqlCommand command = new SqlCommand(sqlString, _operations.Conn);
            //SqlDataReader reader = await command.ExecuteReaderAsync();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    theData.Add(new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        CategoryId = reader.GetInt32(2),
                        CostPrice = reader.GetDecimal(3),
                        DateAdded = reader.GetDateTime(4)
                    });
                }
                PageData = theData;
            }

            reader.Close();
            _operations.Conn.Close();
        }

        /// <summary>
        /// Handles the pagination to the previous page of data table on the Cart page
        /// </summary>
        /// <param name="range">Range of amount of rows to show</param>
        public void PrevPage(int range)
        {
            if (_page == 0)
            {
                return;
            }

            var start = (_page - 1) * range;
            _page--;
            List<Product> theData = new List<Product>();
            _operations.Conn.Open();

            var sqlString = "SELECT * FROM [dbo].[Product] ORDER BY ProductId OFFSET " + start + " ROWS FETCH NEXT " + range + " ROWS ONLY";

            SqlCommand command = new SqlCommand(sqlString, _operations.Conn);
            //SqlDataReader reader = await command.ExecuteReaderAsync();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    theData.Add(new Product()
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        CategoryId = reader.GetInt32(2),
                        CostPrice = reader.GetDecimal(3),
                        DateAdded = reader.GetDateTime(4)
                    });
                }
                PageData = theData;
            }

            reader.Close();
            _operations.Conn.Close();
        }
    }
}