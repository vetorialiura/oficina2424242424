using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Gamecontroller : MonoBehaviour
{
    public static Gamecontroller instance;

    [Header("Score")]
    public int totalScore;
    public TextMeshProUGUI scoreText;
    [Tooltip("Opcional: nome do objeto de texto do score para procurar automaticamente")]
    public string scoreTextObjectName = "ScoreText";

    [Header("UI Panels")]
    public GameObject gameOver;
    public GameObject panelVitoria;

    void Awake()
    {
        // Singleton -> evita instâncias duplicadas ao recarregar cenas
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // ← Persiste entre cenas

        // Re-aponta referências quando uma cena é carregada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        if (instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        UpdateTextMeshProUGUI();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Se scoreText não estiver atribuído, tenta encontrar automaticamente
        if (scoreText == null)
        {
            // 1) procura por objeto com nome (se preenchido)
            if (!string.IsNullOrEmpty(scoreTextObjectName))
            {
                var obj = GameObject.Find(scoreTextObjectName);
                if (obj != null)
                {
                    var tmp = obj.GetComponent<TextMeshProUGUI>();
                    if (tmp != null)
                    {
                        scoreText = tmp;
                        Debug.Log("[Gamecontroller] scoreText encontrado por nome: " + scoreTextObjectName);
                    }
                }
            }

            // 2) fallback: procura primeiro TextMeshProUGUI na cena
            if (scoreText == null)
            {
                var found = FindObjectOfType<TextMeshProUGUI>();
                if (found != null)
                {
                    scoreText = found;
                    Debug.Log("[Gamecontroller] scoreText encontrado automaticamente: " + found.gameObject.name);
                }
            }
        }

        // Atualiza a UI após carregar cena
        UpdateTextMeshProUGUI();
    }

    // Centraliza incremento de pontos e loga para debugar
    public void AddScore(int amount)
    {
        totalScore += amount;
        Debug.Log($"[Gamecontroller] AddScore: +{amount} -> totalScore = {totalScore}");
    
        // Dispara evento Observer
        GameEvents.TriggerScoreChanged(totalScore);
    }

    public void UpdateTextMeshProUGUI()
    {
        if (scoreText != null)
            scoreText.text = totalScore.ToString();
        else
            Debug.LogWarning("[Gamecontroller] scoreText é nulo — UI não atualizada. Verifique se o objeto de texto existe na cena ou atribua manualmente no inspector.");
    }

    public void ShowGameOver()
    {
        // Dispara evento Observer
        GameEvents.TriggerGameOver();
    
        if (gameOver != null)
            gameOver.SetActive(true);
    }

    public void ShowVictory()
    {
        // Dispara evento Observer
        GameEvents.TriggerVictory();
    
        if (panelVitoria != null)
        {
            panelVitoria.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    // ----- Reset / Restart -----
    public void ResetScore()
    {
        totalScore = 0;
        Debug.Log("[Gamecontroller] ResetScore chamado -> totalScore = 0");
        UpdateTextMeshProUGUI();
    }

    public void RestartAndResetScore()
    {
        Debug.Log("[Gamecontroller] RestartAndResetScore chamado.");
        Time.timeScale = 1f;
        ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RestartLevel()
    {
        Debug.Log("[Gamecontroller] RestartLevel chamado.");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}