using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private int aiActiveT;
    private int aiKilledT;
    private int difficultyStageT;

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

        //if (GameObject.Find("Player") == null) //change after end "cutscene" implimented
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
