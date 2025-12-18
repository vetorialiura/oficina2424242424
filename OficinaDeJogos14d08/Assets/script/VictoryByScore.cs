using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Detecta vitória por pontuação
/// ATUALIZADO: Métodos públicos agora usam Command Pattern
/// </summary>
public class VictoryByScore : MonoBehaviour
{
    [Tooltip("Nome da cena de vitória que será carregada.")]
    public string victorySceneName = "VictoryScene";
    
    [Tooltip("Nome da cena do menu principal.")]
    public string menuSceneName = "Menu";
    
    [Tooltip("Score alvo para vencer (ex: 40).")]
    public int targetScore = 40;
    
    bool victoryTriggered = false;

    void Update()
    {
        if (victoryTriggered) return;
        
        if (Gamecontroller.instance == null) return;
        
        if (Gamecontroller.instance.totalScore >= targetScore)
        {
            LoadVictoryScene();
        }
    }

    public void LoadVictoryScene()
    {
        if (victoryTriggered) return;
        
        victoryTriggered = true;
        
        Debug.Log("[VictoryByScore] Carregando cena de vitória: " + victorySceneName);
        
        // Reseta o timeScale antes de carregar a cena
        Time.timeScale = 1f;
        
        if (string.IsNullOrEmpty(victorySceneName))
        {
            Debug.LogError("[VictoryByScore] victorySceneName está vazio. Defina o nome da cena no Inspector.");
            return;
        }
        
        SceneManager.LoadScene(victorySceneName);
    }

    // ==========================================
    // ATUALIZADO: Agora usa Command Pattern com histórico
    // ==========================================
    /// <summary>
    /// Reinicia o nível usando Command Pattern
    /// </summary>
    public void RestartLevel()
    {
        if (CommandHistory.instance != null)
        {
            ICommand restartCommand = new RestartCommand();
            CommandHistory.instance.ExecuteCommand(restartCommand);
        }
        else
        {
            // Fallback
            Debug.LogWarning("[VictoryByScore] CommandHistory não encontrado, executando diretamente");
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // ==========================================
    // ATUALIZADO: Agora usa Command Pattern com histórico
    // ==========================================
    /// <summary>
    /// Volta para o menu usando Command Pattern
    /// </summary>
    public void GoToMenu()
    {
        if (CommandHistory.instance != null)
        {
            ICommand menuCommand = new MenuCommand(menuSceneName);
            CommandHistory.instance.ExecuteCommand(menuCommand);
        }
        else
        {
            // Fallback
            Debug.LogWarning("[VictoryByScore] CommandHistory não encontrado, executando diretamente");
            Time.timeScale = 1f;
            SceneManager.LoadScene(menuSceneName);
        }
    }
}