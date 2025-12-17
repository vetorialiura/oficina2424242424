using UnityEngine;

[CreateAssetMenu(fileName = "NewFruit", menuName = "Game/Fruit Data")]
public class FruitData : ScriptableObject
{
    [Header("Informações da Fruta")]
    public string fruitName = "Apple";
    
    [Header("Gameplay")]
    [Tooltip("Pontos que esta fruta dá ao ser coletada")]
    public int scoreValue = 10;
    
    [Header("Visual")]
    [Tooltip("Sprite principal da fruta")]
    public Sprite fruitSprite;
    
    [Tooltip("Prefab do efeito de coleta (partículas, animação, etc)")]
    public GameObject collectedEffectPrefab;
    
    [Header("Som (Opcional)")]
    [Tooltip("Áudio ao coletar a fruta")]
    public AudioClip collectSound;
    
    [Header("Configurações Extras")]
    [Tooltip("Tempo antes de destruir o objeto após coleta")]
    public float destroyDelay = 0.3f;
}