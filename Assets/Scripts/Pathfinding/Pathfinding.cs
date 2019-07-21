using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    CustomGrid grid;
    void Awake() {
        grid = GetComponent<CustomGrid>();
    }
    public List<Node> FindPath(Vector2 startPositiong, Vector2 targetPosition, GameObject walkableException = null) {
        Node startNode = grid.NodeFromWorldPoint(startPositiong);
        Node targetNode = grid.NodeFromWorldPoint(targetPosition);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count > 0) {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++) {
                if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].hCost < currentNode.hCost) {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode) {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
                var isException = IsWalkableException(walkableException, neighbour);
                if ((!neighbour.walkable && !isException) || closedSet.Contains(neighbour)) continue;

                int newMovementCost = currentNode.gCost + GetNodeDistance(currentNode, neighbour);
                if (newMovementCost < neighbour.gCost || !openSet.Contains(neighbour)) {
                    neighbour.gCost = newMovementCost;
                    neighbour.hCost = GetNodeDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour)) {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
        return null;
    }

    public bool IsWalkableException(GameObject exceptionObject, Node node) {
        var nodeRadius = grid.nodeRadius;
        var bottomLeft = new Vector2(node.position.x - nodeRadius, node.position.y - nodeRadius);
        var bottomRight = new Vector2(node.position.x + nodeRadius, node.position.y - nodeRadius);
        var topLeft = new Vector2(node.position.x - nodeRadius, node.position.y + nodeRadius);
        var topRight = new Vector2(node.position.x + nodeRadius, node.position.y + nodeRadius);
        var collider = exceptionObject.GetComponent<Collider2D>();
        return collider.bounds.Contains(bottomLeft) ||
            collider.bounds.Contains(bottomRight) ||
            collider.bounds.Contains(topLeft) ||
            collider.bounds.Contains(topRight) ||
            collider.bounds.Contains(node.position);
    }
    List<Node> RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        grid.path = path;
        return path;
    }

    int GetNodeDistance(Node nodeA, Node nodeB) {
        int distX = Mathf.Abs(nodeA.xIndex - nodeB.xIndex);
        int distY = Mathf.Abs(nodeA.yIndex - nodeB.yIndex);
        if (distX > distY) {
            return 14 * distY + 10 * (distX - distY);
        }
        return 14 * distX + 10 * (distY - distX);
    }
}
