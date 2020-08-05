using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class ReverseTileSystem : IExecuteSystem
{
    Vector2 startPosition = new Vector2(-1.75f, .95f);
    Vector2 offset = Resources.Load<GameObject>("Prefabs/Blue").GetComponent<SpriteRenderer>().bounds.size;
    readonly IGroup<GameEntity> _mover;
    readonly GameContext _gameContext;
    IGroup<GameEntity> _unmatchTile;
    const float _speed = 3f;

    public ReverseTileSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
        _mover = _gameContext.GetGroup(GameMatcher.MoveBackWithoutMatch);
    }

    public void Execute()
    {
        foreach(GameEntity e in _mover.GetEntities())
        {
            Vector2 target = new Vector2(startPosition.x + e.moveBackWithoutMatch.x * offset.x, startPosition.y - e.moveBackWithoutMatch.y * offset.y);
            Vector2 goPos = new Vector2(e.view.gameObject.transform.position.x, e.view.gameObject.transform.position.y);
            Vector2 dir = target - goPos;
            Vector2 newPos = goPos + dir.normalized * _speed * Time.deltaTime;
            float dist = dir.magnitude;

            if(dist <= _speed * Time.deltaTime)
            {
                e.ReplacePosition(target);
                e.arrayPosition.x = e.moveBackWithoutMatch.x;
                e.arrayPosition.y = e.moveBackWithoutMatch.y;
                e.RemoveMoveBackWithoutMatch();
            }
            else
            {
                e.ReplacePosition(newPos);
            }
        }
    }
}
