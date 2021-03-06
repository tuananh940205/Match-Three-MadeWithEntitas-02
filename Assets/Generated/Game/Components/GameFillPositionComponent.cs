//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public FillPositionComponent fillPosition { get { return (FillPositionComponent)GetComponent(GameComponentsLookup.FillPosition); } }
    public bool hasFillPosition { get { return HasComponent(GameComponentsLookup.FillPosition); } }

    public void AddFillPosition(int newX, int newY) {
        var index = GameComponentsLookup.FillPosition;
        var component = (FillPositionComponent)CreateComponent(index, typeof(FillPositionComponent));
        component.x = newX;
        component.y = newY;
        AddComponent(index, component);
    }

    public void ReplaceFillPosition(int newX, int newY) {
        var index = GameComponentsLookup.FillPosition;
        var component = (FillPositionComponent)CreateComponent(index, typeof(FillPositionComponent));
        component.x = newX;
        component.y = newY;
        ReplaceComponent(index, component);
    }

    public void RemoveFillPosition() {
        RemoveComponent(GameComponentsLookup.FillPosition);
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

    static Entitas.IMatcher<GameEntity> _matcherFillPosition;

    public static Entitas.IMatcher<GameEntity> FillPosition {
        get {
            if (_matcherFillPosition == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.FillPosition);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherFillPosition = matcher;
            }

            return _matcherFillPosition;
        }
    }
}
