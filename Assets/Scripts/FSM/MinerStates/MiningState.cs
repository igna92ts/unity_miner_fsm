using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningState : FSMState
{
    private float elapsed = 0f;
    public MiningState() {
        stateID = "mining";
    }
    public override void Condition(GameObject owner) {
        var miner = owner.GetComponent<Miner>();
        if (miner.TargetMine.GetComponent<Mine>().isEmpty()) {
            miner.fsm.PerformTransition("empty_mine");
            return;
        }

        if (miner.energy == 0) {
            miner.fsm.PerformTransition("no_energy");
            return;
        }
    }

    public override void Execute(GameObject owner) {
        var miner = owner.GetComponent<Miner>();
        elapsed += Time.deltaTime;
        if (elapsed >= miner.mineCooldown) {
            elapsed = 0;
            if (miner.energy > 0) {
                miner.MineOre();
            }
        }
    }
}
