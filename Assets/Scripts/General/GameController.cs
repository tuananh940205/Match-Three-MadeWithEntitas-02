using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Systems _systems;

    void Start()
    {
        Contexts contexts = Contexts.sharedInstance;
        _systems.Initialize();
    }

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }
}