using UnityEngine;

public class Fruits : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;

    public GameObject collected;   // Pode estar vazio sem causar erro
    public int score = 10;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Esconder sprite e desativar collider
            sr.enabled = false;
            circle.enabled = false;

            // Ativar efeito se existir
            if (collected != null)
                collected.SetActive(true);

            // Atualizar score com proteção completa
            if (Gamecontroller.instance != null)
            {
                Gamecontroller.instance.totalScore += score;
                Gamecontroller.instance.UpdateTextMeshProUGUI();

                // Vitória ao alcançar 80 pontos
                if (Gamecontroller.instance.totalScore >= 80)
                    Gamecontroller.instance.ShowVictory();
            }
            else
            {
                Debug.LogError("Gamecontroller.instance está nulo! Verifique se existe Gamecontroller na cena.");
            }

            // Destruir fruta depois
            Destroy(gameObject, 0.3f);
        }
    }
}