using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

namespace BAC___MongoDB
{
    internal class Program
    {
        public static void Main(string[] args) 
        {
            try
            {
                DAO.UserDAO connection = new DAO.UserDAO();

                if (connection.con())
                    inicio();
                else
                    throw new Exception("Conexão perdida!");

                
            }
            catch (Exception err) 
            { 
                Console.WriteLine(err.Message);
                Main(args);
            }
        }

        public static void inicio()
        {
            try
            {
                string? accountInfo = string.Empty;
                Console.Clear();
                Console.WriteLine("Bem vindo ao BAC!");
                Console.WriteLine();
                Console.WriteLine("Digite o número da agência e conta(formato: agencia/conta)");

                accountInfo = Console.ReadLine();

                if (!string.IsNullOrEmpty(accountInfo))
                {
                    Model.UserDTO acountData = new Model.UserDTO();

                    acountData.Agency = accountInfo.Substring(0, 4);
                    acountData.Account = accountInfo.Substring(5);

                    DAO.UserDAO userDAO = new DAO.UserDAO();

                    if(userDAO.access(acountData.Agency, acountData.Account))
                    {
                        View.Main main = new View.Main();

                        main.menu(acountData.Account);
                    }
                }
                else
                {
                    throw new Exception("Devem ser digitado os dados da conta!");
                }


            } 
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
                inicio();
            }
        }
    }
}
