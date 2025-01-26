using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = @"Server=(localdb)\MSSQLLocalDB;Database=Warehouse;Integrated Security=True;";
        DatabaseManager databaseManager = new DatabaseManager(connectionString);
        MenuManager menuManager = new MenuManager(databaseManager);

        menuManager.ShowMenu();
    }
}