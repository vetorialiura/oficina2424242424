using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    
    void OnEnable()
    {
        // Inscreve-se no evento
        GameEvents.OnScoreChanged += UpdateScoreUI;
    }
    
    void OnDisable()
    {
        // Desinscreve-se para evitar memory leaks
        GameEvents.OnScoreChanged -= UpdateScoreUI;
    }
    
    void Start()
    {
        // Inicializa com o score atual
        if (Gamecontroller.instance != null)
        {
            UpdateScoreUI(Gamecontroller.instance.totalScore);
        }
    }
    
    void UpdateScoreUI(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = newScore.ToString();
            Debug.Log($"[ScoreUI] UI atualizada para: {newScore}");
        }
        else
        {
            Debug.LogWarning("[ScoreUI] scoreText não está atribuído!");
        }
    }
}