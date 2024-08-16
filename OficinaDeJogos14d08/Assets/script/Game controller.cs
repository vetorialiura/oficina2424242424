using System;
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
    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateTextMeshProUGUI()
    {
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
}
