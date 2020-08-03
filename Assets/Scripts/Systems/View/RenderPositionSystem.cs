using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class RenderPositionSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;

    public RenderPositionSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Position);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            if(e.position.value.y <= 1.45f && e.view.gameObject.GetComponent<SpriteRenderer>().color == new Color(0, 0, 0, 0))
            {
                e.view.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
            e.view.gameObject.transform.position = e.position.value;
        }
    }
}