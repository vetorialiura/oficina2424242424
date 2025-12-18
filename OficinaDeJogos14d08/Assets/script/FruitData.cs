using UnityEngine;

/// <summary>
/// Scriptable Object para dados de frutas
/// Permite configurar diferentes tipos de frutas sem modificar código
/// </summary>
[CreateAssetMenu(fileName = "New Fruit Data", menuName = "Game/Fruit Data")]
public class FruitData : ScriptableObject
{
    [Header("Fruit Information")]
    [Tooltip("Nome da fruta")]
    public string fruitName;
    
    [Tooltip("Pontuação que a fruta dá ao ser coletada")]
    public int scoreValue;
    
    [Tooltip("Sprite visual da fruta")]
    public Sprite fruitSprite;
    
    [Header("Visual Effects")]
    [Tooltip("Cor das partículas ao coletar")]
    public Color particleColor = Color.white;
    
    [Tooltip("Efeito visual opcional ao coletar")]
    public GameObject collectEffect;
}