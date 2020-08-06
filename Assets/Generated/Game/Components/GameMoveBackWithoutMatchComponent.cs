//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MoveBackWithoutMatchComponent moveBackWithoutMatch { get { return (MoveBackWithoutMatchComponent)GetComponent(GameComponentsLookup.MoveBackWithoutMatch); } }
    public bool hasMoveBackWithoutMatch { get { return HasComponent(GameComponentsLookup.MoveBackWithoutMatch); } }

    public void AddMoveBackWithoutMatch(int newX, int newY) {
        var index = GameComponentsLookup.MoveBackWithoutMatch;
        var component = (MoveBackWithoutMatchComponent)CreateComponent(index, typeof(MoveBackWithoutMatchComponent));
        component.x = newX;
        component.y = newY;
        AddComponent(index, component);
    }

    public void ReplaceMoveBackWithoutMatch(int newX, int newY) {
        var index = GameComponentsLookup.MoveBackWithoutMatch;
        var component = (MoveBackWithoutMatchComponent)CreateComponent(index, typeof(MoveBackWithoutMatchComponent));
        component.x = newX;
        component.y = newY;
        ReplaceComponent(index, component);
    }

    public void RemoveMoveBackWithoutMatch() {
        RemoveComponent(GameComponentsLookup.MoveBackWithoutMatch);
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

    static Entitas.IMatcher<GameEntity> _matcherMoveBackWithoutMatch;

    public static Entitas.IMatcher<GameEntity> MoveBackWithoutMatch {
        get {
            if (_matcherMoveBackWithoutMatch == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MoveBackWithoutMatch);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMoveBackWithoutMatch = matcher;
            }

            return _matcherMoveBackWithoutMatch;
        }
    }
}