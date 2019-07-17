using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public FSM fsm;

    public GameObject[] mines;
    public GameObject bank;
    public GameObject house;
    public GameObject bar;
    public int goldInPocket = 0;
    public int goldInBank = 0;
    public int energy = 20;
    private int mineAmount = 20;
    public float movementSpeed = 3f;
    public float mineCooldown = 1;
    private GameObject targetMine;
    public GameObject TargetMine { set { targetMine = value; } get { return targetMine; } }
    public Pathfinding pathfinding;

    public void Start() {
        mines = GameObject.FindGameObjectsWithTag("Mine");
        this.targetMine = mines[0];
        MakeFSM();
    }
    public void FixedUpdate() {
        fsm.CurrentState.Condition(this.gameObject);
        fsm.CurrentState.Execute(this.gameObject);
    }

    void MakeFSM() {
        SleepingState sleeping = new SleepingState();
        sleeping.AddTransition("wake_up", "walk_to_mine");

        WalkToMineState walkToMine = new WalkToMineState(mines);
        walkToMine.AddTransition("reached_mine", "mining");
        walkToMine.AddTransition("all_empty_mines", "idle");

        IdleState idle = new IdleState(mines);
        idle.AddTransition("money_in_pocket", "walk_to_bank");
        idle.AddTransition("available_mines", "walk_to_mine");
        idle.AddTransition("nothing_to_do", "walk_to_bar");
        idle.AddTransition("no_energy", "walk_to_house");

        WalkToHouseState walkToHouse = new WalkToHouseState(house);
        walkToHouse.AddTransition("reached_house", "sleeping");

        WalkToBarState walkToBar = new WalkToBarState(bar);
        walkToBar.AddTransition("reached_bar", "drinking");

        DrinkingState drinking = new DrinkingState();
        drinking.AddTransition("finished_drink", "idle");

        MiningState mining = new MiningState();
        mining.AddTransition("empty_mine", "walk_to_mine");
        mining.AddTransition("no_energy", "walk_to_house");

        WalkToBankState walkToBank = new WalkToBankState(bank);
        walkToBank.AddTransition("reached_bank", "depositing");

        DepositingState depositing = new DepositingState();
        depositing.AddTransition("finished_deposit", "idle");

        fsm = new FSM();
        fsm.AddState(walkToHouse);
        fsm.AddState(sleeping);
        fsm.AddState(walkToMine);
        fsm.AddState(mining);
        fsm.AddState(walkToBank);
        fsm.AddState(idle);
        fsm.AddState(walkToBar);
        fsm.AddState(depositing);
        fsm.AddState(drinking);
    }

    public void MineOre() {
        var actualAmount = TargetMine.GetComponent<Mine>().ReduceOreBy(mineAmount);
        energy--;
        this.goldInPocket += actualAmount;
    }
}
