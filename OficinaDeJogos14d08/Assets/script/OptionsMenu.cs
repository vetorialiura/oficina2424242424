using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Menu de opções usando Command Pattern
/// Todas as mudanças são comandos que vão pro histórico
/// 
/// COMO CONFIGURAR NO UNITY:
/// 1. Criar Canvas de opções com:
///    - Slider (Volume)
///    - Dropdown (Quality)
///    - Toggle (Fullscreen) - opcional
///    - Button "Apply"
///    - Button "Undo"
///    - Text (History Display)
/// 2. Adicionar este script ao Canvas
/// 3. Arrastar os elementos para os campos no Inspector
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Dropdown qualityDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Button applyButton;
    [SerializeField] private Button undoButton;
    [SerializeField] private TextMeshProUGUI historyText;

    void Start()
    {
        // Configurar valores iniciais
        if (volumeSlider != null)
            volumeSlider.value = AudioListener.volume;

        if (qualityDropdown != null)
            qualityDropdown.value = QualitySettings.GetQualityLevel();

        if (fullscreenToggle != null)
            fullscreenToggle.isOn = Screen.fullScreen;

        // Configurar botões
        if (applyButton != null)
            applyButton.onClick.AddListener(ApplyChanges);

        if (undoButton != null)
            undoButton.onClick.AddListener(UndoLastChange);

        UpdateHistoryDisplay();
    }

    /// <summary>
    /// Aplica mudanças usando comandos
    /// IMPORTANTE: Acumula as mudanças no histórico
    /// </summary>
    public void ApplyChanges()
    {
        if (CommandHistory.instance == null)
        {
            Debug.LogError("[OptionsMenu] CommandHistory não encontrado!");
            return;
        }

        // Comando de Volume (só se mudou)
        if (volumeSlider != null && !Mathf.Approximately(AudioListener.volume, volumeSlider.value))
        {
            ICommand volumeCmd = new ChangeVolumeCommand(volumeSlider.value);
            CommandHistory.instance.ExecuteCommand(volumeCmd);
        }

        // Comando de Qualidade (só se mudou)
        if (qualityDropdown != null && QualitySettings.GetQualityLevel() != qualityDropdown.value)
        {
            ICommand qualityCmd = new ChangeQualityCommand(qualityDropdown.value);
            CommandHistory.instance.ExecuteCommand(qualityCmd);
        }

        // Comando de Fullscreen (só se mudou)
        if (fullscreenToggle != null && Screen.fullScreen != fullscreenToggle.isOn)
        {
            ICommand fullscreenCmd = new ToggleFullscreenCommand();
            CommandHistory.instance.ExecuteCommand(fullscreenCmd);
        }

        UpdateHistoryDisplay();
    }

    /// <summary>
    /// Desfaz última mudança
    /// </summary>
    public void UndoLastChange()
    {
        if (CommandHistory.instance != null)
        {
            CommandHistory.instance.UndoLastCommand();
            
            // Atualizar UI com os valores reais
            if (volumeSlider != null)
                volumeSlider.value = AudioListener.volume;
            
            if (qualityDropdown != null)
                qualityDropdown.value = QualitySettings.GetQualityLevel();

            if (fullscreenToggle != null)
                fullscreenToggle.isOn = Screen.fullScreen;
            
            UpdateHistoryDisplay();
        }
    }

    /// <summary>
    /// Atualiza display do histórico
    /// </summary>
    private void UpdateHistoryDisplay()
    {
        if (historyText == null || CommandHistory.instance == null)
            return;

        var history = CommandHistory.instance.GetHistory();
        
        if (history.Count == 0)
        {
            historyText.text = "Histórico de Comandos:\n(vazio)";
        }
        else
        {
            historyText.text = "Histórico de Comandos:\n" + string.Join("\n", history);
        }
    }

    /// <summary>
    /// Limpa todo o histórico
    /// </summary>
    public void ClearHistory()
    {
        if (CommandHistory.instance != null)
        {
            CommandHistory.instance.ClearHistory();
            UpdateHistoryDisplay();
        }
    }
}