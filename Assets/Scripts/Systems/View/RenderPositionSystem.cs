using Entitas;
using UnityEngine;
using Entitas.Unity;
using System.Collections.Generic;

public class RenderPositionSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    Vector2 startPosition = new Vector2(-2.45f + 0.7f, 1.65f - 0.7f);
    Vector2 offset = Resources.Load<GameObject>("Prefabs/Blue").GetComponent<SpriteRenderer>().bounds.size;

    public RenderPositionSystem(Contexts contexts) : base(contexts.game)
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
            // Debug.LogFormat("" + offset);
            Vector2 pos = new Vector2(startPosition.x + e.arrayPosition.x * offset.x, startPosition.y + e.arrayPosition.y * offset.y);
            // e.view.gameObject.transform.position = new Vector2(startPosition.x + e.arrayPosition.x * offset.x, startPosition.y + e.arrayPosition.y) * offset.y;
            e.view.gameObject.transform.position = pos;
            // Debug.LogFormat("x = {0}, y = {1}", e.arrayPosition.x, e.arrayPosition.y);
        }
    }
}