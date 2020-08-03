using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class MoveSystem : IExecuteSystem
{
    Vector2 startPosition = new Vector2 (-1.75f, .95f);
    Vector2 offset = Resources.Load<GameObject> ("Prefabs/Blue").GetComponent<SpriteRenderer> ().bounds.size;
    readonly IGroup<GameEntity> _moves;
    // readonly IGroup<GameEntity> _moving;
    const float _speed = 2f;

    public MoveSystem (Contexts contexts)
    {
        _moves = contexts.game.GetGroup (GameMatcher.Move);
        // _moving = contexts.game.GetGroup(GameMatcher.Moving);
    }

    public void Execute ()
    {
        foreach (GameEntity e in _moves.GetEntities ())
        {
            // Debug.LogFormat("MoveSystem: {0}", _moves.GetEntities().Length);
            Vector2 target = new Vector2 (startPosition.x + e.move.x * offset.x, startPosition.y - e.move.y * offset.y);
            // Debug.LogFormat("MoveSystem, Execute");
            // Debug.LogFormat(e.view.gameObject.name + "");
            // Debug.LogFormat("_moves = {0}", _moves.GetEntities().Length);
            Vector2 goPos = new Vector2 (e.view.gameObject.transform.position.x, e.view.gameObject.transform.position.y);
            Vector2 dir = target - goPos;
            Vector2 newPos = goPos + dir.normalized * _speed * Time.deltaTime;
            // Debug.LogFormat("dis = {0}", 1 * _speed * Time.deltaTime);
            float dist = dir.magnitude;
            if (dist <= _speed * Time.deltaTime)
            {
                // Debug.LogFormat("MoveComplete");
                // e.view.gameObject.transform.position = target;
                e.ReplacePosition (target);
                e.arrayPosition.x = e.move.x;
                e.arrayPosition.y = e.move.y;
                e.RemoveMove ();
                e.isFindingMatch = true;
                // foreach(GameEntity en in _moving.GetEntities())
                // {
                //     en.RemoveMoving();
                // }
            }
            else
            {
                e.ReplacePosition (newPos);
            }
        }
    }
}