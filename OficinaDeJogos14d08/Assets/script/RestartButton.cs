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
                FindObjectOfType<VictoryByScore>()?.RestartLevel();
            });
        }
        else
        {
            Debug.LogError("Nenhum componente Button encontrado no RestartButton!");
        }
    }
}