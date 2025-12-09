using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gamecontroller : MonoBehaviour
{
    public static Gamecontroller instance;

    public int totalScore;
    public TextMeshProUGUI scoreText;

    public GameObject gameOver;
    public GameObject panelVitoria;

    void Awake()
    {
        // Garante que o instance existe ANTES de qualquer fruta rodar OnTriggerEnter
        instance = this;
    }

    void Start()
    {
        UpdateTextMeshProUGUI();
    }

    public void UpdateTextMeshProUGUI()
    {
        if (scoreText != null)
            scoreText.text = totalScore.ToString();
        else
            Debug.LogError("ScoreText não está atribuído no Gamecontroller da cena!");
    }

    public void ShowGameOver()
    {
        if (gameOver != null)
            gameOver.SetActive(true);
    }

    public void ShowVictory()
    {
        if (panelVitoria != null)
        {
            panelVitoria.SetActive(true);
            Time.timeScale = 0f; // opcional, só se quiser pausar ao vencer
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}