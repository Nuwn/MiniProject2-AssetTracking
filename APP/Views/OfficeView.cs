using API;
using DataModels;
using GUI;

namespace MiniProject2_AssetTracking.Views
{
    public static class OfficeView
    {
        public static void Load()
        {
            Console.WriteLine();
            TEXT.Get.Message("Offices:").Show();
            TEXT.Get.ListedMessage(API.Offices.GetAll.Select(x => $"({x.Id}) {x.Name}").ToArray()).Show();

            Console.WriteLine();
            TEXT.Get.ListedMessage(
                "(1) New Office.",
                "(2) Delete Office.",
                "(ESC) Return.").Show();

            INPUT.AwaitKeyCommand(
                (new[] { ConsoleKey.D1, ConsoleKey.NumPad1 }, NewOffice),
                (new[] { ConsoleKey.D2, ConsoleKey.NumPad2 }, DeleteOffice),
                (new[] { ConsoleKey.Escape }, () => DisplayView.Load()));
            
        }

        public static void NewOffice()
        {
            Office office = new Office();

            TEXT.Get.Message("Enter office name:").Show();
            INPUT.AwaitTextInput(input =>
            {
                office.Name = input!;
            }, input => input != null);

            TEXT.Get.Message("Enter Currency:").Show();
            TEXT.Get.LinedMessage(Currency.GetCurrencies).Show();

            INPUT.AwaitTextInput(input =>
            {
                office.Currency = input!.ToUpper();
            }, input => input != null && Currency.GetCurrencies.Contains(input.ToUpper()));

            API.Offices.Add(office);
            API.Action.Save();

            DisplayView.Load();
        }

        private static void DeleteOffice()
        {
            if (API.Offices.Count == 0)
                DisplayView.Load();

            TEXT.Get.Message("Select Office to Delete (id):").Show();

            Office office;

            INPUT.AwaitTextInput(
                (input) =>
                {
                    //Simple escape incase user ends up in delete but dont want to.
                    if (input == null || input == string.Empty)
                        DisplayView.Load();

                    office = API.Offices.GetSingle(int.Parse(input!))!;

                    if (office == null)
                        DisplayView.Load();

                    API.Offices.Delete(office!);
                    API.Action.Save();
                },
                (input) => input == null || input == string.Empty ||
                int.TryParse(input, out int value) && API.Offices.Has(value));

            DisplayView.Load();
        }

    }
}
