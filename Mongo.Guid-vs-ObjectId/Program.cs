using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Restuta.ConsoleExtensions.Colorfull;

namespace Mongo.Guid_vs_ObjectId
{
    internal delegate void Write(ColorfullString colorfullString);
    internal delegate void WriteLine(ColorfullString colorfullString);

    public class TestDocumentObjectId
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }
    }

    class Program
    {
        static readonly Write W = ColorfullConsole.Write;
        static readonly WriteLine WL = ColorfullConsole.WriteLine;


        static void Main(string[] args)
        {
            Measure.WhenDone = (prefix, time) => WL(prefix.ToString().DarkCyan() + " time: " + (time.ToString() + "ms").DarkYellow());

            const int NumberOfDocuments = 26402104;
            var db = GetDatabase();

            //Measure.Performance(NumberOfDocuments + " documents insertion with", () =>
            //{
            //    foreach (var document in NewTestDocument().Take(NumberOfDocuments))
            //    {
            //        db.GetCollection<TestDocumentObjectId>().Insert(document);   
            //    }
            //});
            //WL((Measure.LastMeasurmentTime / Convert.ToDouble(NumberOfDocuments)).ToString() + "ms per document");

            Measure.Performance(NumberOfDocuments + " BATCH documents insertion with", () =>
            {
                db.GetCollection<TestDocumentObjectId>().InsertBatch(NewTestDocument().Take(NumberOfDocuments));
            });
            WL((Measure.LastMeasurmentTime / Convert.ToDouble(NumberOfDocuments)).ToString() + "ms per document");

            Measure.Performance("Skip 10 000 000 docs and take one", () =>
            {
                var doc = db.GetCollection<TestDocumentObjectId>().AsQueryable().Skip(10000000).Take(1).ToList().SingleOrDefault();
                WL(doc.Id.ToString());
            });

            Measure.Performance("Reading one document by id", () =>
            {
                var doc = db.GetCollection<TestDocumentObjectId>().AsQueryable().SingleOrDefault(x => x.Id == new ObjectId("5137e369932bdc677423e80d"));
                WL(doc.Id.ToString());
            });

            Measure.Performance("Reading one document by id", () =>
            {
                var result = db.GetCollection<TestDocumentObjectId>().AsQueryable().Select(x => x.Id).GroupBy(x => x).Count();
                WL(result.ToString());
            });

        }

        private static MongoDatabase GetDatabase()
        {
            var testDbConnStrBuilder = new MongoUrlBuilder("mongodb://localhost/Guid-vs-ObjectId?safe=true");
            var connectionString = testDbConnStrBuilder.ToMongoUrl();
            var client = new MongoClient(connectionString);
            var mongoDatabase = client.GetServer().GetDatabase(connectionString.DatabaseName);

            mongoDatabase.SetProfilingLevel(ProfilingLevel.All);
            return mongoDatabase;
        }


        public static IEnumerable<TestDocumentObjectId> NewTestDocument()
        {
            while (true)
            {
                yield return new TestDocumentObjectId();
            }
        }
    }
}
