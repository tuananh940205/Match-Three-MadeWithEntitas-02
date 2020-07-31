using Entitas;
using UnityEngine;

public class BoardSizeSystem : IInitializeSystem
{
    readonly GameContext _gameContext;
    private GameEntity _boardEntity;

    public BoardSizeSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }

    public void Initialize()
    {
        // _boardEntity = _gameContext.CreateEntity();
        // _gameContext. = true;
        // _boardEntity = _gameContext.boardColumnEntity;
        // _boardEntity = _gameContext.boardRowEntity;
        // GameEntity e = _gameContext.CreateEntity();
        // e.AddBoardRow(8);
        // e.AddBoardColumn(10);
        GameEntity e = _gameContext.CreateEntity();
        
    }
}