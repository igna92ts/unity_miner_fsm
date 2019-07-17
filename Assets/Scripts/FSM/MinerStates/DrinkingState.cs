using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkingState : FSMState
{
    private float elapsed = 0f;
    private float drinkTime = 5f;
    private bool finishedDrink = false;
    public DrinkingState() {
        stateID = "drinking";
    }

    public override void Condition(GameObject owner) {
        Miner miner = owner.GetComponent<Miner>();
        if (finishedDrink) {
            miner.fsm.PerformTransition("finished_drink");
        }
    }

    public override void Execute(GameObject owner) {
        Miner miner = owner.GetComponent<Miner>();
        elapsed += Time.deltaTime;
        if (elapsed >= drinkTime) {
            elapsed = 0;
            finishedDrink = true;
        }
    }

    public override void DoBeforeLeaving() {
        this.finishedDrink = false;
    }
}
