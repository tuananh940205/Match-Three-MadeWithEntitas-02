using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class AddViewSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    Transform _viewContainer = new GameObject("Game Views").transform;

    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.View));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            e.view.gameObject = Object.Instantiate(e.view.gameObject);
            e.view.gameObject.transform.SetParent(_viewContainer, true);
            e.view.gameObject.Link(e);
        }
    }
}