using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAC___MongoDB.DAO
{
    internal class Connection
    {
        public bool con () 
        {
            var client = new MongoClient("mongodb://0.0.0.0:27017");

            if (client != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool access(string agency, string account) 
        {


            return false;
        }
    }
}
