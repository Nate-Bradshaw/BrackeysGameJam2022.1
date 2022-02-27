using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int aiActive;
    private int aiCreated = 0;
    public int aiKilled;
    public int difficultyStage = 1;

    [SerializeField] private int maxAI;

    [SerializeField] private GameObject ai;
    [SerializeField] private Transform playerLocation;

    private float xSpawn;
    private float zSpawn;

    private bool waveStart;

    public static GameController instance;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        waveStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (waveStart && GameObject.Find("Player") != null)
            SpawnAI(difficultyStage);

        GameObject[] livingAIList = GameObject.FindGameObjectsWithTag("AI");
        aiActive = livingAIList.Length;
        aiKilled = aiCreated - aiActive;

        /*
        if (GameObject.Find("Player") == null)
        {
            
        }
        */
    }

    private void SpawnAI(int amount)
    {
        StartCoroutine(Cooldown());
        for (int y = 0; y < amount; y++)
        {
            xSpawn = Mathf.Round(Random.Range(1, 25)) * 5;
            zSpawn = Mathf.Round(Random.Range(1, 25)) * 5; //30 is too close

            if (Vector3.Distance(playerLocation.position, this.transform.position += new Vector3(xSpawn, 0, zSpawn)) <= 50f)
            {
                y--;
                return;
            }
            else
            {
                if (aiActive <= maxAI)
                {
                    GameObject MyAI = Instantiate(ai, new Vector3(xSpawn, 0, zSpawn), Quaternion.identity);
                    //Debug.Log(transform.childCount);
                    aiCreated++;
                }
            }
        }
        difficultyStage++;
    }

    private IEnumerator Cooldown()
    {
        float waitTime;
        waitTime = 4 * difficultyStage;

        if (waitTime > 16f)
            waitTime = 16f;

        waveStart = false;
        yield return new WaitForSeconds(waitTime);
        waveStart = true;
    }
}
