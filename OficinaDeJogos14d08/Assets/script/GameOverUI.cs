using UnityEngine;

/// <summary>
/// Observer que escuta o evento de Game Over
/// Mostra o painel de Game Over quando o evento é disparado
/// 
/// COMO CONFIGURAR NO UNITY:
/// 1. Criar GameObject vazio: "GameOverManager"
/// 2. Adicionar este componente GameOverUI
/// 3. Arrastar o painel de Game Over para o campo "Game Over Panel"
/// </summary>
public class GameOverUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;

    /// <summary>
    /// INSCREVER-SE no evento quando habilitado
    /// </summary>
    void OnEnable()
    {
        GameEvents.OnGameOver += ShowGameOverPanel;
        Debug.Log("[GameOverUI] Inscrito no evento OnGameOver");
    }

    /// <summary>
    /// DESINSCREVER-SE do evento quando desabilitado
    /// </summary>
    void OnDisable()
    {
        GameEvents.OnGameOver -= ShowGameOverPanel;
        Debug.Log("[GameOverUI] Desinscrito do evento OnGameOver");
    }

    /// <summary>
    /// Método chamado automaticamente quando Game Over acontece
    /// </summary>
    private void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("[GameOverUI] Painel Game Over exibido");
        }
        else
        {
            Debug.LogError("[GameOverUI] gameOverPanel não atribuído no Inspector!");
        }
    }
}