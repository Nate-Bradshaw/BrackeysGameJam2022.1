using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTracker : MonoBehaviour
{
    Text tracker;

    private int aiActiveT;
    private int aiKilledT;
    private int difficultyStageT;


    void Start()
    {
        tracker = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        aiActiveT = GameController.instance.aiActive;
        aiKilledT = GameController.instance.aiKilled;
        difficultyStageT = GameController.instance.difficultyStage;

        tracker.text = "ALIVE: " + aiActiveT + " DEAD: " + aiKilledT + " HEAT: " + difficultyStageT;
    }
}
