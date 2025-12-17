using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryByScore : MonoBehaviour
{
    [Tooltip("Painel que será mostrado quando o score alcançar targetScore.")]
    public GameObject victoryPanel;

    [Tooltip("Score alvo para vencer (ex: 40).")]
    public int targetScore = 40;

    [Tooltip("Se true, pausa o jogo quando abrir o painel.")]
    public bool pauseOnVictory = true;

    [Tooltip("Nome da cena do menu para onde voltar.")]
    public string menuSceneName = "MenuInicial";

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
        if (victoryShown) return;
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

    public void GoToMenu()
    {
        ICommand menuCommand = new MenuCommand(menuSceneName);
        menuCommand.Execute();
    }

    public void RestartLevel()
    {
        ICommand restartCommand = new RestartCommand();
        restartCommand.Execute();
    }
}