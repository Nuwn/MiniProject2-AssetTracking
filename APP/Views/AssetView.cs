using DataModels;
using GUI;


namespace MiniProject2_AssetTracking.Views
{
    public static class AssetView
    {
        public static void Load()
        {
            Console.WriteLine();
            TEXT.Get.ListedMessage(
                "(1) New Asset.",
                "(2) Edit Asset.",
                "(3) Delete Asset.",
                "(ESC) Return.").Show();

            INPUT.AwaitKeyCommand(
                (new[] { ConsoleKey.D1, ConsoleKey.NumPad1 }, NewAsset),
                (new[] { ConsoleKey.D2, ConsoleKey.NumPad2 }, EditAsset),
                (new[] { ConsoleKey.D3, ConsoleKey.NumPad3 }, DeleteAsset),
                (new[] { ConsoleKey.Escape }, () => DisplayView.Load()));
        }

        private static void NewAsset()
        {
            string[] offices = API.Offices.GetAll.Select(x => $"({x.Id}) {x.Name}").ToArray();

            if(offices.Length == 0)
            {
                OfficeView.NewOffice();
                return;
            }

            Asset asset = new Asset();

            TEXT.Get.Message("Device:").Show();
            TEXT.Get.Message("(1) Mobile | (2) Computer").Show();

            INPUT.AwaitKeyCommand(
                (new[] { ConsoleKey.D1, ConsoleKey.NumPad1 }, () => asset.Device = new Mobile()),
                (new[] { ConsoleKey.D2, ConsoleKey.NumPad2 }, () => asset.Device = new Computer()));


            TEXT.Get.Message("Brand:").Show();

            INPUT.AwaitTextInput((input) =>
            {
                asset.Device.Brand = input!;
            }, (input) => input != null);

            TEXT.Get.Message("Model:").Show();

            INPUT.AwaitTextInput((input) =>
            {
                asset.Device.Model = input!;
            }, (input) => input != null);

            TEXT.Get.Message("Price (EUR) (100,00):").Show();

            INPUT.AwaitTextInput((input) =>
            {
                asset.Price = decimal.Parse(input!);
            }, (input) => decimal.TryParse(input, out decimal result));

            TEXT.Get.Message("Purchase date (yyyy-mm-dd):").Show();

            INPUT.AwaitTextInput((input) =>
            {
                asset.PurchaseDate = DateTime.Parse(input!);
            }, (input) => DateTime.TryParse(input, out DateTime result));


            TEXT.Get.Message("Office:").Show();
            TEXT.Get.LinedMessage(offices).Show();

            INPUT.AwaitTextInput((input) =>
            {
                asset.Office = API.Offices.GetSingle(int.Parse(input!))!;
            }, (input) => int.TryParse(input, out int result) && API.Offices.Has(result));

            
            API.Assets.Add(asset);
            API.Action.Save();

            DisplayView.Load();
        }


        private static void EditAsset()
        {
            if (API.Assets.Count == 0)
                DisplayView.Load();

            TEXT.Get.Message("Select Asset to Edit (id):").Show();

            Asset? asset = default;

            INPUT.AwaitTextInput(
                (input) =>
                {
                    asset = API.Assets.GetSingle(int.Parse(input!))!;
                    if (asset == null)
                        DisplayView.Load();
                },
                (input) =>
                int.TryParse(input, out int value) && API.Assets.Has(value));

            TEXT.Get.ListedMessage(
                "Select field to update:",
                "(1) Change device.",
                "(2) Change brand.",
                "(3) Change model.",
                "(4) Change price.",
                "(5) Change purchase date.",
                "(6) Change office.",
                "(ESC) Return."
                ).Show();

            INPUT.AwaitKeyCommand(
                (new[] { ConsoleKey.D1, ConsoleKey.NumPad1 }, () => {
                    TEXT.Get.Message("Select new device:").Show();
                    TEXT.Get.Message("(1) Mobile | (2) Computer").Show();

                    Device old = asset!.Device;
                    Device? newDevice = default;

                    INPUT.AwaitKeyCommand(
                        (new[] { ConsoleKey.D1, ConsoleKey.NumPad1 }, () => newDevice = new Mobile()),
                        (new[] { ConsoleKey.D2, ConsoleKey.NumPad2 }, () => newDevice = new Computer()));

                    newDevice!.Brand = old.Brand;
                    newDevice!.Model = old.Model;

                    asset.Device = newDevice;
                    API.Devices.Delete(old);
                }),
                (new[] { ConsoleKey.D2, ConsoleKey.NumPad2 }, () => {

                    TEXT.Get.Message("New brand:").Show();
                    INPUT.AwaitTextInput((input) =>
                    {
                        asset!.Device.Brand = input!;
                    }, (input) => input != null);
                }),
                (new[] { ConsoleKey.D3, ConsoleKey.NumPad3 }, () => {
                    TEXT.Get.Message("New model:").Show();
                    INPUT.AwaitTextInput((input) =>
                    {
                        asset!.Device.Model = input!;
                    }, (input) => input != null);
                }),
                (new[] { ConsoleKey.D4, ConsoleKey.NumPad4 }, () => {
                    TEXT.Get.Message("New price (100,00):").Show();
                    INPUT.AwaitTextInput((input) =>
                    {
                        asset!.Price = decimal.Parse(input!);
                    }, (input) => decimal.TryParse(input, out decimal result));
                }),
                (new[] { ConsoleKey.D5, ConsoleKey.NumPad5 }, () => {
                    TEXT.Get.Message("New purchase date (yyyy-mm-dd):").Show();
                    INPUT.AwaitTextInput((input) =>
                    {
                        asset!.PurchaseDate = DateTime.Parse(input!);
                    }, (input) => DateTime.TryParse(input, out DateTime result));
                }),
                (new[] { ConsoleKey.D6, ConsoleKey.NumPad6 }, () => {

                    string[] offices = API.Offices.GetAll.Select(x => $"({x.Id}) {x.Name}").ToArray();

                    if (offices != null && offices.Length == 0)
                        return;

                    TEXT.Get.Message("Choose new office:").Show();
                    TEXT.Get.LinedMessage(offices).Show();

                    INPUT.AwaitTextInput((input) =>
                    {
                        if (API.Offices.GetSingle(int.Parse(input!)) != null)
                            asset!.Office = API.Offices.GetSingle(int.Parse(input!))!;
                    }, (input) => int.TryParse(input, out int result) && API.Offices.Has(result));
                }),
                (new[] { ConsoleKey.Escape }, () => DisplayView.Load()));


            API.Assets.Update(asset!);
            API.Action.Save();

            DisplayView.Load();
        }

        private static void DeleteAsset()
        {
            if (API.Assets.Count == 0)
                DisplayView.Load();

            TEXT.Get.Message("Select Asset to Delete (id):").Show();

            Asset asset;

            INPUT.AwaitTextInput(
                (input) =>
                {
                    //Simple escape incase user ends up in delete but dont want to.
                    if(input == null || input == string.Empty)
                        DisplayView.Load();

                    asset = API.Assets.GetSingle(int.Parse(input!))!;

                    if (asset == null)
                        DisplayView.Load();

                    API.Devices.Delete(asset!.Device);
                    API.Assets.Delete(asset!);
                    API.Action.Save();
                },
                (input) => input == null || input == string.Empty || 
                int.TryParse(input, out int value) && API.Assets.Has(value));

            DisplayView.Load();
        }

    }
}
