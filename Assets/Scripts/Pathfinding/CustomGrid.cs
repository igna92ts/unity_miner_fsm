using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGrid : MonoBehaviour
{
    public Vector2 gridWorldSize;
    public List<Node> path;
    public float nodeRadius;
    public LayerMask unwalkableLayer;
    private float nodeDiameter;
    
    Node[,] grid;
    int gridSizeX, gridSizeY;

    void OnDrawGizmos() {
        Gizmos.DrawWireCube(transform.position, new Vector2(gridWorldSize.x, gridWorldSize.y));
        if (grid != null) {
            foreach(Node node in grid) {
                Gizmos.color = (node.walkable) ? Color.white : Color.red;
                if (path != null) {
                    if (path.Contains(node)) Gizmos.color = Color.green;
                }
                Gizmos.DrawCube(node.position, Vector2.one * (nodeDiameter - .1f));
            }
        }
    }

    void Start() {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition) {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.y + gridWorldSize.y / 2) / gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);
        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node) {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) continue;

                int checkX = node.xIndex + x;
                int checkY = node.yIndex + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    void CreateGrid() {
        grid = new Node[gridSizeX, gridSizeY];
        Vector2 bottomLeft = new Vector2(transform.position.x - gridWorldSize.x / 2, transform.position.y - gridWorldSize.y / 2);
        for (int x = 0; x < gridSizeX; x++) {
            for (int y = 0; y < gridSizeY; y++) {
                Vector2 worldPoint = new Vector2(bottomLeft.x + nodeDiameter * x + nodeRadius, bottomLeft.y + nodeDiameter * y + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableLayer));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }
}
