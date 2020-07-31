using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class FillBoardSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _tileEntity;

    public FillBoardSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        _tileEntity = _gameContext.GetGroup(GameMatcher.View);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BoardRow, GameMatcher.BoardColumn, GameMatcher.FillPosition));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasFillPosition;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            Debug.LogFormat("FillBoardSystem entities = {3}, Execute {0}, x = {1}, y = {2}", entities.Count, e.fillPosition.x, e.fillPosition.y, entities.Count);
            GameEntity[] _tileEntities = _tileEntity.GetEntities();
            List<GameEntity> upperList = new List<GameEntity>();
            foreach(GameEntity _tileEntity in _tileEntities)
            {
                if(_tileEntity.arrayPosition.x == e.fillPosition.x)
                {
                    if(_tileEntity.arrayPosition.y < e.fillPosition.y)
                    {
                        Debug.LogFormat("x = {0}, y = {1}", _tileEntity.arrayPosition.x, _tileEntity.arrayPosition.y);
                        upperList.Add(_tileEntity);
                    }
                }
            }
            Debug.LogFormat("upperList = {0}", upperList.Count);
        }
    }
}
