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

                if (!string.IsNullOrEmpty(testeCon.Find().First().ToJson()))
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
        #region get db
        public string userdata(string account, string type)
        {
            try
            {
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("_account", account);

                var result = acc_collection.Find(filter).First().ToJson();

                if(type == "updatebal")
                {
                    string[] resultUpBal = result.Split(" ");
                    return resultUpBal[16];
                }
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
                            try
                            {
                                string[] resultsplit = result.ToString().Split(" ");

                                string resultname = $"{resultsplit[9]} {resultsplit[10]}".Replace(",", "").Replace('"'.ToString(), "");
                                return resultname;
                            }
                            catch(Exception err)
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
                                string stringbal = $"O saldo da sua conta é de: {resultString(result, "currency_bal")} {resultbal[16]}.";
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
                                string stringcurrency = resultcurrency[13].Replace(",", "").Replace('"'.ToString(), "");
                                return stringcurrency;
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
        #endregion

        #region update db
        public bool updateBal(string[] account, double bal)
        {
            try
            {
                var collection = _db.GetCollection<BsonDocument>("account");
                var builder = Builders<BsonDocument>.Filter;

                var filter0 = builder.Eq("_account", account[0]);
                var filter1 = builder.Eq("_account", account[1]);

                double bal0 = Double.Parse(userdata(account[0], "updatebal")) - bal;
                double bal1 = Double.Parse(userdata(account[1], "updatebal"));

                var update0 = Builders<BsonDocument>.Update.Set("_balance", bal0);
                var update1 = Builders<BsonDocument>.Update.Set("_balance", bal1);

                collection.UpdateOne(filter0, update0);
                collection.UpdateOne(filter1, update1);

                return true;
            } 
            catch (Exception err)
            {
                return false;
            }
        }
        #endregion

    }
}
