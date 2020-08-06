using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.Unity;

public class TileFallSystem : IExecuteSystem
{
    readonly GameContext _gameContext;

    public TileFallSystem(Contexts contexts)
    {
        _gameContext = contexts.game;
    }

    public void Execute()
    {

    }
}
