using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class MoveBackSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;

    public MoveBackSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Move, GameMatcher.Moving));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasMove && entity.hasMoving;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            Debug.LogFormat("MoveBackSystem, Execute");
        }
    }
}
