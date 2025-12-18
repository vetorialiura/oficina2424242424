using UnityEngine;
using TMPro;

/// <summary>
/// Observer que GERENCIA e EXIBE o score
/// Este script é responsável por:
/// 1. Manter o valor do score
/// 2. Escutar eventos de coleta de frutas
/// 3. Atualizar a UI automaticamente
/// </summary>
public class ScoreUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Score Management")]
    private int currentScore = 0;

    /// <summary>
    /// Quando o componente é habilitado, INSCREVER-SE no evento de frutas coletadas
    /// </summary>
    void OnEnable()
    {
        // Inscreve no evento de coleta de frutas
        GameEvents.OnFruitCollected += OnFruitCollected;
        Debug.Log("[ScoreUI] Inscrito no evento OnFruitCollected");
        
        // Atualiza display inicial
        UpdateScoreDisplay();
    }

    /// <summary>
    /// Quando o componente é desabilitado, DESINSCREVER-SE do evento
    /// </summary>
    void OnDisable()
    {
        GameEvents.OnFruitCollected -= OnFruitCollected;
        Debug.Log("[ScoreUI] Desinscrito do evento OnFruitCollected");
    }

    /// <summary>
    /// Callback chamado quando uma fruta é coletada
    /// AQUI acontece a contagem!
    /// </summary>
    /// <param name="fruitName">Nome da fruta coletada</param>
    /// <param name="scoreValue">Pontos da fruta</param>
    private void OnFruitCollected(string fruitName, int scoreValue)
    {
        // ADICIONA os pontos ao score
        currentScore += scoreValue;
        
        Debug.Log($"[ScoreUI] Fruta '{fruitName}' coletada! +{scoreValue} pontos. Total: {currentScore}");
        
        // Atualiza a UI
        UpdateScoreDisplay();
        
        // Dispara evento para outros sistemas (como verificação de vitória)
        GameEvents.TriggerScoreChanged(currentScore);
    }

    /// <summary>
    /// Atualiza o texto na tela
    /// </summary>
    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
            Debug.Log($"[ScoreUI] Display atualizado: {currentScore}");
        }
        else
        {
            Debug.LogError("[ScoreUI] scoreText não atribuído no Inspector!");
        }
    }

    /// <summary>
    /// Método público para obter o score atual (se necessário)
    /// </summary>
    public int GetCurrentScore()
    {
        return currentScore;
    }

    /// <summary>
    /// Método para resetar o score (útil para reiniciar o jogo)
    /// </summary>
    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreDisplay();
        Debug.Log("[ScoreUI] Score resetado!");
    }
}
