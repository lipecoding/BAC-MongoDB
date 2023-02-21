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

                Console.WriteLine("Olá " + accountData.Name + "!");
                Console.WriteLine();
                Console.WriteLine("O que iremos fazer hoje?");
                Console.WriteLine("1 - Tranferencias\n2 - Saldo\n3-Moeda Padrão\n4 - Saque");
                

                switch(Console.ReadLine())
                {
                    case "1":
                        {

                        }
                        break; 
                    case "2":
                        {
                            Console.Clear();
                            string bal = userDAO.userdata(account, "balance");
                            Console.WriteLine(bal);
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
