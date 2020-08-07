using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class TileFallSystem : IExecuteSystem
{
    readonly GameContext _gameContext;
    IGroup<GameEntity> _tilesFall;
    Vector2 startPosition = new Vector2(-1.75f, .95f);
    Vector2 offset = Resources.Load<GameObject>("Prefabs/Blue").GetComponent<SpriteRenderer>().bounds.size;
    const float _speed = 3f;

    public TileFallSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
        _tilesFall = _gameContext.GetGroup(GameMatcher.FallDown);
    }

    public void Execute()
    {
        foreach(GameEntity e in _tilesFall.GetEntities())
        {
            Debug.LogFormat("faillll");
            Vector2 target = new Vector2(startPosition.x + e.fallDown.x * offset.x, startPosition.y - e.fallDown.y * offset.y);
            Vector2 goPos = new Vector2(e.view.gameObject.transform.position.x, e.view.gameObject.transform.position.y);
            Debug.LogFormat("pos {0} {1}", e.view.gameObject.transform.position.x, e.view.gameObject.transform.position.y);
            Vector2 dir = target - goPos;
            Vector2 newPos = goPos + dir.normalized * _speed * Time.deltaTime;
            // Debug.LogFormat("dis = {0}", 1 * _speed * Time.deltaTime);
            float dist = dir.magnitude;

            if(dist <= _speed * Time.deltaTime)
            {
                e.ReplaceArrayPosition(e.fallDown.x, e.fallDown.y);
                e.RemoveFallDown();
            }
            else
            {
                e.ReplacePosition(newPos);
            }
        }
    }
}
