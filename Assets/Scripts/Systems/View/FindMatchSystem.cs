using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class FindMatchSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity _boardEntity;

    public FindMatchSystem (Contexts contexts) : base (contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
    {
        return context.CreateCollector (GameMatcher.AllOf (GameMatcher.FindingMatch));
    }

    protected override bool Filter (GameEntity entity)
    {
        return entity.isFindingMatch;
    }

    protected override void Execute (List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            // Debug.LogFormat ("e = {0}, x = {1}, y = {2}", e.view.gameObject.name, e.arrayPosition.x, e.arrayPosition.y);
            List<GameEntity> matchEntityList = new List<GameEntity> ();
            List<GameEntity> temp1 = new List<GameEntity> ();
            GameEntity[] tileEntities = _gameContext.GetGroup (GameMatcher.View).GetEntities ();
            _boardEntity = _gameContext.boardSizeEntity;
            bool isFound = false;
            // Debug.LogFormat("row = {0}, column = {1}", _boardEntity.boardSize.row, _boardEntity.boardSize.column);
            GameEntity[, ] tempArray = new GameEntity[_boardEntity.boardSize.row, _boardEntity.boardSize.column];
            foreach (GameEntity _tileEntity in tileEntities)
            {
                if(_tileEntity.arrayPosition.x >= 0 && _tileEntity.arrayPosition.y >= 0)
                {
                    tempArray[_tileEntity.arrayPosition.x, _tileEntity.arrayPosition.y] = _tileEntity;
                }
            }
            // Debug.LogFormat("tileentities = {0}, temparray = {1}", tileEntities.Length, tempArray.Length);
            for (int y = 0; y < _boardEntity.boardSize.column; y++)
            {
                if (isFound) break;
                for (int x = 0; x < _boardEntity.boardSize.row; x++)
                {
                    if (tempArray[x, y] == e)
                    {
                        //check horizontal - left
                        if (x > 0)
                        {
                            for (int i = x - 1; i >= 0; i--)
                            {
                                if (tempArray[i, y].tileName.name != e.tileName.name) break;
                                temp1.Add (tempArray[i, y]);
                            }
                        }
                        // Check horizontal - right
                        if (x < _boardEntity.boardSize.row - 1)
                        {
                            for (int i = x + 1; i < _boardEntity.boardSize.row; i++)
                            {
                                if (tempArray[i, y].tileName.name != e.tileName.name) break;
                                temp1.Add(tempArray[i, y]);
                            }
                        }
                        ///
                        // Debug.LogFormat("temp1 horizontal = {0}, e = {1}", temp1.Count, e.tileName.name);
                        if (temp1.Count >= 2) matchEntityList.AddRange(temp1);
                        temp1.Clear ();
                        ///
                        //vertical - up
                        if (y > 0)
                        {
                            for (int i = y - 1; i >= 0; i--)
                            {
                                // if(tempArray[x, i].tileName.name == null)
                                // {
                                //     Debug.LogFormat("1 null");
                                // }
                                // if(e.tileName.name == null)
                                // {
                                //     Debug.LogFormat("2 null");
                                // }
                                if (tempArray[x, i].tileName.name != e.tileName.name) break;
                                temp1.Add (tempArray[x, i]);
                            }
                        }
                        // vertical - down
                        if (y < _boardEntity.boardSize.column - 1)
                        {
                            for (int i = y + 1; i < _boardEntity.boardSize.column; i++)
                            {
                                if (tempArray[x, i].tileName.name != e.tileName.name) break;
                                temp1.Add (tempArray[x, i]);
                            }
                        }
                        // Debug.LogFormat("temp1 vertical = {0}, e = {1}", temp1.Count, e.tileName.name);
                        if (temp1.Count >= 2) matchEntityList.AddRange(temp1);
                        isFound = true;
                        break;
                    }
                }
            }
            // Debug.LogFormat ("matchEntityCount = {0}", matchEntityList.Count);
            if (matchEntityList.Count >= 2)
            {
                Debug.LogFormat ("Matched");
                matchEntityList.Add (e);
                foreach (GameEntity _matchEntity in matchEntityList)
                {
                    _matchEntity.isFaded = true;
                }
                _boardEntity.isClearBoard = true;
            }
            else
            {
                // Debug.LogFormat("Unmatched");
                if (!_boardEntity.isClearBoard)
                {
                    Debug.LogFormat ("Not found any match");
                }
            }
            // Debug.LogFormat("matchEntityList = {0}, e = {1}", matchEntityList.Count, e.tileName.name);
        }
    }
}