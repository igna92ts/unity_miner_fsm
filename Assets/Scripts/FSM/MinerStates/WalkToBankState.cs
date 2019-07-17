using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToBankState : WalkToDestinationState
{
    private GameObject bank;
    public WalkToBankState(GameObject bank) {
        this.bank = bank;
        stateID = "walk_to_bank";
    }

    public override void Condition(GameObject owner) {
        var miner = owner.GetComponent<Miner>();
        var ownerCollider = owner.GetComponent<CircleCollider2D>();
        var bankCollider = bank.GetComponent<BoxCollider2D>();
        if (ownerCollider.IsTouching(bankCollider)) {
           miner.fsm.PerformTransition("reached_bank");
        }
    }

    public override void Execute(GameObject owner) {
        this.FollowPath(owner, bank);
    }
}
