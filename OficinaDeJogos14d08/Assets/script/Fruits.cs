using UnityEngine;

/// <summary>
/// Script de coleta de frutas usando Scriptable Object
/// Quando o player coleta, DISPARA um evento (Observer Pattern)
/// </summary>
public class Fruits : MonoBehaviour
{
    [Header("Fruit Configuration")]
    [SerializeField] private FruitData fruitData;

    [Header("Effects")]
    [SerializeField] private GameObject collected;
    [SerializeField] private int totalFruits;

    void Start()
    {
        // Validação do Scriptable Object
        if (fruitData == null)
        {
            Debug.LogError("[Fruits] FruitData não atribuído no Inspector! Adicione um Scriptable Object.");
        }
    }

    /// <summary>
    /// Detecta colisão com o player
    /// </summary>
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // Verifica se o fruitData está configurado
            if (fruitData == null)
            {
                Debug.LogError("[Fruits] Não é possível coletar fruta sem FruitData!");
                return;
            }

            // Log da coleta
            Debug.Log($"[Fruits] Coletou {fruitData.fruitName}: +{fruitData.scoreValue} pontos");

            // DISPARA O EVENTO - ScoreUI vai escutar!
            GameEvents.TriggerFruitCollected(fruitData.fruitName, fruitData.scoreValue);

            // Incrementa contador de frutas
            totalFruits++;

            // Efeito visual de coleta (se configurado)
            if (collected != null)
            {
                Instantiate(collected, transform.position, transform.rotation);
            }

            // Destrói a fruta
            Destroy(gameObject);
        }
    }
}