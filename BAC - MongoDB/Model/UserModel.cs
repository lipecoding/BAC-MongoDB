using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAC___MongoDB.Model
{
    internal class UserDTO
    {
        public string Account { get; set; }
        public string Name { get; set; }
        public string Balance { get; set; }
        public string Currency { get; set; }
        public string Agency { get; set; }
    }
}
