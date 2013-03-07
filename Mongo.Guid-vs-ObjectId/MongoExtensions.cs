using MongoDB.Driver;

namespace Mongo.Guid_vs_ObjectId
{
    public static class MongoExtensions
    {
        /// <summary>
        /// Gets typed mongo collection using given generic type and type name as a collection name.
        /// </summary>
        public static MongoCollection<T> GetCollection<T>(this MongoDatabase db)
        {
            return db.GetCollection<T>(typeof(T).Name);
        }
    }
}