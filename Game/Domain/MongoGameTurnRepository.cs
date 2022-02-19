using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace Game.Domain
{
    public class MongoGameTurnRepository : IGameTurnRepository
    {
        public const string CollectionName = "turns";
        private readonly IMongoCollection<GameTurnEntity> turnsCollection;

        public MongoGameTurnRepository(IMongoDatabase db)
        {
            turnsCollection = db.GetCollection<GameTurnEntity>(CollectionName);
            turnsCollection.Indexes.CreateOne(new CreateIndexModel<GameTurnEntity>(Builders<GameTurnEntity>.IndexKeys
                .Ascending(x => x.GameId)
                .Ascending(x => x.TurnIndex),
                new CreateIndexOptions { Unique = true }));
        }

        public GameTurnEntity Insert(GameTurnEntity gameTurn)
        {
            turnsCollection.InsertOne(gameTurn);
            return gameTurn;
        }

        public IReadOnlyList<GameTurnEntity> GetLastTurns(Guid gameId, int turnNumber)
        {
            var turns = turnsCollection.Find(x => x.GameId == gameId)
                .SortByDescending(x => x.TurnIndex)
                .Limit(turnNumber)
                .ToList();

            return turns;
        }
    }
}