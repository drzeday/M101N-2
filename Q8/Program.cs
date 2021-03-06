﻿using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Q8
{
    class InsertTest
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("test");

            var animals = db.GetCollection<BsonDocument>("animals");

            var animal = new BsonDocument
                            {
                            {"animal", "monkey"}
                            };

            await animals.InsertOneAsync(animal);
            animal.Remove("animal");
            animal.Add("animal", "cat");
            animal["_id"] = ObjectId.GenerateNewId();
            await animals.InsertOneAsync(animal);
            animal.Remove("animal");
            animal.Add("animal", "lion");
            animal["_id"] = ObjectId.GenerateNewId();
            await animals.InsertOneAsync(animal);

            var count = await animals.Find(new BsonDocument())
                .CountAsync();
        }
    }
}
