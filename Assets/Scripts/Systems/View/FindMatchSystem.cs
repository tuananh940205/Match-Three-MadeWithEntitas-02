using UnityEngine;
using Entitas;
using System.Collections.Generic;


public class FindMatchSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _board;

    public FindMatchSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        _board = _gameContext.GetGroup(GameMatcher.AllOf(GameMatcher.BoardColumn, GameMatcher.BoardRow));
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
        // Debug.LogFormat("FindMatchSystem, Execute, {0}", entities.Count);
        GameEntity[] boardEntities = _board.GetEntities();
        // Debug.LogFormat("Length = {0}", boardEntities.Length);
        foreach(GameEntity e in entities)
        {
            // Debug.LogFormat("e = {0}, x = {1}, y = {2}", e.view.gameObject.name, e.arrayPosition.x, e.arrayPosition.y);
            List<GameEntity> matchEntityList = new List<GameEntity>();
            List<GameEntity> temp1 = new List<GameEntity>();
            // List<GameEntity> temp2 = new List<GameEntity>();

            GameEntity[] tileEntities = _gameContext.GetGroup(GameMatcher.View).GetEntities();

            GameEntity[,] tempArray = new GameEntity[boardEntities[0].boardRow.value, boardEntities[0].boardColumn.value];

            foreach(GameEntity _tileEntity in tileEntities)
            {
                tempArray[_tileEntity.arrayPosition.x, _tileEntity.arrayPosition.y] = _tileEntity;
            }
            // Debug.LogFormat("tileentities = {0}, temparray = {1}", tileEntities.Length, tempArray.Length);
            bool isFound = false;
            for(int y = 0; y < boardEntities[0].boardColumn.value; y++)
            {
                if(isFound) break;
                for(int x = 0; x < boardEntities[0].boardRow.value; x++)
                {
                    if(tempArray[x, y] == e)
                    {
                        //check horizontal - left
                        if(x > 0)
                        {
                            for(int i = x - 1; i >= 0; i--)
                            {
                                if(tempArray[i, y].tileName.name != e.tileName.name) break;
                                temp1.Add(tempArray[i, y]);
                            }
                        }
                        // Check horizontal - right
                        if(x < boardEntities[0].boardRow.value - 1)
                        {
                            for(int i = x + 1; i < boardEntities[0].boardRow.value; i++)
                            {
                                if(tempArray[i, y].tileName.name != e.tileName.name) break;
                                temp1.Add(tempArray[i, y]);
                            }
                        }
                        ///
                        Debug.LogFormat("temp1 horizontal = {0}, e = {1}", temp1.Count, e.tileName.name);
                        if(temp1.Count >= 2) matchEntityList.AddRange(temp1);

                        temp1.Clear();
                        
                        ///
                        //vertical - up

                        if(y > 0)
                        {
                            for(int i = y - 1; i >= 0; i--)
                            {
                                if(tempArray[x, i].tileName.name != e.tileName.name) break;
                                temp1.Add(tempArray[x, i]);
                            }
                        }
                        // vertical - down
                        if(y < boardEntities[0].boardColumn.value - 1)
                        {
                            for(int i = y + 1; i < boardEntities[0].boardColumn.value; i++)
                            {
                                if(tempArray[x, i].tileName.name != e.tileName.name) break;
                                temp1.Add(tempArray[x, i]);
                            }
                        }
                        // Debug.LogFormat("temp1 vertical = {0}, e = {1}", temp1.Count, e.tileName.name);
                        if(temp1.Count >= 2) matchEntityList.AddRange(temp1);
                        isFound = true;
                        break;
                    }
                }
            }
            if(matchEntityList.Count >= 2)
            {
                matchEntityList.Add(e);
                foreach(GameEntity _matchEntity in matchEntityList)
                {
                    _matchEntity.isFaded = true;
                }
            }
            // Debug.LogFormat("matchEntityList = {0}, e = {1}", matchEntityList.Count, e.tileName.name);
        }
    }
}