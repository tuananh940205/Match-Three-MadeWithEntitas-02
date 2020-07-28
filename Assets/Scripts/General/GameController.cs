using Entitas;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Systems _systems;

    void Start()
    {
        Contexts contexts = Contexts.sharedInstance;
        _systems = CreateSystems(contexts);
        _systems.Initialize();
    }

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    Systems CreateSystems(Contexts contexts)
    {
        return new Feature("Total Systems")
            .Add(new InputSystems(contexts))
            .Add(new ViewSystems(contexts));
    }
}