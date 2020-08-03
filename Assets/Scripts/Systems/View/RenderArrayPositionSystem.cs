using Entitas;
using UnityEngine;
using Entitas.Unity;
using System.Collections.Generic;

public class RenderArrayPositionSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    Vector2 startPosition = new Vector2(-1.75f, .95f);
    Vector2 offset = Resources.Load<GameObject>("Prefabs/Blue").GetComponent<SpriteRenderer>().bounds.size;

    public RenderArrayPositionSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.View, GameMatcher.ArrayPosition));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView && entity.hasArrayPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            // Debug.LogFormat("RenderPositionSystem, Execute");
            Vector2 pos = new Vector2(startPosition.x + e.arrayPosition.x * offset.x, startPosition.y - e.arrayPosition.y * offset.y);
            e.ReplacePosition(pos);
        }
    }
}