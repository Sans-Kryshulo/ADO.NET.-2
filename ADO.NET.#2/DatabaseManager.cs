using System;
using System.Data.SqlClient;

public class DatabaseManager
{
    private SqlConnection connection;
    private readonly string connectionString;

    public DatabaseManager(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public void Connect()
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

    public void Disconnect()
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

    public void ExecuteAndPrintQuery(string query)
    {
        /*
        if (connection == null || connection.State == System.Data.ConnectionState.Closed)
        {
            Console.WriteLine("Database connection is not established.");
            return;
        }

        try
        {
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
        catch (SqlException ex)
        {
            Console.WriteLine("SQL query error: " + ex.Message);
        }
        */
        using (var cmd = new SqlCommand(query, connection))
        using (var reader = cmd.ExecuteReader())
        {
            if (reader.HasRows) //reader.HasRows = true - є якісь рядки
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
            else // reader.HasRows = false - немає рядків
            {
                Console.WriteLine("No data found.");
            }
        }
    }

    public void ExecuteUserQuery()
    {
        Console.Write("Enter your SQL query: ");
        string query = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(query))
        {
            Console.WriteLine("Query cannot be empty.");
            return;
        }

        if (ContainsUnsafeSql(query))
        {
            Console.WriteLine("Unsafe SQL query detected. Execution is denied.");
            return;
        }

        try
        {
            ExecuteAndPrintQuery(query);
        }
        catch (SqlException ex)
        {
            Console.WriteLine("SQL execution error: " + ex.Message);
        }
    }

    private bool ContainsUnsafeSql(string query)
    {
        string lowerQuery = query.ToLower();
        //"Небезпечні" або "небажані" команди
        string[] forbiddenKeywords = 
        {
            "drop", "delete", "truncate", "alter", "update", "insert",
            "exec", "execute", "--", ";--", "xp_", "sp_", "shutdown"
        };

        foreach (string keyword in forbiddenKeywords)
        {
            if (lowerQuery.Contains(keyword))
            {
                return true;
            }
        }
        //SQL injection
        /*
        if (lowerQuery.Split(';').Length > 2)
        {
            return true;
        }

        return false;
        */
        return (lowerQuery.Split(';').Length > 2);
    }

    public void ShowProductsByCategory(int categoryId)
    {
        ExecuteAndPrintQuery($"SELECT * FROM Products WHERE TypeID = {categoryId}");
    }

    public void ShowProductsBySupplier(int supplierId)
    {
        ExecuteAndPrintQuery($"SELECT * FROM Products WHERE SupplierID = {supplierId}");
    }

    public void DisplayAllProductInfo()
    {
        ExecuteAndPrintQuery("SELECT * FROM Products");
    }
    public void DisplayAllProductTypes()
    {
        ExecuteAndPrintQuery("SELECT * FROM ProductTypes");
    }
    public void DisplayAllSuppliers()
    {
        ExecuteAndPrintQuery("SELECT * FROM Suppliers");
    }
    public void ShowProductWithMaxQuantity()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 * FROM Products ORDER BY Quantity DESC");
    }
    public void ShowProductWithMinQuantity()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 * FROM Products ORDER BY Quantity ASC");
    }
    public void ShowProductWithMinCostPrice()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 * FROM Products ORDER BY CostPrice ASC");
    }
    public void ShowProductWithMaxCostPrice()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 * FROM Products ORDER BY CostPrice DESC");
    }
    public void ShowOldestProduct()
    {
        ExecuteAndPrintQuery("SELECT TOP 1 * FROM Products ORDER BY SupplyDate ASC");
    }
    public void ShowAverageQuantityByProductType()
    {
        ExecuteAndPrintQuery("SELECT TypeID, AVG(Quantity) AS AverageQuantity FROM Products GROUP BY TypeID");
    }
}