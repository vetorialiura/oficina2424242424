using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryByScore : MonoBehaviour
{
    [Tooltip("Painel que será mostrado quando o score alcançar targetScore.")]
    public GameObject victoryPanel;

    [Tooltip("Score alvo para vencer (ex: 80).")]
    public int targetScore = 80;

    [Tooltip("Se true, pausa o jogo quando abrir o painel.")]
    public bool pauseOnVictory = true;

    bool victoryShown = false;

    void Start()
    {
        if (victoryPanel != null)
            victoryPanel.SetActive(false);
        else
            Debug.LogWarning("VictoryByScore: victoryPanel não atribuído no inspector.");
    }

    void Update()
    {
        // evita checar se já mostrou a vitória
        if (victoryShown) return;

        // proteções contra NullReference
        if (Gamecontroller.instance == null) return;

        if (Gamecontroller.instance.totalScore >= targetScore)
        {
            ShowVictory();
        }
    }

    public void ShowVictory()
    {
        if (victoryShown) return;
        victoryShown = true;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        if (pauseOnVictory)
            Time.timeScale = 0f;
    }

    // método utilitário para botão "Menu" do painel
    public void GoToMenu(string menuSceneName = "Menu")
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
}