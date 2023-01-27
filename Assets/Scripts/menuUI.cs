using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuUI : MonoBehaviour
{
    public TMP_InputField playerName;
    public Text bestScoreText;

    // Start is called before the first frame update
    void Start()
    {
        BestScoreConfig(GameManager.Instance.bestScore);
        playerName.text = GameManager.Instance.playerName;
        SceneManager.LoadScene(1);
    }
    public void SaveName()
    {
        GameManager.Instance.playerName = playerName.text.ToString();
    }
    public void BestScoreConfig(int bestScore)
    {
        bestScoreText.text = "Best Score: " + GameManager.Instance.bestScoreName + ": " + bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
