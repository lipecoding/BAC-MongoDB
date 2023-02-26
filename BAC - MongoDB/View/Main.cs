using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAC___MongoDB.View
{
    internal class Main
    {
        private Model.UserDTO accountData = new Model.UserDTO();

        
        public void menu(string account)
        {
            try 
            {
                DAO.UserDAO userDAO = new DAO.UserDAO();
                accountData.Name = userDAO.userdata(account, "name");
                accountData.Account = account;

                Console.Clear();
                Console.WriteLine("Olá " + accountData.Name + "!");
                Console.WriteLine();
                Console.WriteLine("O que iremos fazer hoje?");
                Console.WriteLine("1 - Tranferencias\n2 - Saldo\n3 - Moeda Padrão\n4 - Saque");
                

                switch(Console.ReadLine())
                {
                    case "1":
                        {
                            Console.Clear();

                            Console.WriteLine("Para quem será a tranferencia? (Agencia/Conta)");
                            string?[] accountArray = { accountData.Account, Console.ReadLine().Substring(5) };
                            Console.WriteLine();

                            Console.WriteLine("Qual valor a ser transferido?");
                            double bal = Double.Parse(Console.ReadLine());

                            userDAO.updateBal(accountArray, bal);

                            back();

                        }
                        break; 
                    case "2":
                        {
                            Console.Clear();
                            string bal = userDAO.userdata(account, "balance");
                            Console.WriteLine(bal);
                            back();
                        }
                        break;
                }
                
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
            }

        }

        public void back ()
        {
            Console.WriteLine("Deseja voltar ao menu?");
            string? option = Console.ReadLine();
            if(!string.IsNullOrEmpty(option))
            {
                if(option.Equals("S", StringComparison.OrdinalIgnoreCase))
                {
                    menu(accountData.Account);
                }
                else
                {
                    Program.inicio();
                }
            }

        }
    }
}
