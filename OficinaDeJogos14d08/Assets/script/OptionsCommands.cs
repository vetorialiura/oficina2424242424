using UnityEngine;

// ==========================================
// COMANDO: MUDAR VOLUME
// ==========================================
/// <summary>
/// Comando para alterar o volume do jogo
/// Salva o volume anterior para permitir Undo
/// </summary>
public class ChangeVolumeCommand : ICommand
{
    private float newVolume;
    private float previousVolume;

    public ChangeVolumeCommand(float volume)
    {
        this.previousVolume = AudioListener.volume;
        this.newVolume = volume;
    }

    public void Execute()
    {
        AudioListener.volume = newVolume;
        Debug.Log($"[ChangeVolumeCommand] Volume alterado: {previousVolume:F2} → {newVolume:F2}");
    }

    public void Undo()
    {
        AudioListener.volume = previousVolume;
        Debug.Log($"[ChangeVolumeCommand] Undo - Volume: {newVolume:F2} → {previousVolume:F2}");
    }

    public string GetDescription()
    {
        return $"Change Volume ({previousVolume:F2} → {newVolume:F2})";
    }
}

// ==========================================
// COMANDO: MUDAR QUALIDADE GRÁFICA
// ==========================================
/// <summary>
/// Comando para alterar a qualidade gráfica do jogo
/// Salva a qualidade anterior para permitir Undo
/// </summary>
public class ChangeQualityCommand : ICommand
{
    private int newQualityLevel;
    private int previousQualityLevel;
    private string[] qualityNames = { "Low", "Medium", "High", "Ultra" };

    public ChangeQualityCommand(int qualityLevel)
    {
        this.previousQualityLevel = QualitySettings.GetQualityLevel();
        this.newQualityLevel = qualityLevel;
    }

    public void Execute()
    {
        QualitySettings.SetQualityLevel(newQualityLevel);
        Debug.Log($"[ChangeQualityCommand] Qualidade: {GetQualityName(previousQualityLevel)} → {GetQualityName(newQualityLevel)}");
    }

    public void Undo()
    {
        QualitySettings.SetQualityLevel(previousQualityLevel);
        Debug.Log($"[ChangeQualityCommand] Undo - Qualidade: {GetQualityName(newQualityLevel)} → {GetQualityName(previousQualityLevel)}");
    }

    public string GetDescription()
    {
        return $"Change Quality ({GetQualityName(previousQualityLevel)} → {GetQualityName(newQualityLevel)})";
    }

    private string GetQualityName(int level)
    {
        if (level >= 0 && level < qualityNames.Length)
            return qualityNames[level];
        return $"Level {level}";
    }
}

// ==========================================
// COMANDO: MUDAR FULLSCREEN
// ==========================================
/// <summary>
/// Comando para alternar entre tela cheia e janela
/// </summary>
public class ToggleFullscreenCommand : ICommand
{
    private bool wasFullscreen;

    public ToggleFullscreenCommand()
    {
        this.wasFullscreen = Screen.fullScreen;
    }

    public void Execute()
    {
        Screen.fullScreen = !wasFullscreen;
        Debug.Log($"[ToggleFullscreenCommand] Fullscreen: {wasFullscreen} → {Screen.fullScreen}");
    }

    public void Undo()
    {
        Screen.fullScreen = wasFullscreen;
        Debug.Log($"[ToggleFullscreenCommand] Undo - Fullscreen: {!wasFullscreen} → {wasFullscreen}");
    }

    public string GetDescription()
    {
        return $"Toggle Fullscreen ({wasFullscreen} → {!wasFullscreen})";
    }
}