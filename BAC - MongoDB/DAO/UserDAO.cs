using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAC___MongoDB.DAO
{
    internal class UserDAO
    {
        private static MongoClientSettings _settings = new MongoClientSettings
        {
            ServerSelectionTimeout = new TimeSpan(0, 0, 5),
            Server = new MongoServerAddress("0.0.0.0", 27017)
        };
        private static MongoClient _client = new MongoClient(_settings);
        private static IMongoDatabase _db = _client.GetDatabase("BAC");
        private IMongoCollection<BsonDocument> acc_collection = _db.GetCollection<BsonDocument>("account");
        private FilterDefinitionBuilder<BsonDocument> builder = Builders<BsonDocument>.Filter;

        public bool con()
        {
            try
            {
                var testeCon = _db.GetCollection<BsonDocument>("testeCon");
                var filter = builder.Eq("Teste", "111");

                var result = testeCon.Find(filter);

                return true;
            }
            catch (TimeoutException)
            {
                return false;
            }
        }

        public bool access(string agency, string account)
        {
            try
            {
                var collection = _db.GetCollection<BsonDocument>("agency");
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("_agency", agency) & builder.Eq("_account", account);

                var result = collection.Find(filter).First().ToJson();

                if (!string.IsNullOrEmpty(result.ToString()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (TimeoutException)
            {
                return false;
            }
        }

        public string userdata(string account, string type)
        {
            try
            {
                var filter = builder.Eq("_account", account);

                var result = acc_collection.Find(filter).First().ToJson();

                return resultString(result, type);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return string.Empty;
            }
        }

        public string resultString(string result, string type)
        {
            try
            {
                switch (type)
                {
                    case "name":
                        {
                            string[] r3 = result.ToString().Split(" ");

                            string resultname = $"{r3[9]} {r3[10]}".Replace(",", "");
                            resultname = resultname.Replace('"'.ToString(), "");
                            return resultname;
                        }
                    case "balance":
                        {

                            try
                            {
                                string[] resultbal = result.ToString().Split(" ");
                                string stringbal = $"O saldo da sua conta é de: {resultString(result, "currency")} {resultbal[16]}.";
                                return stringbal;
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                                return string.Empty;
                            }

                        }
                }

                return string.Empty;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return string.Empty;
            }
        }
    }
}
