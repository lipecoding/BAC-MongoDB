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
            Server = new MongoServerAddress("localhost", 27017)
        };
        private static MongoClient _client = new MongoClient(_settings);
        private static IMongoDatabase _db = _client.GetDatabase("BAC");
        private IMongoCollection<BsonDocument> acc_collection = _db.GetCollection<BsonDocument>("account");

        public bool con()
        {
            try
            {
                var testeCon = _db.GetCollection<BsonDocument>("testeCon");
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("Teste", "111");

                if (!string.IsNullOrEmpty(testeCon.Find(filter).First().ToJson()))
                    return true;
                else
                    return false;
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
                var collection = _db.GetCollection<BsonDocument>("account");
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
            catch (Exception err)
            {
                if (err.Message == "Sequence contains no elements")
                {
                    Console.WriteLine("Dados da conta inexistentes!");
                    Program.inicio();
                }
                return false;
            }
        }
        #region get db
        public string userdata(string account, string type)
        {
            try
            {
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("_account", account);

                var result = acc_collection.Find(filter).First().ToJson();
                string response = resultString(result, type);

                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception($"O tipo {type} retornou erro.");
                }
                return response;
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
                            try
                            {
                                string[] resultsplit = result.ToString().Split(" ");

                                string resultname = $"{resultsplit[12]} {resultsplit[13]}".Replace(",", "").Replace('"'.ToString(), "");
                                return resultname;
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                                return string.Empty;
                            }
                        }
                    case "balance":
                        {

                            try
                            {
                                string[] resultbal = result.ToString().Split(" ");
                                string stringbal = $"O saldo da sua conta é de: {resultString(result, "currency_bal")} {resultbal[16]}.".Replace(",", "");
                                return stringbal;
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                                return string.Empty;
                            }

                        }
                    case "currency_bal":
                        {
                            try
                            {
                                string[] resultcurrency = result.ToString().Split(" ");
                                string stringcurrency = resultcurrency[19].Replace(",", "").Replace('"'.ToString(), "");
                                return stringcurrency;
                            }
                            catch (Exception err)
                            {
                                Console.WriteLine(err.Message);
                                return string.Empty;
                            }
                        }
                    case "updatebal":
                        {
                            string[] resultUpBal = result.Split(" ");
                            return resultUpBal[16].Replace(",", "");
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
        #endregion

        #region update db
        public bool transBal(string[] account, double bal)
        {
            try
            {
                var collection = _db.GetCollection<BsonDocument>("account");
                var builder = Builders<BsonDocument>.Filter;

                var filter0 = builder.Eq("_account", account[0]);
                var filter1 = builder.Eq("_account", account[1]);

                var update0 = Builders<BsonDocument>.Update.Inc("_balance", -bal);
                var update1 = Builders<BsonDocument>.Update.Inc("_balance", bal);

                collection.FindOneAndUpdate(filter0, update0);
                collection.FindOneAndUpdate(filter1, update1);

                return true;
            }
            catch (Exception err)
            {
                return false;
            }
        }

        public bool updatecurrency(string account, string currency)
        {
            try
            {
                var collection = _db.GetCollection<BsonDocument>("account");
                var builder = Builders<BsonDocument>.Filter;

                var filter = builder.Eq("_account", account);

                var update = Builders<BsonDocument>.Update.Set("_currency", currency);

                collection.FindOneAndUpdate(filter, update);

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }

        public bool decreasebal (string account, double bal) 
        { 
            try
            {
                var collection = _db.GetCollection<BsonDocument>("account");
                var builder = Builders<BsonDocument>.Filter;

                var filter = builder.Eq("_account", account);

                var update = Builders<BsonDocument>.Update.Inc("_balance", -bal);

                collection.FindOneAndUpdate(filter, update);

                return true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                return false;
            }
        }
        #endregion

    }
}
