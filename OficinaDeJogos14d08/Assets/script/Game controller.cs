using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Controlador principal do jogo usando Singleton Pattern
/// Gerencia score, UI e estados do jogo
/// ATUALIZADO: Agora dispara eventos do Observer Pattern
/// </summary>
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
        DontDestroyOnLoad(gameObject);

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
            // CORRIGIDO: Usando FindFirstObjectByType em vez de FindObjectOfType (deprecated)
            if (scoreText == null)
            {
                var found = FindFirstObjectByType<TextMeshProUGUI>();
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

    // ==========================================
    // ATUALIZADO: Agora dispara evento Observer
    // ==========================================
    /// <summary>
    /// Adiciona pontos e notifica todos os Observers
    /// </summary>
    public void AddScore(int amount)
    {
        totalScore += amount;
        Debug.Log($"[Gamecontroller] AddScore: +{amount} -> totalScore = {totalScore}");
        
        UpdateTextMeshProUGUI();
        
        // 🔔 DISPARAR EVENTO OBSERVER
        // Esta linha notifica todos os Observers inscritos
        GameEvents.TriggerScoreChanged(totalScore);
    }

    public void UpdateTextMeshProUGUI()
    {
        //if (scoreText != null)
        //    scoreText.text = totalScore.ToString();
        //else
        //    Debug.LogWarning("[Gamecontroller] scoreText é nulo — UI não atualizada. Verifique se o objeto de texto existe na cena ou atribua manualmente no inspector.");
    }

    // ==========================================
    // ATUALIZADO: Agora dispara evento Observer
    // ==========================================
    /// <summary>
    /// Mostra tela de Game Over e notifica Observers
    /// </summary>
    public void ShowGameOver()
    {
        // 🔔 DISPARAR EVENTO OBSERVER
        GameEvents.TriggerGameOver();
        
        // UI antiga (mantém compatibilidade)
        if (gameOver != null)
            gameOver.SetActive(true);
    }

    // ==========================================
    // ATUALIZADO: Agora dispara evento Observer
    // ==========================================
    /// <summary>
    /// Mostra tela de Vitória e notifica Observers
    /// </summary>
    public void ShowVictory()
    {
        // 🔔 DISPARAR EVENTO OBSERVER
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
        
        // Dispara evento para atualizar Observers
        GameEvents.TriggerScoreChanged(totalScore);
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