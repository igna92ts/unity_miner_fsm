using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToMineState : WalkToDestinationState
{
    private GameObject destinationMine;
    private GameObject[] mines;
    public WalkToMineState(GameObject[] mines) {
        this.mines = mines; 
        stateID = "walk_to_mine";
    }

    private GameObject FindDestinationMine(GameObject owner) {
        var ownerPosition = owner.transform.position;
        var shortestDistance = 0f;
        GameObject currentMine = null;
        for (var i = 0; i < mines.Length; i++) {
            var newDistance = Vector2.Distance(ownerPosition, mines[i].transform.position); 
            if ((shortestDistance == 0 || newDistance < shortestDistance) && !mines[i].GetComponent<Mine>().isEmpty()) {
                shortestDistance = newDistance;
                currentMine = mines[i];
            }
        }
        return currentMine;
    }
    public override void Condition(GameObject owner) {
        var miner = owner.GetComponent<Miner>();

        if (!destinationMine) {
            miner.fsm.PerformTransition("all_empty_mines");
            return;
        }

        var ownerCollider = owner.GetComponent<CircleCollider2D>();
        var mineCollider = destinationMine.GetComponent<BoxCollider2D>();
        if (ownerCollider.IsTouching(mineCollider)) {
            owner.GetComponent<Miner>().TargetMine = destinationMine;
            miner.fsm.PerformTransition("reached_mine");
            return;
        }
    }

    public override void Execute(GameObject owner) {
        destinationMine = FindDestinationMine(owner);
        this.FollowPath(owner, destinationMine);
    }
}
