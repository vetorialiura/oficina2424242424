using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ADICIONE ESTA LINHA

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D rig;
    
    public bool isJumping;
    public bool doublejump;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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
    
    // ADICIONE ESTA FUNÇÃO
    IEnumerator LoadGameOverScene()
    {
        // Opcional: pequeno delay antes de carregar a cena
        yield return new WaitForSeconds(0.5f);
        
        // Carrega a cena de Game Over (troque "GameOver" pelo nome da sua cena)
        SceneManager.LoadScene("CUTSCENE - Derrota");
    }
}