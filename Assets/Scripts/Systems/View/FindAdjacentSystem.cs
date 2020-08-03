using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class FindAdjacentSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity _boardEntity;

    public FindAdjacentSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.FirstPosition, GameMatcher.LastPosition));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasFirstPosition && entity.hasLastPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            Debug.LogFormat ("FindAdjacentSystem, Execute");
            if (Vector2.Distance (new Vector2 (e.firstPosition.x, e.firstPosition.y), new Vector2 (e.lastPosition.x, e.lastPosition.y)) >= 0.5f)
            {
                float swipeAngle = Mathf.Atan2(e.lastPosition.y - e.firstPosition.y, e.lastPosition.x - e.firstPosition.x) * 180 / Mathf.PI;
                int secondX = -1;
                int secondY = -1;
                // GameEntity e2;
                // Debug.LogFormat ("Angle = {0}", swipeAngle);
                _boardEntity = _gameContext.boardSizeEntity;
                // Debug.LogFormat("swipeAngle = {0}", swipeAngle);
                if (swipeAngle > -45 && swipeAngle < 45)
                {
                    if (e.arrayPosition.x < _boardEntity.boardSize.row - 1)
                    {
                        secondX = e.arrayPosition.x + 1;
                        secondY = e.arrayPosition.y;
                    }
                }
                else if (swipeAngle > 45 && swipeAngle < 135)
                {
                    if (e.arrayPosition.y > 0)
                    {
                        secondX = e.arrayPosition.x;
                        secondY = e.arrayPosition.y - 1;
                    }
                }
                else if (swipeAngle > 135 || swipeAngle < -135)
                {
                    if (e.arrayPosition.x > 0)
                    {
                        secondX = e.arrayPosition.x - 1;
                        secondY = e.arrayPosition.y;
                    }
                }
                else if (swipeAngle > -135 && swipeAngle < -45)
                {
                    if (e.arrayPosition.y < _boardEntity.boardSize.column - 1)
                    {
                        secondX = e.arrayPosition.x;
                        secondY = e.arrayPosition.y + 1;
                    }
                }
                // Debug.LogFormat("x = {0}, y = {1}", secondX, secondY);
                if (secondX != -1 && secondY != -1)
                {
                    IGroup<GameEntity> secondEntities = _gameContext.GetGroup (GameMatcher.View);
                    foreach (GameEntity _gameEntity in secondEntities.GetEntities())
                    {
                        if (_gameEntity.arrayPosition.x == secondX && _gameEntity.arrayPosition.y == secondY)
                        {
                            GameEntity e2 = _gameEntity;
                            if (e2 != null)
                            {
                                // Debug.LogFormat("Ent 1 = {0}, ent2 = {1}", e.view.gameObject.name, e2.view.gameObject.name);
                                // Debug.LogFormat ("Found the second");
                                e.ReplaceMove (e2.arrayPosition.x, e2.arrayPosition.y);
                                e2.ReplaceMove (e.arrayPosition.x, e.arrayPosition.y);
                                // Debug.LogFormat("Not null");
                                break;
                            }
                        }
                    }
                }
            }
            e.RemoveFirstPosition ();
            e.RemoveLastPosition ();
        }
    }
}