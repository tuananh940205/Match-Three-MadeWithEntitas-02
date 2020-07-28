//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public BoardColumnComponent boardColumn { get { return (BoardColumnComponent)GetComponent(GameComponentsLookup.BoardColumn); } }
    public bool hasBoardColumn { get { return HasComponent(GameComponentsLookup.BoardColumn); } }

    public void AddBoardColumn(int newValue) {
        var index = GameComponentsLookup.BoardColumn;
        var component = (BoardColumnComponent)CreateComponent(index, typeof(BoardColumnComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceBoardColumn(int newValue) {
        var index = GameComponentsLookup.BoardColumn;
        var component = (BoardColumnComponent)CreateComponent(index, typeof(BoardColumnComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveBoardColumn() {
        RemoveComponent(GameComponentsLookup.BoardColumn);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherBoardColumn;

    public static Entitas.IMatcher<GameEntity> BoardColumn {
        get {
            if (_matcherBoardColumn == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BoardColumn);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBoardColumn = matcher;
            }

            return _matcherBoardColumn;
        }
    }
}
