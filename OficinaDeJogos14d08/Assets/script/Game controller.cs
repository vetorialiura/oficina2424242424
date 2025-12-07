using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gamecontroller : MonoBehaviour
{
    public int totalScore;

    public static Gamecontroller instance;
    public TextMeshProUGUI scoreText;

    public GameObject gameOver;
    public GameObject panelVitoria;

    void Start()
    {
        instance = this;
    }

    public void UpdateTextMeshProUGUI()
    {
        scoreText.text = totalScore.ToString();

        // 🟢 SE O SCORE FOR 80, ATIVA VITÓRIA
        if (totalScore >= 80)
        {
            Time.timeScale = 0f;
            Victory();
        }
    }

    public void Victory()
    {
        panelVitoria.SetActive(true);
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void RestartGame(string lvlName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(lvlName);
    }
}