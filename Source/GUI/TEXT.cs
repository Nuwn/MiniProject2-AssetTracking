namespace GUI
{
    public sealed class TEXT
    {
        private static TEXT? singleton;
        public static TEXT Get => singleton ??= new TEXT();


        private string message = string.Empty;
        private ConsoleColor color = ConsoleColor.White;

        public TEXT Message(string v)
        {
            message = v;
            return this;
        }

        public TEXT LinedMessage(params string[] text)
        {
            message = OutputLine(text);
            return this;
        }
        public TEXT ListedMessage(params string[] text)
        {
            message = OutputList(text);
            return this;
        }

        public TEXT Color(ConsoleColor color)
        {
            this.color = color;
            return this;
        }

        public void Show()
        {
            ConsoleColor old = Console.ForegroundColor;
            Console.ForegroundColor = color;

            Console.WriteLine(message);

            Console.ForegroundColor = old;
        }

        static string OutputLine(params string[] text) => string.Join(';', text).Replace(";", " | ");
        static string OutputList(params string[] text) => string.Join('\n', text);
    }
}
