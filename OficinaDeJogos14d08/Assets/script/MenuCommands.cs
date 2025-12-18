using UnityEngine;
using UnityEngine.SceneManagement;

// INTERFACE CORRETA - DEVE TER ESTES 3 MÉTODOS
public interface ICommand
{
    void Execute();
    void Undo();
    string GetDescription();
}

// PlayCommand COMPLETO
public class PlayCommand : ICommand
{
    private string sceneName;
    private string previousScene;
    
    public PlayCommand(string scene)
    {
        sceneName = scene;
        previousScene = SceneManager.GetActiveScene().name;
    }
    
    public void Execute()
    {
        Debug.Log($"[PlayCommand] Carregando: {sceneName}");
        
        if (SaveSystem.instance != null)
        {
            SaveSystem.instance.ResetDeaths();
        }
        
        SceneManager.LoadScene(sceneName);
    }
    
    public void Undo()
    {
        Debug.Log($"[PlayCommand] Undo - Voltando para: {previousScene}");
        SceneManager.LoadScene(previousScene);
    }
    
    public string GetDescription()
    {
        return $"Play Game (Scene: {sceneName})";
    }
}

// RestartCommand COMPLETO
public class RestartCommand : ICommand
{
    private string currentScene;
    
    public RestartCommand()
    {
        currentScene = SceneManager.GetActiveScene().name;
    }
    
    public void Execute()
    {
        Debug.Log("[RestartCommand] Reiniciando nível");
        Time.timeScale = 1f;
        
        if (Gamecontroller.instance != null)
            Gamecontroller.instance.ResetScore();
        
        SceneManager.LoadScene(currentScene);
    }
    
    public void Undo()
    {
        Debug.LogWarning("[RestartCommand] Undo não aplicável para Restart");
    }
    
    public string GetDescription()
    {
        return $"Restart Level ({currentScene})";
    }
}

// MenuCommand COMPLETO
public class MenuCommand : ICommand
{
    private string menuScene;
    private string previousScene;
    
    public MenuCommand(string scene)
    {
        menuScene = scene;
        previousScene = SceneManager.GetActiveScene().name;
    }
    
    public void Execute()
    {
        Debug.Log($"[MenuCommand] Voltando para: {menuScene}");
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);
    }
    
    public void Undo()
    {
        Debug.Log($"[MenuCommand] Undo - Voltando para: {previousScene}");
        SceneManager.LoadScene(previousScene);
    }
    
    public string GetDescription()
    {
        return $"Go to Menu ({menuScene})";
    }
}