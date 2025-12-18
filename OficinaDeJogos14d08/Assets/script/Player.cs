using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D rig;
    
    public bool isJumping;
    public bool doublejump;
    private Animator anim;
    
    // === ADICIONE ESTA VARIÁVEL ===
    private bool isDead = false; // Previne múltiplas mortes
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;
        
        if (Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (Input.GetAxis("Horizontal") < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                doublejump = true;
                anim.SetBool("Jump", true);
            }
            else
            {
                if (doublejump)
                {
                    rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    doublejump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("Jump", false);
        }
        
        if (collision.gameObject.tag == "spike")
        {
            // === PROTEÇÃO: SÓ MORRE UMA VEZ ===
            if (isDead) return; // Já morreu, ignora
            isDead = true; // Marca como morto
            
            Debug.Log("[Player] MORREU NOS ESPINHOS!");
            
            // DISPARA O EVENTO - LivesUI vai escutar!
            GameEvents.TriggerPlayerDied();
            
            // Registra a morte no sistema de save
            if (SaveSystem.instance != null)
            {
                SaveSystem.instance.AddDeath();
            }
            
            // CARREGA A CENA DE GAME OVER
            StartCoroutine(LoadGameOverScene());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }
    
    IEnumerator LoadGameOverScene()
    {
        // Opcional: pequeno delay antes de carregar a cena
        yield return new WaitForSeconds(0.5f);
        
        // Carrega a cena de Game Over
        SceneManager.LoadScene("CUTSCENE - Derrota");
    }
}