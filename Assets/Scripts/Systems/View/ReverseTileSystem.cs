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
            // Debug.LogFormat("MoveSystem: {0}", _mover.GetEntities().Length);
            Vector2 target = new Vector2(startPosition.x + e.moveByUserInput.x * offset.x, startPosition.y - e.moveByUserInput.y * offset.y);
            // Debug.LogFormat("MoveSystem, Execute");
            // Debug.LogFormat(e.view.gameObject.name + "");
            // Debug.LogFormat("_mover = {0}", _mover.GetEntities().Length);
            Vector2 goPos = new Vector2(e.view.gameObject.transform.position.x, e.view.gameObject.transform.position.y);
            Vector2 dir = target - goPos;
            Vector2 newPos = goPos + dir.normalized * _speed * Time.deltaTime;
            // Debug.LogFormat("dis = {0}", 1 * _speed * Time.deltaTime);
            float dist = dir.magnitude;

            if(dist <= _speed * Time.deltaTime)
            {
                // Debug.LogFormat("MoveComplete");
                e.ReplacePosition(target);
                e.arrayPosition.x = e.moveByUserInput.x;
                e.arrayPosition.y = e.moveByUserInput.y;
                e.RemoveMoveByUserInput();
            }
            else
            {
                e.ReplacePosition(newPos);
            }
        }
    }
}
