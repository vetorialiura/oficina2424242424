using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryByScore : MonoBehaviour
{
    [Tooltip("Nome da cena de vitória que será carregada.")]
    public string victorySceneName = "VictoryScene";
    
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
}