using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;
using DG.Tweening;

public class TileFadedSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;

    public TileFadedSystem(Contexts contexts) : base (contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Faded);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isFaded;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            e.view.gameObject.GetComponent<SpriteRenderer>().DOFade(0, .65f).OnComplete(() => 
            {
                e.view.gameObject.Unlink();
                // e.view.gameObject.SetActive(false);



                // GameEntity _newGameEn = _gameContext.CreateEntity();
                // e.view.gameObject.Link(_newGameEn);

                e.Destroy();
                
                // e.isFaded = false; 
            });
        }
    }
}