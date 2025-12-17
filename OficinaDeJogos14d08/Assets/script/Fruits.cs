using UnityEngine;

public class Fruits : MonoBehaviour
{
    [Header("Dados da Fruta")]
    [Tooltip("Scriptable Object com os dados desta fruta")]
    public FruitData fruitData;
    
    private SpriteRenderer sr;
    private CircleCollider2D circle;
    private GameObject collectedInstance;
    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
        
        // Valida se fruitData foi atribuído
        if (fruitData == null)
        {
            Debug.LogError($"[Fruits] FruitData não atribuído em {gameObject.name}!");
            return;
        }
        
        // Aplica o sprite do SO
        if (fruitData.fruitSprite != null && sr != null)
        {
            sr.sprite = fruitData.fruitSprite;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (fruitData == null)
            {
                Debug.LogError("[Fruits] FruitData está nulo!");
                return;
            }
            
            // Esconder sprite e desativar collider
            sr.enabled = false;
            circle.enabled = false;
            
            // Ativar efeito de coleta se existir no SO
            if (fruitData.collectedEffectPrefab != null)
            {
                collectedInstance = Instantiate(
                    fruitData.collectedEffectPrefab, 
                    transform.position, 
                    Quaternion.identity
                );
                collectedInstance.SetActive(true);
            }
            // Logo depois de verificar se fruitData != null
            Debug.Log($"[Fruits] Coletado: {fruitData.fruitName} | Pontos: {fruitData.scoreValue} | Delay: {fruitData.destroyDelay}s");
            
            // Tocar som se existir (opcional - requer AudioSource)
            if (fruitData.collectSound != null)
            {
                AudioSource.PlayClipAtPoint(fruitData.collectSound, transform.position);
            }
            
            // Atualizar score usando Observer Pattern
            if (Gamecontroller.instance != null)
            {
                Gamecontroller.instance.AddScore(fruitData.scoreValue);
                
                // Vitória ao alcançar 80 pontos
                if (Gamecontroller.instance.totalScore >= 80)
                    Gamecontroller.instance.ShowVictory();
            }
            else
            {
                Debug.LogError("[Fruits] Gamecontroller.instance está nulo!");
            }
            
            // Destruir fruta após o delay definido no SO
            Destroy(gameObject, fruitData.destroyDelay);
        }
    }
}