using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

// public class ResetArrayPositionSystem : ReactiveSystem<GameEntity>
// {
//     readonly GameContext _gameContext;

//     public ResetArrayPositionSystem(Contexts contexts) : base(contexts.game)
//     {
//         _gameContext = contexts.game;
//     }

//     protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
//     {
//         return context.CreateCollector(GameMatcher.AllOf())
//     }
// }