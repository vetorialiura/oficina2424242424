using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CarregarCena : MonoBehaviour
{

    public string cenaParacarregar; 
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene(cenaParacarregar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
