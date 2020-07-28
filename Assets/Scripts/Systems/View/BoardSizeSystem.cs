using UnityEngine;
using Entitas;

public class BoardSizeSystem : IInitializeSystem
{
    readonly GameContext _gameContexts;

    public BoardSizeSystem(Contexts contexts)
    {
        _gameContexts = contexts.game;
    }

    public void Initialize()
    {
        GameEntity e = _gameContexts.CreateEntity();
        e.AddBoardRow(8);
        e.AddBoardColumn(10);
    }
}