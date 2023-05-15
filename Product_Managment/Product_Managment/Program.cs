using System.Data.SqlClient;
using Spectre.Console;

namespace Product_Managment
{
    public class product_Managment
    {
        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection("server=IN-333K9S3;database=product_managment_App;Integrated Security = true");
            con.Open();
            return con;
        }
        public void Add_Product()
        {
            SqlConnection con = GetConnection();
            string query = $"insert into Product_Managment values(@Name,@Brand,@Quantity,@Price)";
            SqlCommand cmd = new SqlCommand(query, con);

            string name = AnsiConsole.Ask<string>("[yellow]Enter Name:[/]");
            string brand = AnsiConsole.Ask<string>("[yellow]Enter Brand:[/]");
            decimal quantity = AnsiConsole.Ask<decimal>("[yellow]Enter Quantity:[/]");
            decimal price = AnsiConsole.Ask<decimal>("[yellow]Enter Price:[/]");

            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Brand", brand);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@Price", price);

            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[rgb(124,211,76)]Product Added Sucessfully [/]");

            con.Close();
        }

        public void View_Product()
        {
            SqlConnection con = GetConnection();
            string id = AnsiConsole.Ask<string>("[yellow]Enter Product ID you Want to get: [/]");
            string query = $"select * from Product_Managment where ID = {id}";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Brand");
            table.AddColumn("Quantity");
            table.AddColumn("Price");
            table.Title("[underline rgb(131,111,255)]PRODUCT DETAILS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["ID"].ToString(), reader["Name"].ToString(), reader["Brand"].ToString(), reader["Quantity"].ToString(), reader["Price"].ToString());
            }

            AnsiConsole.Write(table);
            con.Close();

        }
        public void View_All_Products()
        {
            SqlConnection con = GetConnection();
            string query = "select * from Product_Managment";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Brand");
            table.AddColumn("Quantity");
            table.AddColumn("Price");
            table.Title("[underline rgb(131,111,255)]PRODUCTS DETAILS[/]");
            table.BorderColor(Color.LightSlateGrey);
            foreach (var column in table.Columns)
            {
                column.Centered();
            }

            while (reader.Read())
            {
                table.AddRow(reader["ID"].ToString(), reader["Name"].ToString(), reader["Brand"].ToString(), reader["Quantity"].ToString(), reader["Price"].ToString());
            }

            AnsiConsole.Write(table);

            con.Close();
        }
        public void update_Product()
        {
            SqlConnection con = GetConnection();
            int id = AnsiConsole.Ask<int>("Enter the Product ID you want to update:");
            string query = $"update Product_Managment set Name=@Name,Brand=@Brand,Quantity=@Quantity,Price=@Price where ID = {id}";
            SqlCommand cmd = new SqlCommand(query, con);

            string Name = AnsiConsole.Ask<string>("[yellow]Enter Updated Name:[/]");
            string Brand = AnsiConsole.Ask<string>("[yellow]Enter Updated Brand:[/]");
            decimal Quantity = AnsiConsole.Ask<decimal>("[yellow]Enter Updated Quantity:[/]");
            decimal Price = AnsiConsole.Ask<decimal>("[yellow]Enter Updated Price:[/]");

            cmd.Parameters.AddWithValue("@Name", Name);
            cmd.Parameters.AddWithValue("@Brand", Brand);
            cmd.Parameters.AddWithValue("@Quantity", Quantity);
            cmd.Parameters.AddWithValue("@Price", Price);

            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[rgb(124,211,76)]Product Updated Sucessfully [/]");
            con.Close();
        }

        public void Delete_Product()
        {
            SqlConnection con = GetConnection();
            int id = AnsiConsole.Ask<int>("[yellow]Enter the product id you want to Delete:[/]");
            string query = $"delete from Product_Managment where ID = {id}";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.ExecuteNonQuery();

            AnsiConsole.MarkupLine("[rgb(124,211,76)]Product Deleted Sucessfully [/]");
            con.Close();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Write(new FigletText("Product Managment App").Centered().Color(Color.SlateBlue1));
            product_Managment product = new product_Managment();
            while (true)
            {
                Console.WriteLine();
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[green]Select your choice :[/]")
                    .AddChoices(new[] {
                        "Add Product", "View Product By ID", "View All Products",
                        "Update Product By ID","Delete Product By ID"
                    }));

                switch (choice)
                {
                    case "Add Product":
                        {
                            product.Add_Product();
                            break;
                        }
                    case "View Product By ID":
                        {
                            product.View_Product();
                            break;
                        }
                    case "View All Products":
                        {
                            product.View_All_Products();
                            break;
                        }
                    case "Update Product By ID":
                        {
                            product.update_Product();
                            break;
                        }
                    case "Delete Product By ID":
                        {
                            product.Delete_Product();
                            break;
                        }
                }
            }
        }
    }
}