using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryManager : MonoBehaviour
{
    public GameObject panelVitoria;

    private int totalFruits;
    private int collected = 0;

    void Start()
    {
        // USE O NOME EXATO DA TAG
        totalFruits = GameObject.FindGameObjectsWithTag("fruit").Length;

        Debug.Log("FRUTAS DETECTADAS NA FASE: " + totalFruits);

        panelVitoria.SetActive(false);
    }

    public void AddFruit()
    {
        collected++;

        if (collected >= totalFruits)
        {
            panelVitoria.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}