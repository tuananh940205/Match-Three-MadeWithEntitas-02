using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CleanupSystem : ICleanupSystem {
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _isFindingMatch;
    // readonly IGroup<GameEntity> _isFillingBoard;

    public CleanupSystem (Contexts contexts) {
        _gameContext = contexts.game;
        _isFindingMatch = _gameContext.GetGroup (GameMatcher.FindingMatch);
        // _isFillingBoard = _gameContext.GetGroup(GameMatcher.FillPosition);
    }

    public void Cleanup ()
    {
        foreach (GameEntity e in _isFindingMatch.GetEntities ())
        {
            e.isFindingMatch = false;
        }
        // foreach(GameEntity e in _isFillingBoard.GetEntities())
        // {
        //     e.Destroy();
        // }
    }
}