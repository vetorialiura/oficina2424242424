using UnityEngine;
using TMPro;

public class MenuStatsUI : MonoBehaviour
{
    [Header("Referências UI")]
    public TextMeshProUGUI deathsText;
    public TextMeshProUGUI highScoreText;
    
    void Start()
    {
        UpdateUI();
    }
    
    void OnEnable()
    {
        // Atualiza sempre que a tela fica ativa
        UpdateUI();
    }
    
    void UpdateUI()
    {
        if (SaveSystem.instance != null)
        {
            PlayerStats stats = SaveSystem.instance.currentStats;
            
            if (deathsText != null)
            {
                deathsText.text = $"Mortes: {stats.totalDeaths}";
            }
            
            if (highScoreText != null)
            {
                highScoreText.text = $"Recorde: {stats.highScore}";
            }
            
            Debug.Log($"[MenuStatsUI] UI atualizada - Mortes: {stats.totalDeaths}");
        }
        else
        {
            Debug.LogWarning("[MenuStatsUI] SaveSystem.instance é nulo!");
        }
    }
}