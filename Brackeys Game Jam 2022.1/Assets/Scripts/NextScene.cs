using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private int aiActiveT;
    private int aiKilledT;
    private int difficultyStageT;

    Text tracker;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("OnlyOne");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        if (GameObject.Find("Game Controller") != null)
        {
            aiActiveT = GameController.instance.aiActive;
            aiKilledT = GameController.instance.aiKilled;
            difficultyStageT = GameController.instance.difficultyStage;
        }

        if (GameObject.Find("Player") == null && SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }


        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            tracker = GameObject.Find("PostText").GetComponent<Text>();
            tracker.text = "ALIVE: " + aiActiveT + " DEAD: " + aiKilledT + " HEAT: " + difficultyStageT;

            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene(0);
                //Destroy(this, 0f);
            }
        }

        //if (GameObject.Find("Player") == null) //change after end "cutscene" implimented
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
