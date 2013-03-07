using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using Restuta.ConsoleExtensions.Colorfull;

namespace Mongo.Guid_vs_ObjectId
{
    internal delegate void Write(ColorfullString colorfullString);
    internal delegate void WriteLine(ColorfullString colorfullString);
        
    class Program
    {
        static readonly Write W = ColorfullConsole.Write;
        static readonly WriteLine WL = ColorfullConsole.WriteLine;


        static void Main(string[] args)
        {
            Measure.WhenDone = (prefix, time) => WL(prefix.ToString().DarkCyan() + " time: " + (time.ToString() + "ms").DarkYellow());


            
            var db = GetDatabase();

            
            
            //actual sugar
            Measure.Performance("1M document insertion with GUIDs", () =>
            {
                
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
    }
}
