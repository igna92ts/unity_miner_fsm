using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool walkable;
    public Vector2 position;
    public int gCost;
    public int hCost;
    public int FCost { get { return gCost + hCost; } }
    public int xIndex, yIndex;
    public Node parent;

    public Node(bool walkable, Vector2 position, int xIndex, int yIndex) {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
        this.walkable = walkable;
        this.position = position;
    }
}
