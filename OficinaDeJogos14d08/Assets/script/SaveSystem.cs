using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int totalDeaths = 0;
    public int highScore = 0;
    public int totalFruitsCollected = 0;
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    
    private string savePath;
    public PlayerStats currentStats;
    
    void Awake()
    {
        // Singleton
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Define o caminho do arquivo de save
        savePath = Path.Combine(Application.persistentDataPath, "playerdata.json");
        Debug.Log($"[SaveSystem] Caminho do save: {savePath}");
        
        // Carrega dados ao iniciar
        LoadGame();
    }
    
    // Salva os dados em JSON
    public void SaveGame()
    {
        try
        {
            string json = JsonUtility.ToJson(currentStats, true);
            File.WriteAllText(savePath, json);
            Debug.Log($"[SaveSystem] Jogo salvo! Mortes: {currentStats.totalDeaths}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveSystem] Erro ao salvar: {e.Message}");
        }
    }
    
    // Carrega os dados do JSON
    public void LoadGame()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string json = File.ReadAllText(savePath);
                currentStats = JsonUtility.FromJson<PlayerStats>(json);
                Debug.Log($"[SaveSystem] Jogo carregado! Mortes: {currentStats.totalDeaths}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Erro ao carregar: {e.Message}");
                currentStats = new PlayerStats();
            }
        }
        else
        {
            Debug.Log("[SaveSystem] Nenhum save encontrado. Criando novo...");
            currentStats = new PlayerStats();
            SaveGame();
        }
    }
    
    // Incrementa mortes e salva
    public void AddDeath()
    {
        currentStats.totalDeaths++;
        Debug.Log($"[SaveSystem] Morte registrada! Total: {currentStats.totalDeaths}");
        SaveGame();
    }
    
    // Atualiza high score se necessário
    public void UpdateHighScore(int score)
    {
        if (score > currentStats.highScore)
        {
            currentStats.highScore = score;
            Debug.Log($"[SaveSystem] Novo recorde: {currentStats.highScore}");
            SaveGame();
        }
    }
    
    // Reseta os dados (útil para debug)
    public void ResetStats()
    {
        currentStats = new PlayerStats();
        SaveGame();
        Debug.Log("[SaveSystem] Estatísticas resetadas!");
    }
}