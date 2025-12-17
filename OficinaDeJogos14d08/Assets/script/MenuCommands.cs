using UnityEngine;
using UnityEngine.SceneManagement;

// ===== INTERFACE =====
public interface ICommand
{
    void Execute();
}

// ===== COMANDO: JOGAR =====
public class PlayCommand : ICommand
{
    private string sceneName;
    
    public PlayCommand(string scene)
    {
        sceneName = scene;
    }
    
    public void Execute()
    {
        Debug.Log($"[PlayCommand] Carregando: {sceneName}");
        
        // Reseta as mortes ao iniciar um novo jogo
        if (SaveSystem.instance != null)
        {
            SaveSystem.instance.ResetDeaths();
        }
        
        SceneManager.LoadScene(sceneName);
    }
}

// ===== COMANDO: REINICIAR =====
public class RestartCommand : ICommand
{
    public void Execute()
    {
        Debug.Log("[RestartCommand] Reiniciando nível");
        Time.timeScale = 1f;
        
        // Reseta o score
        if (Gamecontroller.instance != null)
            Gamecontroller.instance.totalScore = 0;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

// ===== COMANDO: VOLTAR AO MENU =====
public class MenuCommand : ICommand
{
    private string menuScene;
    
    public MenuCommand(string scene)
    {
        menuScene = scene;
    }
    
    public void Execute()
    {
        Debug.Log($"[MenuCommand] Voltando para: {menuScene}");
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);
    }
}