using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : FSMState
{
    public IdleState() {
        stateID = "idle";
    }
    private bool IsMineAvailable() {
        foreach(Mine mine in mines) {
            if (mine.OreInMine > 0) {
                return true;
            }
        }
        return false;
    }
    private List<Mine> mines = new List<Mine>();
    public IdleState(GameObject[] minesObjArray) {
        foreach (GameObject mine in minesObjArray) {
            Mine actualMine = mine.GetComponent<Mine>();
            mines.Add(actualMine);
        }
        stateID = "idle";
    }
    public override void Condition(GameObject owner) {
        Miner miner = owner.GetComponent<Miner>();
        if (miner.goldInPocket > 0) {
            miner.fsm.PerformTransition("money_in_pocket");
            return;
        }


        if (IsMineAvailable()) {
            miner.fsm.PerformTransition("available_mines");
            return;
        }

        if (miner.energy == 0) {
            miner.fsm.PerformTransition("no_energy");
            return;
        }

        miner.fsm.PerformTransition("nothing_to_do");
    }

    public override void Execute(GameObject owner) { }
}
