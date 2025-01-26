using System;
using System.Data.SqlClient;

class Program
{
    static SqlConnection connection; // Global connection

    static void Main()
    {
        string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Warehouse;Integrated Security=True;";
        bool exit = false;

        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Connect to the database");
            Console.WriteLine("2. Disconnect from the database");
            Console.WriteLine("3. Display all product information");
            Console.WriteLine("4. Display all product types");
            Console.WriteLine("5. Display all suppliers");
            Console.WriteLine("6. Show product with maximum quantity");
            Console.WriteLine("7. Show product with minimum quantity");
            Console.WriteLine("8. Show product with minimum cost price");
            Console.WriteLine("9. Show product with maximum cost price");
            Console.WriteLine("10. Show products by specified category");
            Console.WriteLine("11. Show products by specified supplier");
            Console.WriteLine("12. Show the oldest product in the warehouse");
            Console.WriteLine("13. Show average quantity per product type");
            Console.WriteLine("14. Exit");
            Console.Write("Your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ConnectToDatabase(connectionString);
                    break;
                case "2":
                    DisconnectFromDatabase();
                    break;
                case "3":
                    DisplayAllProductInfo();
                    break;
                case "4":
                    DisplayAllProductTypes();
                    break;
                case "5":
                    DisplayAllSuppliers();
                    break;
                case "6":
                    ShowProductWithMaxQuantity();
                    break;
                case "7":
                    ShowProductWithMinQuantity();
                    break;
                case "8":
                    ShowProductWithMinCostPrice();
                    break;
                case "9":
                    ShowProductWithMaxCostPrice();
                    break;
                case "10":
                    ShowProductsByCategory();
                    break;
                case "11":
                    ShowProductsBySupplier();
                    break;
                case "12":
                    ShowOldestProduct();
                    break;
                case "13":
                    ShowAverageQuantityByProductType();
                    break;
                case "14":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void ConnectToDatabase(string connectionString)
    {
        if (connection == null)
        {
            connection = new SqlConnection(connectionString);
        }

        if (connection.State == System.Data.ConnectionState.Closed)
        {
            try
            {
                connection.Open();
                Console.WriteLine("Successfully connected to the database!");
                Console.WriteLine($"Server: {connection.DataSource}");
                Console.WriteLine($"Database: {connection.Database}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("You are already connected to the database.");
        }
    }

    static void DisconnectFromDatabase()
    {
        if (connection != null && connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
            Console.WriteLine("Disconnected from the database.");
        }
        else
        {
            Console.WriteLine("Connection is already closed or was not established.");
        }
    }
    /////
    static void DisplayAllProductInfo()
    {
        ExecuteAndPrintQuery("SELECT * FROM Products");
    }

    static void DisplayAllProductTypes()
    {
        ExecuteAndPrintQuery("SELECT * FROM ProductTypes");
    }

    static void DisplayAllSuppliers()
    {
        ExecuteAndPrintQuery("SELECT * FROM Suppliers");
    }

    static void ShowProductWithMaxQuantity()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 * FROM Products ORDER BY Quantity DESC");
    }

    static void ShowProductWithMinQuantity()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 * FROM Products ORDER BY Quantity ASC");
    }

    static void ShowProductWithMinCostPrice()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 * FROM Products ORDER BY CostPrice ASC");
    }

    static void ShowProductWithMaxCostPrice()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 * FROM Products ORDER BY CostPrice DESC");
    }
    /////
    static void ShowProductsByCategory()
    {
        Console.Write("Enter category ID: ");
        string categoryId = Console.ReadLine();
        ExecuteAndPrintQuery($"SELECT * FROM Products WHERE TypeID = {categoryId}");
    }

    static void ShowProductsBySupplier()
    {
        Console.Write("Enter supplier ID: ");
        string supplierId = Console.ReadLine();
        ExecuteAndPrintQuery($"SELECT * FROM Products WHERE SupplierID = {supplierId}");
    }

    static void ShowOldestProduct()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 * FROM Products ORDER BY SupplyDate ASC");
    }

    static void ShowAverageQuantityByProductType()
    {
        ExecuteAndPrintQuery("SELECT TypeID, AVG(Quantity) AS AverageQuantity FROM Products GROUP BY TypeID");
    }
    /////
    static void ExecuteAndPrintQuery(string query)
    {
        if (connection == null || connection.State == System.Data.ConnectionState.Closed)
        {
            Console.WriteLine("Database connection is not established.");
            return;
        }

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Console.Write(reader[i] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}