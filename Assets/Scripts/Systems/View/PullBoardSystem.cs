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

            foreach(GameEntity entity in _tileEntities)
            {
                if(entity.arrayPosition.x >= 0 && entity.arrayPosition.y >= 0)
                {
                    _tilesArray[entity.arrayPosition.x, entity.arrayPosition.y] = entity;
                }
                else
                {
                    // Debug.LogFormat("x = {0}, y = {1}", entity.arrayPosition.x, entity.arrayPosition.y);
                    _negativeTileList.Add(entity);
                    // negativeTiles[entity.arrayPosition.x]++;
                }
            }

            for(int x = 0; x < _tilesArray.GetLength(0); x++)
            {
                for(int y = 0; y < _tilesArray.GetLength(1); y++)
                {
                    if(_tilesArray[x, y] == null)
                    {
                        if(maxY[x] < y)
                        {
                            maxY[x] = y;
                        }
                    }
                }
            }

            // Grab the list of tile could be pull down
            for(int i = 0; i < e.tileColumnPull.tileNumber.Length; i++)
            {
                if(e.tileColumnPull.tileNumber[i] > 0)
                {
                    List<GameEntity> _listPullDown = new List<GameEntity>();
                    List<GameEntity> _sameNegative = new List<GameEntity>();
                    // Debug.LogFormat("negative tile list {0}", _negativeTileList.Count);
                    foreach(GameEntity entity in _negativeTileList)
                    {
                        if(entity.arrayPosition.x == i)
                        {
                            // Debug.LogFormat("Add up");
                            _sameNegative.Add(entity);
                            // entity.ReplaceTileName(entity.arrayPosition.y + "");
                            // _negativeTileList.Remove(entity);
                        }
                    }

                    // Add upper
                    int count = -1;
                    for(int j = 0; j < _sameNegative.Count; j++)
                    {
                        for(int k = 0; k < _sameNegative.Count; k++)
                        {
                            if(_sameNegative[k].arrayPosition.y == count)
                            {
                                // Debug.LogFormat("Add down");
                                _listPullDown.Add(_sameNegative[k]);
                                // _sameNegative.Remove(_sameNegative[k]);
                                // _sameNegative[k].ReplaceTileName(_sameNegative[k].arrayPosition.y + "Up");
                                count--;
                                break;
                            }
                        }
                        if(_sameNegative.Count == 0) break;
                    }
                    _listPullDown.Reverse();

                    // Add down
                    // Debug.LogFormat("maxY {0}", maxY[i]);
                    for(int j = 0; j < maxY[i]; j++)
                    {
                        if(_tilesArray[i, j] != null)
                        {
                            _listPullDown.Add(_tilesArray[i, j]);
                            // _tilesArray[i, j].ReplaceTileName(_tilesArray[i, j].arrayPosition.y + "Down");
                        }
                    }

                    // Debug.LogFormat("listpulldown {0}", _listPullDown.Count);
                    for(int j = 0; j < _listPullDown.Count; j++)
                    {
                        // Debug.LogFormat("i {0}, e {1} {2}", j, _listPullDown[j].arrayPosition.x, _listPullDown[j].arrayPosition.y);
                    }

                    for(int j = 0; j < _listPullDown.Count; j++)
                    {
                        _listPullDown[j].ReplaceFallDown(i, j);
                    }
                }
            }
            e.RemoveTileColumnPull();
        }
    }
}