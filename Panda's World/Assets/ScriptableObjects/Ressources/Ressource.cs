using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ressource", menuName = "Ressource")]
public class Ressource : ScriptableObject
{
    [Tooltip("Nom de la Ressource.")]
    public string ressourceName;
    
    [Tooltip("Rule Tile associé a la ressource.")]
    public RuleTile ruleTile;
    
    // -------------------------------
    [Header("Ressource Settings")]
    
    [Tooltip("chance to spawn in percentage (spawn rate)"), Range(0,100)]
    public int rarity;
    [Tooltip("Life points")]
    public int pv;
    [Tooltip("Rate for bonus drop in percentage"), Range(0,100)]
    public int bonusDropRate; 
    [Tooltip("purity is a bonus value for the future"), Range(0,100)]
    public int purity;
    // -------------------------------
    
    
    
}