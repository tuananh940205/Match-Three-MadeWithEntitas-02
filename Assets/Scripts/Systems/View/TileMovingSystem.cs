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
                int maxX;
                int maxY;
                int secondX = -1;
                int secondY = -1;
                GameEntity secondEntity;
                Debug.LogFormat("Angle = {0}", swipeAngle);

                GameEntity[] boardEntities = _board.GetEntities();
                // Debug.LogFormat("board Count = {0}", boardEntities.Length);

                maxX = boardEntities[0].boardRow.value;
                maxY = boardEntities[0].boardColumn.value;

                if(swipeAngle > -45 && swipeAngle < 45)
                {
                    if(e.arrayPosition.x < maxX - 1)
                    {
                        secondX = e.arrayPosition.x + 1;
                        secondY = e.arrayPosition.y;
                    }
                }
                else if(swipeAngle > 45 && swipeAngle < 135)
                {
                    if(e.arrayPosition.y > 0)
                    {
                        secondX = e.arrayPosition.x;
                        secondY = e.arrayPosition.y - 1;
                    }
                }
                else if(swipeAngle > 135 || swipeAngle < -135)
                {
                    if(e.arrayPosition.x > 0)
                    {
                        secondX = e.arrayPosition.x - 1;
                        secondY = e.arrayPosition.y;
                    }
                }
                else if(swipeAngle > -135 && swipeAngle < -45)
                {
                    if(e.arrayPosition.y < maxY - 1)
                    {
                        secondX = e.arrayPosition.x;
                        secondY = e.arrayPosition.y + 1;
                    }
                }

                Debug.LogFormat("x = {0}, y = {1}", secondX, secondY);

                if(secondX != -1 && secondY != -1)
                {
                    IGroup<GameEntity> secondEntities = _gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.ArrayPosition));
                    foreach(GameEntity _gameEntity in secondEntities.GetEntities())
                    {
                        if(_gameEntity.arrayPosition.x == secondX && _gameEntity.arrayPosition.y == secondY)
                        {
                            secondEntity = _gameEntity;

                            if(secondEntity != null)
                            {
                                Debug.LogFormat("Not null");
                                break;
                            }
                        }
                        // else
                        //     Debug.LogFormat("x = {0}, y = {1}", secondX, secondY);
                    }
                }
            }
        }
    }
}