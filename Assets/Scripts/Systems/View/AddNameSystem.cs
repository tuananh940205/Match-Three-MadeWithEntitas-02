using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class AddNameSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;

    public AddNameSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.View, GameMatcher.TileName));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasTileName;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            e.view.gameObject.name = e.tileName.name;
        }
    }
}