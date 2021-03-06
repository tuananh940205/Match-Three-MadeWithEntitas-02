//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TileColumnFillComponent tileColumnFill { get { return (TileColumnFillComponent)GetComponent(GameComponentsLookup.TileColumnFill); } }
    public bool hasTileColumnFill { get { return HasComponent(GameComponentsLookup.TileColumnFill); } }

    public void AddTileColumnFill(int[] newTileNumber) {
        var index = GameComponentsLookup.TileColumnFill;
        var component = (TileColumnFillComponent)CreateComponent(index, typeof(TileColumnFillComponent));
        component.tileNumber = newTileNumber;
        AddComponent(index, component);
    }

    public void ReplaceTileColumnFill(int[] newTileNumber) {
        var index = GameComponentsLookup.TileColumnFill;
        var component = (TileColumnFillComponent)CreateComponent(index, typeof(TileColumnFillComponent));
        component.tileNumber = newTileNumber;
        ReplaceComponent(index, component);
    }

    public void RemoveTileColumnFill() {
        RemoveComponent(GameComponentsLookup.TileColumnFill);
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

    static Entitas.IMatcher<GameEntity> _matcherTileColumnFill;

    public static Entitas.IMatcher<GameEntity> TileColumnFill {
        get {
            if (_matcherTileColumnFill == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TileColumnFill);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTileColumnFill = matcher;
            }

            return _matcherTileColumnFill;
        }
    }
}
