using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class InitializeBoardSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    string[] names = new string[]
    {
        "Blue",
        "Blue2",
        "Green",
        "Purple",
        "Red",
        "Yellow",
    };

    public InitializeBoardSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BoardColumn, GameMatcher.BoardRow));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasBoardRow && entity.hasBoardColumn;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach(GameEntity e in entities)
        {
            for(int y = 0; y < e.boardColumn.value; y++)
            {
                for(int x = 0; x < e.boardRow.value; x++)
                {
                    GameObject go = Resources.Load<GameObject>("Prefabs/" + names[Random.Range(0, names.Length - 1)]);
                    GameEntity _gameEntity = _gameContext.CreateEntity();
                    _gameEntity.AddView(go);
                    _gameEntity.AddArrayPosition(x, y);
                    
                }
            }
        }
    }
}