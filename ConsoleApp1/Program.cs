using System;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice = "y";
            while (choice == "y")
            {
                int ch = Menu();
                switch (ch)
                {
                    case 1: GetEmployees(); break;
                    case 2:
                        {
                            Console.WriteLine("Enter Id");
                            int id = Convert.ToByte(Console.ReadLine());
                            Console.WriteLine("Enter Name");
                            string name = Console.ReadLine();
                            Console.WriteLine("Enter address");
                            string address = Console.ReadLine();
                            Console.WriteLine("Enter Salary");

                            int salary = Convert.ToInt16(Console.ReadLine());
                            Insert(id, name, address, salary);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter Id");
                            int id = Convert.ToByte(Console.ReadLine());
                            Console.WriteLine("Enter address");
                            string address = Console.ReadLine();
                            Console.WriteLine("Enter Salary");

                            int salary = Convert.ToInt16(Console.ReadLine());
                            Update(id, address, salary);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Enter Id");
                            int id = Convert.ToByte(Console.ReadLine());



                            Delete(id);
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Enter Id for which to search record");
                            int id = Convert.ToByte(Console.ReadLine());
                            GetEmployeeById(id);
                            break;
                        }
                }
                Console.WriteLine("DO you want to repeat");
                choice = Console.ReadLine();
            }
             
        }

        static int Menu()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Get List of Employees");
            Console.WriteLine("2. Insert");
            Console.WriteLine("3. Edit");
            Console.WriteLine("4. Delete");
            Console.WriteLine("5. Search");
            Console.WriteLine("Enter choice");
            int ch = Byte.Parse(Console.ReadLine());
            return ch;
        }
        static SqlConnection GetConnection()
        {
            return new SqlConnection(@"data source=adminvm\SQLEXPRESS;initial catalog=hmDB;user id=sa;password=pass@123");
            
        }
        static void GetEmployees()
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Select * from employee";
            command.Connection = connection;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader[0].ToString() + "  " + reader["name"] + " " + reader["address"] + " " + reader["salary"].ToString());
                }
                reader.Close();
                connection.Close();
            }
            else
                Console.WriteLine("No Records");
        }

        static void Insert(int id, string name, string address, int salary)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Insert into employee(id, name, address, salary) values(@id, @name,@address,@salary)";
            command.Connection = connection;
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@salary", salary);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }


        static void Update(int id,string address, int salary)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "update employee set address=@address, salary =@salary where id=@id";
            command.Connection = connection;
            command.Parameters.AddWithValue("@id", id);
             
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@salary", salary);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        static void Delete(int id)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Delete employee where id=@id";
            command.Connection = connection;
            command.Parameters.AddWithValue("@id", id);

            
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        static void GetEmployeeById(int id)
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand();
            command.CommandText = "Selec * from Employee where id=@id";
            command.Parameters.AddWithValue("@id", id);
            command.Connection = connection;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if(reader.HasRows)
            {
                reader.Read();
                Console.WriteLine(reader[0].ToString() + "  " + reader["name"] + " " + reader["address"] + " " + reader["salary"].ToString());
                reader.Close();
                connection.Close();
            }
           
            else
                Console.WriteLine("No Record with this ID");
        }
}
        }
     