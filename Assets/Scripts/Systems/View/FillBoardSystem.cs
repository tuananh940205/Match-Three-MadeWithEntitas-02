using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class FillBoardSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _tileEntityGroup;
    string[] names = new string[] { "Blue", "Blue2", "Green", "Purple", "Red", "Yellow" };

    public FillBoardSystem(Contexts contexts) : base(contexts.game)
    {
        _gameContext = contexts.game;
        _tileEntityGroup = _gameContext.GetGroup(GameMatcher.View);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BoardSize, GameMatcher.TileColumnFill));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasTileColumnFill;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            for(int i = 0; i < e.tileColumnFill.tileNumber.Length; i++)
            {
                if(e.tileColumnFill.tileNumber[i] > 0)
                {
                    for(int j = 0; j < e.tileColumnFill.tileNumber[i]; j++)
                    {
                        GameEntity _newTile = _gameContext.CreateEntity();
                        string name = names[Random.Range(0, names.Length - 1)];
                        GameObject go = Resources.Load<GameObject>("Prefabs/" + name);
                        _newTile.AddView(go);
                        _newTile.AddTileName(name);
                        _newTile.AddArrayPosition(i, -j - 1);
                        _newTile.view.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
                    }
                }
            }
            
            int[] temp = e.tileColumnFill.tileNumber;
            e.RemoveTileColumnFill();
            e.ReplaceTileColumnPull(temp);
        }
    }
}