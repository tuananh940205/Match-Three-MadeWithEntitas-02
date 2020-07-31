using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
public class ViewComponent : IComponent
{
    public GameObject gameObject;
}

[Game]
public class ArrayPositionComponent : IComponent
{
    public int x;
    public int y;
}

[Game, Unique]
public class BoardRowComponent : IComponent
{
    public int value;
}

[Game, Unique]
public class BoardColumnComponent : IComponent
{
    public int value;
}

[Game]
public class MovingComponent : IComponent
{
    public Vector2 startPos;
    public Vector2 endPos;
}

[Game]
public class MoveComponent : IComponent
{
    public int x;
    public int y;
}

[Game]
public class PositionComponent : IComponent
{
    public Vector2 value;
}

[Game]
public class FindingMatchComponent : IComponent
{
}

[Game]
public class TileNameComponent : IComponent
{
    public string name;
}

[Game]
public class FadedComponent : IComponent
{
}

[Game]
public class FillPositionComponent : IComponent
{
    public int x;
    public int y;
}