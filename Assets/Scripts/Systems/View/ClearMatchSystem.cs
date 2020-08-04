using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class ClearMatchSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    IGroup<GameEntity> _fadeTile;
    IGroup<GameEntity> _groupTileEntities;
    string[] names = new string[] { "Blue", "Blue2", "Green", "Purple", "Red", "Yellow" };

    public ClearMatchSystem(Contexts contexts) : base (contexts.game)
    {
        _gameContext = contexts.game;
        _fadeTile = _gameContext.GetGroup(GameMatcher.Faded);
        _groupTileEntities = _gameContext.GetGroup(GameMatcher.View);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ClearBoard);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isClearBoard;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            // Debug.LogFormat("TileFadedSystem, Execute");
            // Debug.LogFormat("Count = {0}", _fadeTile.GetEntities().Length);
            int[] additionalArr = new int[e.boardSize.column];
            for(int i = 0; i < additionalArr.Length; i++)
            {
                additionalArr[i] = 0;
            }
            int count = _fadeTile.GetEntities().Length;
            foreach (GameEntity en in _fadeTile.GetEntities())
            {
                int yMax = 0;
                // Debug.LogFormat ("Clearedddd");
                en.view.gameObject.GetComponent<SpriteRenderer>().DOFade(0, .65f).OnComplete(() => 
                {
                    // GameEntity _newGameEntity = _gameContext.CreateEntity();
                    if(yMax < en.arrayPosition.y) yMax = en.arrayPosition.y;

                    additionalArr[en.arrayPosition.x]++;
                    en.view.gameObject.Unlink();
                    Object.Destroy(en.view.gameObject);
                    en.Destroy();
                    if(count == 1)
                    {
                        for(int i = 0; i < additionalArr.Length; i++)
                        {
                            if(additionalArr[i] > 0)
                            {
                                List<GameEntity> _pullDownTiles = new List<GameEntity>();
                                for(int j = 0; j < additionalArr[i]; j++)
                                {
                                    GameEntity _newTile = _gameContext.CreateEntity();
                                    string name = names[Random.Range(0, names.Length - 1)];
                                    GameObject go = Resources.Load<GameObject>("Prefabs/" + name);
                                    _newTile.AddView(go);
                                    _newTile.AddTileName(name);
                                    _newTile.AddArrayPosition(i, -j - 1);
                                    _newTile.view.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                                    _pullDownTiles.Add(_newTile);
                                }
                                // grab the rest of list
                                GameEntity[] _tileEntities = _groupTileEntities.GetEntities();
                                foreach(GameEntity _tileEntity in _tileEntities)
                                {
                                    if(_tileEntity.arrayPosition.x == i)
                                    {
                                        if(_tileEntity.arrayPosition.y >= 0 && _tileEntity.arrayPosition.y < yMax)
                                        {
                                            _pullDownTiles.Add(_tileEntity);
                                        }
                                    }
                                }
                                // Debug.LogFormat("Pulldown tile = {0}", _pullDownTiles.Count);
                                //pull tiles here
                                foreach(GameEntity _tileEntity in _pullDownTiles)
                                {
                                    _tileEntity.ReplaceMove(_tileEntity.arrayPosition.x, _tileEntity.arrayPosition.y + additionalArr[i]);
                                }
                                e.isClearBoard = false;
                            }
                        }
                    }
                    else
                    {
                        count--;
                    }
                });
            }
            
        }
    }
}