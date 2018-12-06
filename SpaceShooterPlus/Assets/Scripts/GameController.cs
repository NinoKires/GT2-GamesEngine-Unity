using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public HazardEntry[] hazards;

    public Vector3 spawnValues;

    public int hazardCount;
    private int score;
    private int totalWeight;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    private bool gameOver;
    private bool restart;

    private ArrayList currentHazards;

    private const byte targetCheckWait = 120;
    private byte checkTime;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        currentHazards = new ArrayList();
        checkTime = 0;
        StartCoroutine(SpawnWaves());
        totalWeight = 0;
        foreach (HazardEntry entry in hazards)
        {
            totalWeight += entry.weight;
        }
    }

    private void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (checkTime >= targetCheckWait)
            {
                ClearHazards();
                checkTime = 0;
            }
        }
    }

    void ClearHazards()
    {
        foreach (GameObject item in currentHazards)
        {
            if (item == null)
            {
                currentHazards.Remove(item);
                return;
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = null;
                int randomChoice = Random.Range(0, totalWeight);
                foreach (HazardEntry entry in hazards)
                {
                    if (randomChoice < entry.weight)
                    {
                        hazard = entry.hazard;
                        break;
                    }
                    randomChoice -= entry.weight;
                }

                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                hazard = (GameObject)Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
            hazardCount++;
            spawnWait *= 0.9f;

            if (gameOver)
            {
                restartText.text = "Press 'R' for restart";
                restart = true;
                break;
            }
        }
    }

    public ArrayList GetHazards()
    {
        return currentHazards;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}

[System.Serializable]
public struct HazardEntry
{
    public GameObject hazard;
    public int weight;
}
