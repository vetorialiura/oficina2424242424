using UnityEngine;

public static class GameEvents
{
    // ==================== EVENTO DE FRUTAS ====================
    public static event System.Action<string, int> OnFruitCollected;
    
    public static void TriggerFruitCollected(string fruitName, int scoreValue)
    {
        Debug.Log($"[GameEvents] TriggerFruitCollected: {fruitName} (+{scoreValue} pontos)");
        OnFruitCollected?.Invoke(fruitName, scoreValue);
    }

    // ==================== EVENTO DE SCORE ====================
    public static event System.Action<int> OnScoreChanged;
    
    public static void TriggerScoreChanged(int newScore)
    {
        Debug.Log($"[GameEvents] TriggerScoreChanged: {newScore}");
        OnScoreChanged?.Invoke(newScore);
    }

    // ==================== EVENTO DE MORTE DO PLAYER (NOVO!) ====================
    /// <summary>
    /// Evento disparado quando o player morre
    /// </summary>
    public static event System.Action OnPlayerDied;
    
    /// <summary>
    /// Dispara o evento de morte do player
    /// </summary>
    public static void TriggerPlayerDied()
    {
        Debug.Log("[GameEvents] TriggerPlayerDied");
        OnPlayerDied?.Invoke();
    }

    // ==================== EVENTOS DE GAME OVER/VITÓRIA ====================
    public static event System.Action OnGameOver;
    
    public static void TriggerGameOver()
    {
        Debug.Log("[GameEvents] TriggerGameOver");
        OnGameOver?.Invoke();
    }

    public static event System.Action OnVictory;
    
    public static void TriggerVictory()
    {
        Debug.Log("[GameEvents] TriggerVictory");
        OnVictory?.Invoke();
    }
}