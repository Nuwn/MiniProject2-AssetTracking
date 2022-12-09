using API;
using MiniProject2_AssetTracking.Views;

namespace MiniProject2_AssetTracking.App
{
    internal class App
    {
        public App() {
            Currency.LoadRates();

            DisplayView.Load();
        }
    }
}

// Query the database for the assets


//var assets = AppDBContext.Get.Assets.ToList();








//// Load all currencies, cache it.
//Currency.LoadRates();

//// Check that we have loaded the data we need.
//foreach (var office in Office.GetAllOffices)
//{
//    static void Exit()
//    {
//        Console.WriteLine("Unable to load currencies, please restart.");
//        Environment.Exit((int)ExitCode.InvalidRates);
//    }

//    var currency = Office.GetCurrency(office);
//    if (currency == null || Currency.GetRate(currency) == null)
//    {
//        Exit();
//        break;
//    }
//}

//// Create list and added mock data.
//List<Asset> assets = new()
//{
//    new Asset(new Computer("HP", "Elitebook"), Office.OfficeType.London, new DateTime(2020,02,15), 399m),
//    new Asset(new Mobile("IPhone", "8"), Office.OfficeType.Oslo, new DateTime(2019,07,15), 999.90m),
//    new Asset(new Mobile("Samsung", "S10"), Office.OfficeType.Tokyo, new DateTime(2021,05,15), 500.50m, "DKK")
//};

//const int padding = 15;
//// String format for output
//string format = $$"""{0, -{{padding}}}{1, -{{padding}}}{2, -{{padding}}}{3, -{{padding}}}{4, -{{padding}}}{5, -{{padding}}}{6, -{{padding}}}{7, -{{padding}}}""";
//// Header
//string[] header = { "Type", "Brand", "Model", "Office", "Purchase Date", "Price (EUR)", "Currency", "Local price today" };
//string[] space = header.Select(x1 => new string(x1.Select(x2 => '-').ToArray())).ToArray();

//// resize the console, works only on windows, so we can see the proper format
//Console.SetWindowSize(string.Format(format, header).Length + 10, 40);

//static string[] GetRecord(Asset asset)
//{
//    string type = asset.Technology.GetCategory;
//    string brand = asset.Technology.Brand;
//    string model = asset.Technology.Model;
//    string office = asset.Office.ToString();
//    string p_date = asset.PurchaseDate.ToString("yyyy-MM-dd");
//    string price = asset.Price.ToString();
//    string currency = asset.Currency;
//    string localPrice = (asset.Price * Currency.GetRate(currency)!.Value).ToString("0.00");

//    return new string[] { type, brand, model,  office, p_date, price, currency, localPrice };
//}

//void OutputAllRecords()
//{
//    // Print headers
//    Console.WriteLine(string.Format(format, header));
//    Console.WriteLine(string.Format(format, space));

//    assets.OrderBy(x => x.Technology.GetCategory)
//        .ThenBy(x => x.PurchaseDate)
//        .ToList()
//        .ForEach(x => {
//            // color check
//            if(x.PurchaseDate < DateTime.Now.AddYears(-2).AddMonths(-9))
//            {
//                Console.ForegroundColor = ConsoleColor.Red;
//            }
//            else if(x.PurchaseDate < DateTime.Now.AddYears(-2).AddMonths(-6))
//            {
//                Console.ForegroundColor = ConsoleColor.Yellow;
//            }
//            else
//            {
//                Console.ForegroundColor = ConsoleColor.White;
//            }

//            Console.WriteLine(string.Format(format, GetRecord(x)));

//            Console.ForegroundColor = ConsoleColor.Gray;
//         });
//}

//// for testing and a means to stop the loop
//bool isDone = false;
//while (!isDone)
//{
//    // display list
//    OutputAllRecords();

//    Console.WriteLine("\nWould you like to add new assets? (Y/N)");
//    ConsoleKeyInfo input = Console.ReadKey();
//    Console.WriteLine();

//    if(input.Key == ConsoleKey.Y)
//    {
//        // start building a new input
//        CreateNewAsset();
//    }
//    else if(input.Key == ConsoleKey.N)
//    {
//        Console.WriteLine("Exiting program.");
//        isDone = true;
//    }
//}

//void CreateNewAsset()
//{
//    Asset asset = new Asset();

//    Console.Write("\nCreate new (C: Computer, M: Mobile): ");

//    ConsoleKeyInfo key = Console.ReadKey();
//    asset.Technology = (key.Key == ConsoleKey.C) ? 
//        new Computer() : (key.Key == ConsoleKey.M) ? 
//        new Mobile() : new Computer();

//    Console.WriteLine($"\nCreated new {asset.Technology.GetCategory}!");

//    Console.Write("\nEnter brand: ");

//    asset.Technology.Brand = Console.ReadLine() ?? "";

//    Console.Write("\nEnter model: ");

//    asset.Technology.Model = Console.ReadLine() ?? "";

//    Console.Write("\nPurchase date (yyyy-mm-dd): ");

//    bool isValidDate = false;
//    while (!isValidDate)
//    {
//        string? date = Console.ReadLine();
//        if (DateTime.TryParse(date, out DateTime time))
//        {
//            asset.PurchaseDate = time;
//            isValidDate = true;
//        }
//    }

//    Console.Write("\nPrice (e.g 199,99): ");

//    bool isDecimal = false;
//    while (!isDecimal)
//    {
//        if(decimal.TryParse(Console.ReadLine(), out decimal price))
//        {
//            asset.Price = price;
//            isDecimal = true;
//        }
//    }

//    Console.Write("Purchase in currency (" + string.Join(", ", Currency.GetCurrencies) + "): ");

//    bool isValidCurrency = false;
//    while (!isValidCurrency)
//    {
//        string? currency = Console.ReadLine();
//        if (Currency.GetCurrencies.Contains(currency))
//        {
//            asset.Currency = currency!;
//            isValidCurrency = true;
//        }
//    }

//    string[] offices = Office.GetNames;
//    var s = string.Join("; ", offices.Select((d, i) => $"{i} = {d.Trim()}" ).ToArray());

//    Console.Write($"\nOffice ({s}): ");

//    bool isValidOffice = false;
//    while(!isValidOffice)
//    {
//        if (int.TryParse(Console.ReadLine(), out int index))
//        {
//            if(Enum.IsDefined(typeof(Office.OfficeType), index))
//            {
//                asset.Office = (Office.OfficeType)index;
//                isValidOffice = true;
//            }
//        }
//    }

//    assets.Add(asset);
//    Console.WriteLine("Asset created! press any key to continue.");
//    Console.ReadKey();
//}

