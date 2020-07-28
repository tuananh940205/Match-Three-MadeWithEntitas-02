//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ArrayPositionComponent arrayPosition { get { return (ArrayPositionComponent)GetComponent(GameComponentsLookup.ArrayPosition); } }
    public bool hasArrayPosition { get { return HasComponent(GameComponentsLookup.ArrayPosition); } }

    public void AddArrayPosition(int newX, int newY) {
        var index = GameComponentsLookup.ArrayPosition;
        var component = (ArrayPositionComponent)CreateComponent(index, typeof(ArrayPositionComponent));
        component.x = newX;
        component.y = newY;
        AddComponent(index, component);
    }

    public void ReplaceArrayPosition(int newX, int newY) {
        var index = GameComponentsLookup.ArrayPosition;
        var component = (ArrayPositionComponent)CreateComponent(index, typeof(ArrayPositionComponent));
        component.x = newX;
        component.y = newY;
        ReplaceComponent(index, component);
    }

    public void RemoveArrayPosition() {
        RemoveComponent(GameComponentsLookup.ArrayPosition);
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

    static Entitas.IMatcher<GameEntity> _matcherArrayPosition;

    public static Entitas.IMatcher<GameEntity> ArrayPosition {
        get {
            if (_matcherArrayPosition == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ArrayPosition);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherArrayPosition = matcher;
            }

            return _matcherArrayPosition;
        }
    }
}