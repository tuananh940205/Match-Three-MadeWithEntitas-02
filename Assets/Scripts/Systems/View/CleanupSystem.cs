using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class CleanupSystem : ICleanupSystem
{
    readonly GameContext _gameContext;
    readonly IGroup<GameEntity> _isFindingMatch;

    public CleanupSystem(Contexts contexts)
    {
        _isFindingMatch = contexts.game.GetGroup(GameMatcher.FindingMatch);
    }

    public void Cleanup()
    {
        foreach(GameEntity e in _isFindingMatch.GetEntities())
        {
            e.isFindingMatch = false;
        }
    }
}
