using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using Library.Providers;
using Library.Providers.Impl;

namespace MessengerDemoConsole
{
    public class ConsoleProvider : IProvider
    {
        public ConsoleProvider()
        {
            Settings = new ProviderSettings(MessagePriority.Low);
        }

        public IProviderSettings Settings { get; }
        public void Write(string message)
        {
            Console.Write(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<IProvider>
            {
                new ConsoleProvider(),

                new FileProvider(
                    new FileProviderSettings(MessagePriority.Low, @"c:\temp\test2.txt"))
            };

            var m = new Messenger(list);

            m.Send(MessagePriority.Medium, "Hello!");
            Console.Read();
        }
    }
}
