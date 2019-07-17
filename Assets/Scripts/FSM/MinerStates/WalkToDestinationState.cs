using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToDestinationState : FSMState
{
    public List<Node> pathToDestination;
    public void FollowPath(GameObject owner, GameObject destination) {
        var miner = owner.GetComponent<Miner>();
        if (pathToDestination == null) {
            pathToDestination = new List<Node>();
            pathToDestination = miner.pathfinding.FindPath(owner.transform.position, destination.transform.position, destination);
        }
        if ((Vector2)owner.transform.position == pathToDestination[0].position) {
            pathToDestination.RemoveAt(0);
        }
        var movementSpeed = miner.movementSpeed;
        owner.transform.position = Vector2.MoveTowards(owner.transform.position, pathToDestination[0].position, movementSpeed * Time.deltaTime);
    }
    public override void Execute(GameObject owner) { }
    public override void Condition(GameObject owner) { }

    public override void DoBeforeLeaving() {
        this.pathToDestination = null;
    }
}
