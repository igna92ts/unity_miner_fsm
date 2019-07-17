using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingState : FSMState
{
    private float elapsed = 0f;
    private float sleepTime = 5;
    private bool wokeUp = false;
    public SleepingState() {
        stateID = "sleeping";
    }
    public override void Condition(GameObject owner) {
        if (wokeUp) {
            Miner miner = owner.GetComponent<Miner>();
            miner.fsm.PerformTransition("wake_up");
        }
    }

    public override void DoBeforeEntering() {
        this.wokeUp = false;
    }

    public override void Execute(GameObject owner) {
        elapsed += Time.deltaTime;
        if (elapsed >= sleepTime) {
            wokeUp = true;
            var miner = owner.GetComponent<Miner>();
            miner.energy = 20;
            elapsed = 0;
        }
    }
}
