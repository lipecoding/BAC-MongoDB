using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAC___MongoDB.Model
{
    internal class AcountData
    {
        private string account = string.Empty;
        private string agency = string.Empty;
        private string name = string.Empty;
        private string balance = string.Empty;
        private string currency = string.Empty;

        public string Agency { get => Agency1; set => Agency1 = value; }
        public string Account { get => account; set => account = value; }
        public string Agency1 { get => agency; set => agency = value; }
        public string Name { get => name; set => name = value; }
        public string Balance { get => balance; set => balance = value; }
        public string Currency { get => currency; set => currency = value; }
    }
}
