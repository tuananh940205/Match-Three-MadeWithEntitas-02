using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class PullBoardSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;

    public PullBoardSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.TileColumnPull);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasTileColumnPull;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            GameEntity[] _tileEntities = _gameContext.GetGroup(GameMatcher.View).GetEntities();
            GameEntity[,] _tilesArray = new GameEntity[e.boardSize.row, e.boardSize.column];
            int[] maxY = new int[e.boardSize.row];
            List<GameEntity> _negativeTileList = new List<GameEntity>();

            // grab the max depth
            for(int i = 0; i < maxY.Length; i++)
            {
                maxY[i] = 0;
                // negativeTiles[i] = 0;
            }
            foreach(GameEntity entity in _tileEntities)
            {
                if(entity.arrayPosition.x >= 0 && entity.arrayPosition.y >= 0)
                {
                    _tilesArray[entity.arrayPosition.x, entity.arrayPosition.y] = entity;
                }
                else
                {
                    Debug.LogFormat("x = {0}, y = {1}", entity.arrayPosition.x, entity.arrayPosition.y);
                    _negativeTileList.Add(entity);
                    // negativeTiles[entity.arrayPosition.x]++;
                }
            }
            // for(int i = 0; i < negativeTiles.Length; i++)
            // {
            //     if(negativeTiles[i] > 0)
            //     {
            //         Debug.LogFormat("i = {0}, x = {1}", i, negativeTiles[i]);
            //     }
            // }
            for(int y = 0; y < _tilesArray.GetLength(1); y++)
            {
                for(int x = 0; x < _tilesArray.GetLength(0); x++)
                {
                    if(_tilesArray[x, y] == null)
                    {
                        // Debug.LogFormat("Null {0} {1}", x, y);
                        if(maxY[x] < y)
                        {
                            maxY[x] = y;
                        }
                    }
                }
            }
            // for(int i = 0; i < maxY.Length; i++)
            // {
            //     Debug.LogFormat("Index {0} y = {1}", i, maxY[i]);
            // }
            for(int i = 0; i < e.tileColumnPull.tileNumber.Length; i++)
            {
                if(e.tileColumnPull.tileNumber[i] > 0)
                {
                    List<GameEntity> _pullDownList = new List<GameEntity>();

                    for(int j = maxY[i] - 1; j >= 0; j--)
                    {
                        if(_tilesArray[i, j] != null)
                        {
                            _pullDownList.Add(_tilesArray[i, j]);
                        }
                    }

                    // if(_negativeTileList.Count > 0)
                    // {
                    //     for(int j = 0; j < _negativeTileList.Count; j++)
                    //     {
                    //         if(_negativeTileList[j].arrayPosition.x == i)
                    //         {
                    //             _pullDownList.Add(_negativeTileList[j]);
                    //             _negativeTileList.Remove(_negativeTileList[j]);
                    //         }
                    //     }
                    // }
                    // Another way to sort

                    if(_negativeTileList.Count > 0)
                    {
                        for(int j = 0; j < _negativeTileList.Count; j++)
                        {
                            
                        }
                    }


                    // Debug.LogFormat("index {0} total {1}", i, _pullDownList.Count);
                }
            }
        }
    }
}