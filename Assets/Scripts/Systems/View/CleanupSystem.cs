using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CleanupSystem : ICleanupSystem
{
    readonly GameContext _gameContext;
    IGroup<GameEntity> _isFillingBoard;

    public CleanupSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
        _isFillingBoard = _gameContext.GetGroup(GameMatcher.TileColumnFill);
    }

    public void Cleanup()
    {
        foreach(GameEntity e in _isFillingBoard.GetEntities())
        {
            // int[] temp = e.tileColumnFill.tileNumber;
            // e.RemoveTileColumnFill();
            // e.ReplaceTileColumnPull(temp);
        }
    }
}