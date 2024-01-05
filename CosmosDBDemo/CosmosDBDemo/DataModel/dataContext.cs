using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CosmosDBDemo.Models;

namespace CosmosDBDemo.DataModel
{
    public class DataContext
    {
        //private CosmosClient _client = null;
        //private Database _database = null;
        private Container _container = null;

        public void ConnectToDB()
        {
            try
            {
                var con = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Cosmos")["AccountURL"];
                var key= new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Cosmos")["AuthKey"];
                var _connectionString = $"AccountEndpoint={con};AccountKey={key}";
                CosmosClient _client = new CosmosClient(_connectionString);

                var db = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Cosmos")["Database"];
                Database _database = _client.GetDatabase(db);
                _container = _database.GetContainer("Apps");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateAppAsync(Apps apps)
        {
            var app = new Apps
            {
                id = Guid.NewGuid().ToString(),
                AppName = apps.AppName,
                Date = apps.Date,
                Version = apps.Version,
                Org = apps.Org,
                feedback = apps.feedback
            };

            ConnectToDB();
            await _container.CreateItemAsync(app);
        }

        public async Task UpdateAppAsync(Apps apps)
        {
            ConnectToDB();
            var app = new Apps
            {
                id = Guid.NewGuid().ToString(),
                AppName = apps.AppName,
                Date = apps.Date,
                Version = apps.Version,
                Org = apps.Org,
                feedback = apps.feedback
            };

            await _container.ReplaceItemAsync(apps,apps.id);
        }

        public async Task DeleteAppAsync(Guid id)
        {
            ConnectToDB();
            await _container.DeleteItemAsync<Apps>(id.ToString(),new PartitionKey(id.ToString()));
        }

        public async Task<List<Apps>> GetAppsAsync()
        {
            ConnectToDB();
            string query = "Select * from c";
            var appsIterator = _container.GetItemQueryIterator<Apps>(query);
            var apps = await appsIterator.ReadNextAsync();
            
            return apps.ToList();
        }

        public async Task<Apps> GetAppsByIdAsync(string id)
        {
            ConnectToDB();
            var app = await _container.ReadItemAsync<Apps>(id, new PartitionKey(id));
            return app;
        }
    }

}
