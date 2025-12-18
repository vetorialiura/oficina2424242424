using UnityEngine;

/// <summary>
/// Subject do padrão Observer
/// Gerencia TODOS os eventos do jogo
/// </summary>
public static class GameEvents
{
    // ==================== EVENTO DE FRUTAS ====================
    /// <summary>
    /// Evento disparado quando uma fruta é coletada
    /// Passa o nome da fruta e seus pontos
    /// </summary>
    public static event System.Action<string, int> OnFruitCollected;
    
    /// <summary>
    /// Dispara o evento de fruta coletada
    /// </summary>
    public static void TriggerFruitCollected(string fruitName, int scoreValue)
    {
        Debug.Log($"[GameEvents] TriggerFruitCollected: {fruitName} (+{scoreValue} pontos)");
        OnFruitCollected?.Invoke(fruitName, scoreValue);
    }

    // ==================== EVENTO DE SCORE ====================
    /// <summary>
    /// Evento disparado quando o score total muda
    /// Usado por outros sistemas (ex: verificar vitória)
    /// </summary>
    public static event System.Action<int> OnScoreChanged;
    
    /// <summary>
    /// Dispara o evento de mudança de score
    /// </summary>
    public static void TriggerScoreChanged(int newScore)
    {
        Debug.Log($"[GameEvents] TriggerScoreChanged: {newScore}");
        OnScoreChanged?.Invoke(newScore);
    }

    // ==================== EVENTOS DE GAME OVER/VITÓRIA ====================
    /// <summary>
    /// Evento disparado quando o jogador perde
    /// </summary>
    public static event System.Action OnGameOver;
    
    public static void TriggerGameOver()
    {
        Debug.Log("[GameEvents] TriggerGameOver");
        OnGameOver?.Invoke();
    }

    /// <summary>
    /// Evento disparado quando o jogador vence
    /// </summary>
    public static event System.Action OnVictory;
    
    public static void TriggerVictory()
    {
        Debug.Log("[GameEvents] TriggerVictory");
        OnVictory?.Invoke();
    }
}