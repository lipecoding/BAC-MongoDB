using System;
using System.Collections.Generic;
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
                            userDAO.userdata(account, "balance");
                        }
                        break;
                }
                
            }
            catch (Exception err)
            {
                Console.Write(err.Message);
            }

        }
    }
}
