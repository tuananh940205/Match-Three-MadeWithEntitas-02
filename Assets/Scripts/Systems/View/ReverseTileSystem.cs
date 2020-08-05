using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ReverseTileSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;

    public ReverseTileSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TileReverse);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isTileReverse;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {

        }
    }
}
