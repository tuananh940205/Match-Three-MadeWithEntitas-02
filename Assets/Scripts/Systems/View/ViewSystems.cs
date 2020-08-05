public class ViewSystems : Feature
{
    public ViewSystems(Contexts contexts) : base("View Systems")
    {
        // Initialize
        Add(new BoardSizeSystem(contexts));

        // Execute
        Add(new MoveSystem(contexts));

        // ReactiveSystem
        Add(new InitializeBoardSystem(contexts));
        Add(new AddViewSystem(contexts));
        Add(new AddNameSystem(contexts));
        Add(new RenderPositionSystem(contexts));
        Add(new RenderArrayPositionSystem(contexts));
        Add(new FindAdjacentSystem(contexts));
        Add(new FindMatchSystem(contexts));
        Add(new ReverseTileSystem(contexts));
        Add(new ClearMatchSystem(contexts));
        Add(new FillBoardSystem(contexts));

        // Cleanup
        Add(new CleanupSystem(contexts));
    }
}