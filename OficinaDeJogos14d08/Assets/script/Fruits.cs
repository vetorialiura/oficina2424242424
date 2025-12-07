using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Fruits : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public GameObject collected;
    public int score;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            sr.enabled = false;
            circle.enabled = false;
            collected.SetActive(true);

            Gamecontroller.instance.totalScore += score;
            Gamecontroller.instance.UpdateTextMeshProUGUI();

            //  ADICIONE ESTA LINHA AQUI 
            FindObjectOfType<VictoryManager>().AddFruit();

            Destroy(gameObject, 0.3f);
        }
    }
}