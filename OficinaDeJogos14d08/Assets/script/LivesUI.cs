using UnityEngine;
using TMPro;

/// <summary>
/// Observer que GERENCIA e EXIBE as vidas
/// Funciona EXATAMENTE como o ScoreUI.cs
/// Este script é responsável por:
/// 1. Manter o valor das vidas
/// 2. Escutar eventos de morte do player
/// 3. Atualizar a UI automaticamente
/// </summary>
public class LivesUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text livesText;

    [Header("Lives Management")]
    [SerializeField] private int maxLives = 1; // Seu personagem tem 1 vida
    private int currentLives;
    private bool hasInitialized = false; // Flag para evitar reinicialização

    /// <summary>
    /// Inicializa as vidas ANTES de tudo
    /// </summary>
    void Awake()
    {
        // Inicializa as vidas apenas uma vez
        if (!hasInitialized)
        {
            currentLives = maxLives;
            hasInitialized = true;
            Debug.Log($"[LivesUI] Vidas inicializadas: {currentLives}");
        }
    }

    /// <summary>
    /// Quando o componente é habilitado, INSCREVER-SE no evento de morte
    /// </summary>
    void OnEnable()
    {
        // Inscreve no evento de morte do player
        GameEvents.OnPlayerDied += OnPlayerDied;
        Debug.Log("[LivesUI] Inscrito no evento OnPlayerDied");
        
        // Atualiza display inicial
        UpdateLivesDisplay();
    }

    /// <summary>
    /// Quando o componente é desabilitado, DESINSCREVER-SE do evento
    /// </summary>
    void OnDisable()
    {
        GameEvents.OnPlayerDied -= OnPlayerDied;
        Debug.Log("[LivesUI] Desinscrito do evento OnPlayerDied");
    }

    /// <summary>
    /// Callback chamado quando o player morre
    /// AQUI acontece a contagem!
    /// </summary>
    private void OnPlayerDied()
    {
        Debug.Log($"[LivesUI] OnPlayerDied CHAMADO! Vidas ANTES: {currentLives}");
        
        // DIMINUI as vidas
        currentLives--;
        
        Debug.Log($"[LivesUI] Player morreu! Vidas DEPOIS: {currentLives}");
        
        // Atualiza a UI
        UpdateLivesDisplay();
        
        // Se acabaram as vidas
        if (currentLives <= 0)
        {
            Debug.Log("[LivesUI] Game Over! Sem vidas!");
            // Dispara evento de Game Over (se você quiser usar)
            GameEvents.TriggerGameOver();
        }
    }

    /// <summary>
    /// Atualiza o texto na tela
    /// </summary>
    private void UpdateLivesDisplay()
    {
        if (livesText != null)
        {
            // Formato: ❤️ Vidas: 1
            livesText.text = "Vidas: " + currentLives;
            Debug.Log($"[LivesUI] Display atualizado: {currentLives} vidas");
        }
        else
        {
            Debug.LogError("[LivesUI] livesText não atribuído no Inspector!");
        }
    }

    /// <summary>
    /// Método público para obter as vidas atuais (se necessário)
    /// </summary>
    public int GetCurrentLives()
    {
        return currentLives;
    }

    /// <summary>
    /// Método para resetar as vidas (útil para reiniciar o jogo)
    /// Chame este método quando reiniciar a cena
    /// </summary>
    public void ResetLives()
    {
        currentLives = maxLives;
        hasInitialized = true;
        UpdateLivesDisplay();
        Debug.Log("[LivesUI] Vidas resetadas!");
    }

    /// <summary>
    /// Chamado quando a cena é carregada
    /// Reseta as vidas automaticamente
    /// </summary>
    void Start()
    {
        // Reseta as vidas quando a cena começa
        // Isso garante que sempre comece com maxLives
        currentLives = maxLives;
        UpdateLivesDisplay();
        Debug.Log($"[LivesUI] Start - Vidas: {currentLives}");
    }
}

