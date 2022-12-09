using API;
using GUI;

namespace MiniProject2_AssetTracking.Views
{
    public static class DisplayView
    {
        public static void Load()
        {
            Console.Clear();
           
            TEXT.Get.Message("Your Assets.").Show();

            Console.WriteLine();

            string format = "{0,-4}{1,-10}{2, -12}{3,-12}{4,-15}{5,-20}{6,-15}{7,-15}{8,-15}";
            string[] header = {"id", "Type", "Brand", "Model", "Office", "Purchase Date", "Price (EUR)", "Currency", "Local price" };
            TEXT.Get.Message(string.Format(format, header)).Show();

            foreach (var asset in Assets.GetAll.OrderBy(x => x.Office.Id).ThenBy(y => y.PurchaseDate))
            {
                string[] output = {
                    asset.Id.ToString(),
                    asset.Device.GetType().Name,
                    asset.Device.Brand,
                    asset.Device.Model,
                    asset.Office.Name,
                    asset.PurchaseDate.ToString("dd-MM-yyyy"),
                    asset.Price.ToString(),
                    asset.Office.Currency,
                    (asset.Price * Currency.GetRate(asset.Office.Currency))!.Value.ToString("0.##")
                };

                TEXT.Get.Message(string.Format(format, output)).Color(ValidateDate(asset.PurchaseDate)).Show();  
            }

            ShowMenu();
        }


        private static void ShowMenu()
        {
            Console.WriteLine();
            TEXT.Get.ListedMessage(
                "(1) Asset Menu.",
                "(2) Office Menu",
                "(ESC) Exit.").Color(ConsoleColor.White).Show();

            INPUT.AwaitKeyCommand(
                (new[] { ConsoleKey.D1, ConsoleKey.NumPad1 }, ShowAssetView),
                (new[] { ConsoleKey.D2, ConsoleKey.NumPad2 }, ShowOfficeView),
                (new[] { ConsoleKey.Escape }, () => {}));
        }

        private static void ShowAssetView() => AssetView.Load();

        private static void ShowOfficeView() => OfficeView.Load();

        static ConsoleColor ValidateDate(DateTime date)
        {
            if (date < DateTime.Now.AddYears(-2).AddMonths(-9))
            {
                return ConsoleColor.Red;
            }
            else if (date < DateTime.Now.AddYears(-2).AddMonths(-6))
            {
                return ConsoleColor.Yellow;
            }
            else
            {
                return ConsoleColor.White;
            }
        }

    }
}
