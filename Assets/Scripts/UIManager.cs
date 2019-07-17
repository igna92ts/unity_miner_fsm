using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mineTextPrefab;
    public GameObject scrollViewContent;
    public GameObject[] mines;
    void Awake() {
        mines = GameObject.FindGameObjectsWithTag("Mine");
        foreach(GameObject mine in mines) {
            GameObject textObj = Instantiate(mineTextPrefab);
            textObj.transform.SetParent(scrollViewContent.transform, false);
            mine.GetComponent<Mine>().oreText = textObj.GetComponent<Text>();
        }
    }
}
