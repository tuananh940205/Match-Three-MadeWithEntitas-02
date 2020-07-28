public class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts) : base("View Systems")
    {
        // Initialize
        Add(new BoardSizeSystem(contexts));

        // Execute
        
        // ReactiveSystem
        Add(new InitializeBoardSystem(contexts));
        Add(new AddViewSystem(contexts));
        Add(new RenderPositionSystem(contexts));
        Add(new TileMovingSystem(contexts));
        // Cleanup
    }
}