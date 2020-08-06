//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TileColumnPullComponent tileColumnPull { get { return (TileColumnPullComponent)GetComponent(GameComponentsLookup.TileColumnPull); } }
    public bool hasTileColumnPull { get { return HasComponent(GameComponentsLookup.TileColumnPull); } }

    public void AddTileColumnPull(int[] newTileNumber) {
        var index = GameComponentsLookup.TileColumnPull;
        var component = (TileColumnPullComponent)CreateComponent(index, typeof(TileColumnPullComponent));
        component.tileNumber = newTileNumber;
        AddComponent(index, component);
    }

    public void ReplaceTileColumnPull(int[] newTileNumber) {
        var index = GameComponentsLookup.TileColumnPull;
        var component = (TileColumnPullComponent)CreateComponent(index, typeof(TileColumnPullComponent));
        component.tileNumber = newTileNumber;
        ReplaceComponent(index, component);
    }

    public void RemoveTileColumnPull() {
        RemoveComponent(GameComponentsLookup.TileColumnPull);
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

    static Entitas.IMatcher<GameEntity> _matcherTileColumnPull;

    public static Entitas.IMatcher<GameEntity> TileColumnPull {
        get {
            if (_matcherTileColumnPull == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TileColumnPull);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTileColumnPull = matcher;
            }

            return _matcherTileColumnPull;
        }
    }
}