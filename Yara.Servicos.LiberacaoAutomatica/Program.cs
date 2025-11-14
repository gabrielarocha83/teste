using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Yara.Servicos.PortalFinanceiro;

namespace Yara.Servicos.LiberacaoAutomatica
{
    static class Program
    {
        static void Main(string[] args)
        {
            YaraPortalFinanceiro portalFinanceiro = new YaraPortalFinanceiro();
            log4net.Config.XmlConfigurator.Configure();
            if (System.Environment.UserInteractive)
            {
                string parameter = string.Concat(args);

                switch (parameter)
                {

                    case "/i":
                        ManagedInstallerClass.InstallHelper(new string[] {Assembly.GetExecutingAssembly().Location});
                        break;

                    case "/u":
                        ManagedInstallerClass.InstallHelper(new string[]
                            {"/u", Assembly.GetExecutingAssembly().Location});
                        break;

                    case "/run":

                        Console.WriteLine("Iniciando Serviços Assíncronos Portal Financeiro Yara.");
                        Console.WriteLine("Pressione qualquer tecla para parar.");

                        portalFinanceiro.Start();

                        Console.ReadKey();

                        Console.WriteLine("Serviço Parado.");
                        break;

                    default:

                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("Serviços Assíncronos Portal Financeiro Yara.");
                        Console.WriteLine("---------------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine(
                            "- Para instalar esse serviço execute \"Yara.Servicos.PortalFinanceiro.exe /i\"");
                        Console.WriteLine(
                            "- Para remover esse serviço execute \"Yara.Servicos.PortalFinanceiro.exe /u\"");
                        Console.WriteLine(
                            "- Para executar esse serviço execute \"Yara.Servicos.PortalFinanceiro.exe /run\"");
                        Console.WriteLine();

                        break;
                }
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                    new YaraPortalFinanceiro()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
