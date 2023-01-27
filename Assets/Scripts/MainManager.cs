using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    
    public Text ScoreText;
    public Text bestScoreText;


    public Button startButton;
    public Button exitButton;
    public Button restartButton;
   
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public GameObject GameOverText;
    
    public bool m_Started = false;
    public int m_Points;    
    public bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
        AddPoint(0);
        BestScoreConfig(GameManager.Instance.bestScore);
    }
    private void Update()
    {
        
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        /* if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }*/
        

    }

    void AddPoint(int point)
    {
        m_Points += point;
       // ScoreText.text = $"Score : {m_Points}";
        ScoreText.text = m_Points.ToString();
       

    }

  

    public void GameOver()
    {
        m_GameOver = true;
        // GameManager.Instance.SaveGame(m_Points);
        // GameManager.Instance.LoadGame();
        //GameOverText.SetActive(true);
        BestScoreConfig(m_Points);
        restartButton.gameObject.SetActive(true);
       
    }
    public void Restart()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }
    public void BestScoreConfig(int points)
    {
        int bestScore = GameManager.Instance.bestScore;
        if (points>bestScore)
        {
            bestScore = points;
            GameManager.Instance.bestScoreName = GameManager.Instance.playerName;
        }
        GameManager.Instance.bestScore = bestScore;
        bestScoreText.text = "Best Score: " + GameManager.Instance.bestScoreName + ": " + bestScore;
    }



    public void QuitGame()
    {

#if UNITY_EDITOR     
        
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
        GameManager.Instance.SaveData();
    }
}
