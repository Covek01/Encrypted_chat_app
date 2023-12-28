using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using CipherNew.Cyphers;
using CipherNew.DataLayer;

namespace CipherNew
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IMessageManager, MessageManager>()
                .AddSingleton<IUserManager, UserManager>()
                .AddSingleton<ICypher, RailFenceCypher>()
                .BuildServiceProvider();
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Forms.InsertName(provider));
        }
    }
}