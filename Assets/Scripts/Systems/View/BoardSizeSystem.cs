using Entitas;
using UnityEngine;

public class BoardSizeSystem : IInitializeSystem {
    readonly GameContext _gameContext;
    private GameEntity _boardEntity;

    public BoardSizeSystem (Contexts contexts) {
        _gameContext = contexts.game;
    }

    public void Initialize () {
        _boardEntity = _gameContext.SetBoardSize (8, 10);
    }
}