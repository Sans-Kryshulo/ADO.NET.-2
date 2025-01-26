using System;

public class MenuManager
{
    private readonly DatabaseManager dbManager;

    public MenuManager(DatabaseManager databaseManager)
    {
        dbManager = databaseManager;
    }

    public void ShowMenu()
    {
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
                    dbManager.Connect();
                    break;
                case "2":
                    dbManager.Disconnect();
                    break;
                case "3":
                    dbManager.DisplayAllProductInfo();
                    break;
                case "4":
                    dbManager.DisplayAllProductTypes();
                    break;
                case "5":
                    dbManager.DisplayAllSuppliers();
                    break;
                case "6":
                    dbManager.ShowProductWithMaxQuantity();
                    break;
                case "7":
                    dbManager.ShowProductWithMinQuantity();
                    break;
                case "8":
                    dbManager.ShowProductWithMinCostPrice();
                    break;
                case "9":
                    dbManager.ShowProductWithMaxCostPrice();
                    break;
                case "10":
                    Console.Write("Enter category ID: ");
                    int categoryId = int.Parse(Console.ReadLine());
                    dbManager.ShowProductsByCategory(categoryId);
                    break;
                case "11":
                    Console.Write("Enter supplier ID: ");
                    int supplierId = int.Parse(Console.ReadLine());
                    dbManager.ShowProductsBySupplier(supplierId);
                    break;
                case "12":
                    dbManager.ShowOldestProduct();
                    break;
                case "13":
                    dbManager.ShowAverageQuantityByProductType();
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
}