using System;
using UnityEngine;

public static class GameEvents
{
    // Evento para atualização de score
    public static event Action<int> OnScoreChanged;
    
    // Evento para Game Over
    public static event Action OnGameOver;
    
    // Evento para Vitória
    public static event Action OnVictory;
    
    // Métodos para disparar os eventos
    public static void TriggerScoreChanged(int newScore)
    {
        OnScoreChanged?.Invoke(newScore);
        Debug.Log($"[GameEvents] Score mudou para: {newScore}");
    }
    
    public static void TriggerGameOver()
    {
        OnGameOver?.Invoke();
        Debug.Log("[GameEvents] Game Over disparado!");
    }
    
    public static void TriggerVictory()
    {
        OnVictory?.Invoke();
        Debug.Log("[GameEvents] Vitória disparada!");
    }
}