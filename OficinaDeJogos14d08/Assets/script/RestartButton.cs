using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(() => {
                Debug.Log("BOTÃO RESTART CLICADO!");
                
                // Usando comando para reiniciar o nível
                ICommand restartCommand = new RestartCommand();
                restartCommand.Execute();
            });
        }
        else
        {
            Debug.LogError("Nenhum componente Button encontrado no RestartButton!");
        }
    }
}