using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAC___MongoDB.Model
{
    internal class UserDTO
    {
        private string account = string.Empty;
        private string agency = string.Empty;
        private string name = string.Empty;
        private string balance = string.Empty;
        private string currency = string.Empty;

        public string Account { get => account; set => account = value; }
        public string Name { get => name; set => name = value; }
        public string Balance { get => balance; set => balance = value; }
        public string Currency { get => currency; set => currency = value; }
        public string Agency { get => agency; set => agency = value; }
    }
}
