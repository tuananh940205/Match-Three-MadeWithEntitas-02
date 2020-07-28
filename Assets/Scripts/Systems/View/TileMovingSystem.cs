using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class TileMovingSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _board;

    public TileMovingSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        _board = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.BoardColumn, GameMatcher.BoardRow));
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Moving);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasMoving && entity.hasArrayPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            // Debug.LogFormat("Moningggg");
            if(Vector2.Distance(e.moving.startPos, e.moving.endPos) >= 0.5f)
            {
                float swipeAngle = Mathf.Atan2(e.moving.endPos.y - e.moving.startPos.y, e.moving.endPos.x - e.moving.startPos.x) * 180 / Mathf.PI;
                Debug.LogFormat("Angle = {0}", swipeAngle);

                GameEntity[] boardEntities = _board.GetEntities();
                Debug.LogFormat("board Count = {0}", boardEntities.Length);

                // if(swipeAngle > -45 && swipeAngle < 45)
                // {
                //     if(e.arrayPosition.x < )
                // }
            }
        }
    }
}