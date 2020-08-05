using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class FindMatchSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    GameEntity _boardEntity;
    IGroup<GameEntity> _findingMatchGroup;
    // IGroup<GameEntity> _isFindingMatch;

    public FindMatchSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        _findingMatchGroup = _gameContext.GetGroup(GameMatcher.FindingMatch);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.FindingMatch));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isFindingMatch;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            // Debug.LogFormat ("e = {0}, x = {1}, y = {2}", e.view.gameObject.name, e.arrayPosition.x, e.arrayPosition.y);
            List<GameEntity> matchEntityList = new List<GameEntity>();
            List<GameEntity> temp1 = new List<GameEntity>();
            GameEntity[] tileEntities = _gameContext.GetGroup(GameMatcher.View).GetEntities();
            _boardEntity = _gameContext.boardSizeEntity;
            bool isFound = false;
            // Debug.LogFormat("row = {0}, column = {1}", _boardEntity.boardSize.row, _boardEntity.boardSize.column);
            GameEntity[,] tempArray = new GameEntity[_boardEntity.boardSize.row, _boardEntity.boardSize.column];
            foreach(GameEntity _tileEntity in tileEntities)
            {
                if(_tileEntity.arrayPosition.x >= 0 && _tileEntity.arrayPosition.y >= 0)
                {
                    tempArray[_tileEntity.arrayPosition.x, _tileEntity.arrayPosition.y] = _tileEntity;
                }
            }
            // Debug.LogFormat("tileentities = {0}, temparray = {1}", tileEntities.Length, tempArray.Length);
            for(int y = 0; y < _boardEntity.boardSize.column; y++)
            {
                if(isFound) break;
                for(int x = 0; x < _boardEntity.boardSize.row; x++)
                {
                    if(tempArray[x, y] == e)
                    {
                        //check horizontal - left
                        if(x > 0)
                        {
                            for(int i = x - 1; i >= 0; i--)
                            {
                                if(tempArray[i, y].tileName.name != e.tileName.name) break;
                                temp1.Add (tempArray[i, y]);
                            }
                        }
                        // Check horizontal - right
                        if(x < _boardEntity.boardSize.row - 1)
                        {
                            for(int i = x + 1; i < _boardEntity.boardSize.row; i++)
                            {
                                if(tempArray[i, y].tileName.name != e.tileName.name) break;
                                temp1.Add(tempArray[i, y]);
                            }
                        }
                        ///
                        // Debug.LogFormat("temp1 horizontal = {0}, e = {1}", temp1.Count, e.tileName.name);
                        if(temp1.Count >= 2) matchEntityList.AddRange(temp1);
                        temp1.Clear();
                        ///
                        //vertical - up
                        if(y > 0)
                        {
                            for (int i = y - 1; i >= 0; i--)
                            {
                                if (tempArray[x, i].tileName.name != e.tileName.name) break;
                                temp1.Add (tempArray[x, i]);
                            }
                        }
                        // vertical - down
                        if(y < _boardEntity.boardSize.column - 1)
                        {
                            for (int i = y + 1; i < _boardEntity.boardSize.column; i++)
                            {
                                if (tempArray[x, i].tileName.name != e.tileName.name) break;
                                temp1.Add (tempArray[x, i]);
                            }
                        }
                        // Debug.LogFormat("temp1 vertical = {0}, e = {1}", temp1.Count, e.tileName.name);
                        if(temp1.Count >= 2) matchEntityList.AddRange(temp1);
                        isFound = true;
                        break;
                    }
                }
            }
            // Debug.LogFormat ("matchEntityCount = {0}", matchEntityList.Count);
            if(matchEntityList.Count >= 2)
            {
                Debug.LogFormat("Matched Event Execution Here");
                // e.isMatch = true;
                matchEntityList.Add(e);
                foreach(GameEntity _matchEntity in matchEntityList)
                {
                    _matchEntity.isFaded = true;
                }
                if(!_boardEntity.isClearBoard)
                {
                    _boardEntity.isClearBoard = true;
                }
            }
            else
            {
                // IF MATCH FAILURE
                // Debug.LogFormat("Unmatched");
                GameEntity[] entitiesWithFindingMatch = _findingMatchGroup.GetEntities();
                List<GameEntity> unmatchList = new List<GameEntity>();
                if(entitiesWithFindingMatch.Length == 2)
                {
                    // Debug.LogFormat("Unmatched");
                }
                foreach(GameEntity en in entitiesWithFindingMatch)
                {
                    if(!en.isMatch)
                    {
                        unmatchList.Add(en);
                    }
                }
                // Debug.LogFormat("List count {0}", unmatchList.Count);
                if(unmatchList.Count == 2)
                {
                    // _boardEntity.isTileReverse = true;
                    // Debug.LogFormat("Swap backkk");
                    int x1 = unmatchList[1].arrayPosition.x;
                    int y1 = unmatchList[1].arrayPosition.y;
                    int x2 = unmatchList[0].arrayPosition.x;
                    int y2 = unmatchList[0].arrayPosition.y;
                    unmatchList[0].ReplaceMoveBackWithoutMatch(x1, y1);
                    unmatchList[1].ReplaceMoveBackWithoutMatch(x2, y2);

                }
                // Debug.LogFormat("entitiesWithFindingMatch count = {0}", entitiesWithFindingMatch.Length);
                // List<GameEntity> isMatchListEntities = new List<GameEntity>();
                // foreach(GameEntity entity in entitiesWithFindingMatch)
                // {
                //     if(!entity.isMatch)
                //     {
                //         isMatchListEntities.Add(entity);
                //     }
                // }
                // Debug.LogFormat("Count = {0}", isMatchListEntities.Count);
                // if(isMatchListEntities.Count == 2)
                // {
                //     Debug.LogFormat("Failed");
                // }
                // if(!_boardEntity.isClearBoard)
                // {
                //     GameEntity[] failedTileEntities = _failEntityGroup.GetEntities();
                //     if(failedTileEntities.Length == 2)
                //     {
                //         int x1 = failedTileEntities[0].arrayPosition.x;
                //         int y1 = failedTileEntities[0].arrayPosition.y;
                //         int x2 = failedTileEntities[1].arrayPosition.x;
                //         int y2 = failedTileEntities[1].arrayPosition.y;
                //         failedTileEntities[0].ReplaceMove(x2, y2);
                //         failedTileEntities[1].ReplaceMove(x1, y1);
                        
                //     }
                // }
            }
            // Debug.LogFormat("matchEntityList = {0}, e = {1}", matchEntityList.Count, e.tileName.name);
            e.isFindingMatch = false;
        }
    }
}