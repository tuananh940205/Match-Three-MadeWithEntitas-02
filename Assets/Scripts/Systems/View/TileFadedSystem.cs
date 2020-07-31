using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;
using DG.Tweening;

public class TileFadedSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _board;

    public TileFadedSystem(Contexts contexts) : base (contexts.game)
    {
        _gameContext = contexts.game;
        _board = _gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.BoardRow, GameMatcher.BoardColumn));
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
            Debug.LogFormat("TileFadedSystem, Execute with x = {0}, y = {1}, entities = {2}", e.arrayPosition.x, e.arrayPosition.y, entities.Count);
            GameEntity[] boardEnts = _board.GetEntities();
            // Debug.LogFormat("length = {0}", boardEnts.Length);
            // GameEntity[] _boardEn = _board.GetEntities();

            e.view.gameObject.GetComponent<SpriteRenderer>().DOFade(0, .65f).OnComplete(() => 
            {
                foreach(GameEntity board in boardEnts)
                {
                    // Debug.LogFormat("AAA");
                    board.ReplaceFillPosition(e.arrayPosition.x, e.arrayPosition.y);
                }
                e.view.gameObject.Unlink();
                // e.view.gameObject.SetActive(false);
                Object.Destroy(e.view.gameObject);
                // GameEntity _newGameEn = _gameContext.CreateEntity();
                // e.view.gameObject.Link(_newGameEn);
                e.Destroy();
                // e.isFaded = false;
            });
        }
    }
}