﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToHouseState : WalkToDestinationState
{
    private GameObject house;
    public WalkToHouseState(GameObject house) {
        this.house = house;
        stateID = "walk_to_house";
    }

    public override void Execute(GameObject owner) {
        this.FollowPath(owner, house);
    }
    public override void Condition(GameObject owner) {
        var miner = owner.GetComponent<Miner>();
        var ownerCollider = owner.GetComponent<CircleCollider2D>();
        var bankCollider = house.GetComponent<BoxCollider2D>();
        if (ownerCollider.IsTouching(bankCollider)) {
           miner.fsm.PerformTransition("reached_house");
        }
    }
}
