using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToBarState : WalkToDestinationState
{
    private GameObject bar;
    public WalkToBarState(GameObject bar) {
        this.bar = bar;
        stateID = "walk_to_bar";
    }
    public override void Execute(GameObject owner) {
        this.FollowPath(owner, bar);
    }
    public override void Condition(GameObject owner) {
        var miner = owner.GetComponent<Miner>();
        var ownerCollider = owner.GetComponent<CircleCollider2D>();
        var bankCollider = bar.GetComponent<BoxCollider2D>();
        if (ownerCollider.IsTouching(bankCollider)) {
           miner.fsm.PerformTransition("reached_bar");
        }
    }
}
