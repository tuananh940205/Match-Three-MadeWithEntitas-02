using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class FindAdjacentSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _board;

    public FindAdjacentSystem(Contexts contexts) : base(contexts.game)
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
                GameEntity e2;
                // Debug.LogFormat("Angle = {0}", swipeAngle);

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

                // Debug.LogFormat("x = {0}, y = {1}", secondX, secondY);

                if(secondX != -1 && secondY != -1)
                {
                    IGroup<GameEntity> secondEntities = _gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.ArrayPosition));
                    foreach(GameEntity _gameEntity in secondEntities.GetEntities())
                    {
                        if(_gameEntity.arrayPosition.x == secondX && _gameEntity.arrayPosition.y == secondY)
                        {
                            e2 = _gameEntity;

                            if(e2 != null)
                            {
                                // Debug.LogFormat("Ent 1 = {0}, ent2 = {1}", e.view.gameObject.name, e2.view.gameObject.name);
                                int target1X = e2.arrayPosition.x;
                                int target1Y = e2.arrayPosition.y;
                                int target2X = e.arrayPosition.x;
                                int target2Y = e.arrayPosition.y;
                                // Vector2 target1 = e2.view.gameObject.transform.position;
                                // Vector2 target2 = e.view.gameObject.transform.position;
                                e.ReplaceMove(target1X, target1Y);
                                e2.ReplaceMove(target2X, target2Y);
                                // e2.ReplaceMove(e.view.gameObject.transform.position);
                                // Debug.LogFormat("Not null");
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}