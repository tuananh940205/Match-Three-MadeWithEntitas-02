//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public MovingComponent moving { get { return (MovingComponent)GetComponent(GameComponentsLookup.Moving); } }
    public bool hasMoving { get { return HasComponent(GameComponentsLookup.Moving); } }

    public void AddMoving(UnityEngine.Vector2 newStartPos, UnityEngine.Vector2 newEndPos) {
        var index = GameComponentsLookup.Moving;
        var component = (MovingComponent)CreateComponent(index, typeof(MovingComponent));
        component.startPos = newStartPos;
        component.endPos = newEndPos;
        AddComponent(index, component);
    }

    public void ReplaceMoving(UnityEngine.Vector2 newStartPos, UnityEngine.Vector2 newEndPos) {
        var index = GameComponentsLookup.Moving;
        var component = (MovingComponent)CreateComponent(index, typeof(MovingComponent));
        component.startPos = newStartPos;
        component.endPos = newEndPos;
        ReplaceComponent(index, component);
    }

    public void RemoveMoving() {
        RemoveComponent(GameComponentsLookup.Moving);
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

    static Entitas.IMatcher<GameEntity> _matcherMoving;

    public static Entitas.IMatcher<GameEntity> Moving {
        get {
            if (_matcherMoving == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Moving);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMoving = matcher;
            }

            return _matcherMoving;
        }
    }
}