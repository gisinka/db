using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace Game.Domain
{
    public class GameTurnEntity
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private readonly Dictionary<Guid, PlayerDecision> decisions;

        //TODO: Придумать какие свойства должны быть в этом классе, чтобы сохранять всю информацию о закончившемся туре.
        [BsonElement]
        public Guid Id { get; private set; }

        [BsonElement]
        public Guid GameId { get; }

        [BsonElement]
        public Guid WinnerId { get; }

        [BsonElement]
        public int TurnIndex { get; }

        public IReadOnlyDictionary<Guid, PlayerDecision> Decisions => decisions;

        [BsonConstructor]
        public GameTurnEntity(Guid id, Guid gameId, Guid winnerId, Dictionary<Guid, PlayerDecision> decisions, int turnIndex)
        {
            Id = id;
            GameId = gameId;
            this.decisions = decisions;
            WinnerId = winnerId;
            TurnIndex = turnIndex;
        }

        public GameTurnEntity(Guid gameId, Guid winnerId, Dictionary<Guid, PlayerDecision> decisions, int turnIndex) : this(Guid.Empty, gameId, winnerId, decisions, turnIndex) { }

        public GameTurnEntity() { }
    }
}