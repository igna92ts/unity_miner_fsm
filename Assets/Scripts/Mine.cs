using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mine : MonoBehaviour
{
    private int oreInMine = 100;
    public int OreInMine { get { return oreInMine; } }
    public Text oreText;
    private float regenTime = 30f;
    private int regenAmount = 5;

    private float elapsed = 0f;

    public int ReduceOreBy(int amount) {
        int actualAmount = amount;
        if (this.oreInMine - amount <= 0) {
            actualAmount = this.oreInMine;
            this.oreInMine = 0;
        } else {
            this.oreInMine -= amount;
        }
        this.oreText.text = "Ore in mine: " + oreInMine;
        return actualAmount;
    }

    public bool isEmpty() {
        return this.oreInMine > 0 ? false : true;
    }

    void Start() {
        oreText.text = "Ore in mine: " + oreInMine;
    }

    void Update() {
        elapsed += Time.deltaTime;
        if (elapsed >= regenTime) {
            elapsed = 0;
            oreInMine += regenAmount;
            oreText.text = "Ore in mine: " + oreInMine;
        }
    }
}
