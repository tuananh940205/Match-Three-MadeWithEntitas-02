using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class InitializeBoardSystem : ReactiveSystem<GameEntity>
{
    readonly GameContext _gameContext;
    string[] names = new string[] { "Blue", "Blue2", "Green", "Purple", "Red", "Yellow" };

    public InitializeBoardSystem (Contexts contexts) : base (contexts.game)
    {
        _gameContext = contexts.game;
    }

    protected override ICollector<GameEntity> GetTrigger (IContext<GameEntity> context)
    {
        return context.CreateCollector (GameMatcher.BoardSize);
    }

    protected override bool Filter (GameEntity entity)
    {
        return entity.hasBoardSize;
    }

    protected override void Execute (List<GameEntity> entities)
    {
        foreach (GameEntity e in entities)
        {
            string[, ] gameObjectNames = new string[e.boardSize.row, e.boardSize.column];
            for (int y = 0; y < e.boardSize.column; y++) {
                for (int x = 0; x < e.boardSize.row; x++)
                {
                    List<string> nameList = new List<string> ();
                    GameEntity _gameEntity = _gameContext.CreateEntity ();
                    nameList.AddRange (names);

                    if (x > 0) nameList.Remove (gameObjectNames[x - 1, y]);
                    if (y > 0) nameList.Remove (gameObjectNames[x, y - 1]);

                    string name = nameList[Random.Range (0, nameList.Count - 1)];
                    gameObjectNames[x, y] = name;
                    GameObject go = Resources.Load<GameObject> ("Prefabs/" + name);
                    _gameEntity.AddView (go);
                    _gameEntity.AddTileName (name);
                    _gameEntity.AddArrayPosition (x, y);
                }
            }
        }
    }
}