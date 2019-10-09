using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//takes care of score, coins counter
public class levelManager : MonoBehaviour
{
    //keeps track of total score and coins collected
    public int scoreValue;
    public int coinsValue;

    public Text score;
    public GameObject yellowCoin;
    public GameObject redCoin;
    public GameObject blueCoin;
    public GameObject healthCoin;
    public Text coinsScore;

    private float min;
    private float max;

    public GameObject[] blocks;

    private float coinSpawnCounter;
    public bool isGameOver;

    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;

    public GroundisLava gil;
    private int disappearCounter;
    public GameObject spikes;
    private int spikeDisappearCounter;
    public GameObject bomb;
    public GameObject arrow;
    private int bombSpawnCounter;

    private int levelNum;

    void Start()
    {
        isGameOver = false;
        disappearCounter = 0;
        spikeDisappearCounter = 0;
        bombSpawnCounter = 0;
        levelNum = 1; //used to choose the sub level, chooses each and then randomly after playing all 

        coinsValue = 0;
        scoreValue = 0;
        min = 3f;
        max = 6f;
        coinSpawnCounter = Random.Range(min,max);
        InvokeRepeating("UpdateScore", 0.2f,0.2f);
        InvokeRepeating("SpawnCoins", coinSpawnCounter, coinSpawnCounter);
    }


    void UpdateScore()
    {
        if (scoreValue > 1 && scoreValue % 200 == 0) //good at 200
        {
            //once every level has gone, choose on at random
            
            if (levelNum >=5)
            {
                ObstacleManager(Random.Range(1, 5));
            }
            else
            {
                ObstacleManager(levelNum);
                levelNum++; 
            }
        }

        if (!isGameOver)
        {
            scoreValue++;
            score.text = scoreValue.ToString();
        }
        else
        {
            CancelInvoke("UpdateScore");
            StartCoroutine(ShowGameOverPanel());
        }
    }

    void SaveGameData()
    {
        try
        {
            gameData data = saveSystem.LoadGameData();
            coinsValue += data.totalCoins;
        } catch(System.Exception e)
        {
            Debug.Log(e);
        }
        
        saveSystem.saveLevelInfo(this);
    }

    //------------- coin logic -------------------

    public void AddCoinsScore(int points)
    {
        coinsValue += points;
        coinsScore.text = "x " + coinsValue;
    }


    void SpawnCoins()
    {
        
        coinSpawnCounter = Random.Range(min, max);

        if (Random.value > 0.65)
        {
            SpawnYellowCoin();
        }
        
        if (Random.value > 0.75)
        {
            SpawnRedCoin();
        }
        if (Random.value > 0.85)
        {
            SpawnBlueCoin();
        }
        if (Random.value > 0.75)
        {
            SpawnHealthCoin();
        }

        CancelInvoke("SpawnCoins");
        if (!isGameOver)
        {
            InvokeRepeating("SpawnCoins", coinSpawnCounter, coinSpawnCounter);
        }
    }

    void SpawnYellowCoin()
    {
        int index = Random.Range(0, 7);
        float x_axis = Random.Range(-1f,1f);
        Instantiate(yellowCoin, new Vector3(blocks[index].transform.position.x + x_axis, blocks[index].transform.position.y+1f,0), Quaternion.identity);
    }

    void SpawnRedCoin()
    {
        int index = Random.Range(4, 10);
        float x_axis = Random.Range(-1f, 1f);
        Instantiate(redCoin, new Vector3(blocks[index].transform.position.x + x_axis, blocks[index].transform.position.y + 1f, 0), Quaternion.identity);
    }

    void SpawnBlueCoin()
    {
        int index = Random.Range(7,10);
        float x_axis = Random.Range(-1f, 1f);
        Instantiate(blueCoin, new Vector3(blocks[index].transform.position.x + x_axis, blocks[index].transform.position.y + 1f, 0), Quaternion.identity);
    }
    void SpawnHealthCoin()
    {
        int index = Random.Range(0,10);
        float x_axis = Random.Range(-1f, 1f);
        Instantiate(healthCoin, new Vector3(blocks[index].transform.position.x + x_axis, blocks[index].transform.position.y + 1f, 0), Quaternion.identity);
    }

    // ---------------------Menu UI handling --------------------------
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("BoxSCene");
        Time.timeScale = 1f;
    }
    public IEnumerator ShowGameOverPanel()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1;
        yield return new WaitForSeconds(0.3f);
        gameOverPanel.SetActive(true);
        scoreText.text = scoreValue.ToString();
        coinsText.text = coinsValue.ToString();
        SaveGameData();
    }
    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    // ------------- Obstacle Logic ------------- 
    void ObstacleManager(int level)
    {
        switch (level)
        {
            case 1:
                gil.StartLavaAnim();
                break;
            case 2:
                InvokeRepeating("ChooseDisappearingBlocks" ,1f ,10f);
                break;
            case 3:
                InvokeRepeating("ChooseBlockSpikes", 1f, 10f);
                break;
            case 4:
                InvokeRepeating("SpawnDangerArrow", 1f, 6f);
                break;
        }
    }

    public void ChooseDisappearingBlocks()
    {
        
        if(disappearCounter == 2)
        {
            CancelInvoke("ChooseDisappearingBlocks");
            disappearCounter = 0;
        }

        disappearCounter++;
        int b1 = Random.Range(2,5);
        int b2 = Random.Range(5,7);
        int b3 = Random.Range(7,10);

        blocks[b1].GetComponent<Animator>().SetBool("fade",true);
        blocks[b2].GetComponent<Animator>().SetBool("fade",true); 
        blocks[b3].GetComponent<Animator>().SetBool("fade",true);

        StartCoroutine(AppearBlocks(b1,b2,b3));
    }

    IEnumerator AppearBlocks(int b1, int b2, int b3)
    {
        yield return new WaitForSeconds(8);
        blocks[b1].GetComponent<Animator>().SetBool("fade",false);
        blocks[b2].GetComponent<Animator>().SetBool("fade",false);
        blocks[b3].GetComponent<Animator>().SetBool("fade", false);
    }

    public void ChooseBlockSpikes()
    {
        if (spikeDisappearCounter == 2)
        {
            CancelInvoke("ChooseBlockSpikes");
            spikeDisappearCounter = 0;
        }
        spikeDisappearCounter++;
        int s2 = Random.Range(2,5);
        int s3 = Random.Range(5,7);
        int s4 = Random.Range(7,10);
      
        Instantiate(spikes, new Vector3(blocks[s2].transform.position.x, blocks[s2].transform.position.y + 0.5f, 0), Quaternion.identity);
        Instantiate(spikes, new Vector3(blocks[s3].transform.position.x, blocks[s3].transform.position.y + 0.5f, 0), Quaternion.identity);
        Instantiate(spikes, new Vector3(blocks[s4].transform.position.x, blocks[s4].transform.position.y + 0.5f, 0), Quaternion.identity);
    }

    public void SpawnDangerArrow()
    {
        if(bombSpawnCounter == 3)
        {
            CancelInvoke("SpawnDangerArrow");
            bombSpawnCounter = 0;
        }
        bombSpawnCounter++;

        float xPos = Random.Range(-8.0f,8.0f);
        Instantiate(arrow, new Vector2(xPos, 4f), Quaternion.identity);
        StartCoroutine(SpawnBomb(xPos));
    }
    IEnumerator SpawnBomb(float xPos)
    {
        yield return new WaitForSeconds(3);
        Instantiate(bomb, new Vector2(xPos, 4.5f), Quaternion.identity);
    }
}
