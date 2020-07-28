using Entitas;
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

[Game]
public class BoardRowComponent : IComponent
{
    public int value;
}

[Game]
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