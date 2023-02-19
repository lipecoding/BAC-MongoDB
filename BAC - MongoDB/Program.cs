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

                
            }
            catch (Exception err) 
            { 
                Console.WriteLine(err.Message);
            }
        }

        public static void inicio()
        {
            try
            {
                string accountInfo = string.Empty;

                Console.WriteLine("Bem vindo ao BAC!");
                Console.WriteLine();
                Console.WriteLine("Digite o número da agência e conta(formato: agencia/conta)");

                accountInfo = Console.ReadLine();

                if (!string.IsNullOrEmpty(accountInfo))
                {

                }
                else
                {
                    throw new Exception("Devem ser digitado os dados da conta!");
                }


            } 
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }
        }
    }
}
