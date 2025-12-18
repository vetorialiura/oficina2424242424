using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Botão de restart que usa Command Pattern
/// ATUALIZADO: Agora usa CommandHistory para histórico de comandos
/// </summary>
public class RestartButton : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() => {
                Debug.Log("[RestartButton] Botão clicado!");
                
                // Usando Command Pattern com histórico
                if (CommandHistory.instance != null)
                {
                    ICommand restartCommand = new RestartCommand();
                    CommandHistory.instance.ExecuteCommand(restartCommand);
                }
                else
                {
                    Debug.LogWarning("[RestartButton] CommandHistory não encontrado, executando diretamente");
                    ICommand restartCommand = new RestartCommand();
                    restartCommand.Execute();
                }
            });
        }
        else
        {
            Debug.LogError("[RestartButton] Nenhum componente Button encontrado!");
        }
    }
}