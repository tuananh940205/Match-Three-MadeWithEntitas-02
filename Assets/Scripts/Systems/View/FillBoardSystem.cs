using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class FillBoardSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _tileEntityGroup;
    string[] names = new string[]
    {
        "Blue",
        "Blue2",
        "Green",
        "Purple",
        "Red",
        "Yellow",
    };

    public FillBoardSystem (Contexts contexts) : base (contexts.game)
    {
        _gameContext = contexts.game;
        _tileEntityGroup = _gameContext.GetGroup (GameMatcher.View);
    }

    protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
    {
        return context.CreateCollector (GameMatcher.FillPosition);
    }

    protected override bool Filter (GameEntity entity)
    {
        return entity.hasFillPosition;
    }

    protected override void Execute (List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            Debug.LogFormat("FillBoardSystem, Execute x = {0}, y = {1}", e.fillPosition.x, e.fillPosition.y);
            // Debug.LogFormat("FillBoardSystem entities = {3}, Execute {0}, x = {1}, y = {2}", entities.Count, e.fillPosition.x, e.fillPosition.y, entities.Count);
            // GameEntity[] _tileEntities = _tileEntityGroup.GetEntities();
            // int a = 0;
            // for(int i = 0; i < _tileEntities.Length; i++)
            // {
            //     if(_tileEntities[i].arrayPosition.y == e.fillPosition.y)
            //     {
            //         if(a >= _tileEntities[i].arrayPosition.x)
            //         {
            //             a = _tileEntities[i].arrayPosition.x - 1;
            //         }
            //     }
            // }
            // Debug.LogFormat("a = {0}", a);
            // GameEntity _newTile = _gameContext.CreateEntity();
            // GameObject go = Resources.Load<GameObject>("Prefabs/" + names[Random.Range(0, names.Length - 1)]);
            // _newTile.AddView(go);
            // _newTile.AddTileName(go.name);
            // _newTile.AddArrayPosition(e.fillPosition.x , a);
            // Debug.LogFormat("gameObject {0} {1}", _newTile.view.gameObject.transform.position.x, _newTile.view.gameObject.transform.position.y);
        }
    }
}