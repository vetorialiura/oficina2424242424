using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class EfeitoDigitador : MonoBehaviour
{
    private TextMeshProUGUI componentetexto;
    private AudioSource _audiosource;
    private string mensagemOriginal;
    public bool imprimindo;
    public float TempoEntreLetras = 0.1f;

    private void Awake()
    {
        TryGetComponent(out componentetexto);
        TryGetComponent(out _audiosource);
        mensagemOriginal = componentetexto.text;
        componentetexto.text = "";
    }

    private void OnEnable()
    {
        imprimirMensagem(mensagemOriginal);
    }

    private void OnDisable()
    {
        componentetexto.text = mensagemOriginal;
        StopAllCoroutines();
    }

    private void imprimirMensagem(string mensagem)
    {
        if (gameObject.activeInHierarchy)
        {
            if (imprimindo )return;
            imprimindo = true;
            StartCoroutine(LetraPorLetra(mensagem));
        }
    }

    IEnumerator LetraPorLetra(string mensagem)
    {
        string msg = "";
        foreach (var letra in mensagem)
        {
            msg += letra;
            componentetexto.text = msg;
            _audiosource.Play();
            yield return new WaitForSeconds(TempoEntreLetras);
        }

        imprimindo = false;
        StopAllCoroutines();

    }
}
