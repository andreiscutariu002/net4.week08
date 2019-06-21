using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=.;Initial Catalog=Library;Integrated Security=True";

            SqlConnection connection = new SqlConnection
            {
                ConnectionString = connectionString
            };

            SelectStatemets(connection);
            InsertNewBook(connection, "Book 1", 2, 2019, 100);
        }

        private static void SelectStatemets(SqlConnection connection)
        {
            try
            {
                connection.Open();

                var selectString = "select * from Author";
                selectString += string.Format($" where Name = @nume;" +
                                              $"SELECT CAST(scope_identity() AS int)");

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };

                SqlParameter nameParameter = new SqlParameter();

                nameParameter.DbType = DbType.String;
                nameParameter.ParameterName = "nume";
                nameParameter.Value = "Misu";

                command.Parameters.Add(nameParameter);

                var returnedValue = command.ExecuteScalar();

                Console.WriteLine(returnedValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Dispose();
            }
        }


        private static void InsertNewBook(SqlConnection connection, string title, int publisherId, int year, int price)
        {
            try
            {
                connection.Open();

                var selectString = @"
                        INSERT INTO [dbo].[Book]
                                   ([Title]
                                   ,[PublisherId]
                                   ,[Year]
                                   ,[Price])
                             VALUES
                                   (@title
                                   ,@publisherId
                                   ,@year
                                   ,@price);
                SELECT CAST(scope_identity() AS int)";

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = selectString
                };

                SqlParameter titleSqlParameter = new SqlParameter
                {
                    DbType = DbType.String,
                    ParameterName = "title",
                    Value = title
                };

                SqlParameter publisherIdSqlParameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "publisherId",
                    Value = publisherId
                };

                SqlParameter yearParameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "year",
                    Value = year
                };

                SqlParameter priceParameter = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "price",
                    Value = price
                };

                command.Parameters.Add(titleSqlParameter);
                command.Parameters.Add(publisherIdSqlParameter);
                command.Parameters.Add(yearParameter);
                command.Parameters.Add(priceParameter);

                var returnedValue = command.ExecuteScalar();

                Console.WriteLine(returnedValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}
