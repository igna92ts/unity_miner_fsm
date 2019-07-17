using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositingState : FSMState
{
    private float elapsed = 0f;
    private float transactionTime = 5f;
    public DepositingState() {
        stateID = "depositing";
    }

    public override void Condition(GameObject owner) {
        Miner miner = owner.GetComponent<Miner>();
        if (miner.goldInPocket == 0) {
            miner.fsm.PerformTransition("finished_deposit");
        }
    }

    public override void Execute(GameObject owner) {
        Miner miner = owner.GetComponent<Miner>();
        elapsed += Time.deltaTime;
        if (elapsed >= transactionTime) {
            elapsed = 0;
            miner.goldInBank += miner.goldInPocket;
            miner.goldInPocket = 0;
        }
    }
}
