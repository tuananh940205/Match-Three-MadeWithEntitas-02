﻿using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class BoardSizeComponent : IComponent
{
    public int row;
    public int column;
}

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
public class FindingMatchComponent : IComponent { }

[Game]
public class TileNameComponent : IComponent
{
    public string name;
}

[Game]
public class FadedComponent : IComponent { }

[Game]
public class FillPositionComponent : IComponent
{
    public int x;
    public int y;
}

[Game]
public class FirstPositionComponent : IComponent
{
    public float x;
    public float y;
}

[Game]
public class LastPositionComponent : IComponent
{
    public float x;
    public float y;
}

[Game]
public class ClearBoardComponent : IComponent { }