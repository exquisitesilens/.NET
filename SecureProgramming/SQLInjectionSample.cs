using System;
using System.Data.SqlClient;

namespace SQLInjectionSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            doSecureCode();
        }

        private static void doSecureCode()
        {
            Console.WriteLine("Please enter a product ID:");
            string id = Console.ReadLine();

            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=localhost\\SQLEXPRESS;" +
                                   "Initial Catalog=Northwind;" +
                                   "Integrated Security=sspi";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Products WHERE ProductID = @id";
            cmd.Parameters.AddWithValue("@id", id); // param
            cmd.Connection = con;

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.Write("Product: ");
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i].ToString());
                        Console.Write(", ");
                    }
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}