using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Biome", menuName = "Biome")]
public class Biome : ScriptableObject
{
    [Tooltip("Nom du biome.")]
    public string biomeName;

    [Tooltip("Seuil de Perlin Noise pour assigner ce biome.")]
    public float threshold;

    [Tooltip("Rule Tile associé au biome.")]
    public RuleTile ruleTile;

    [Header("Environnement spécifiques au biome")]
    [Tooltip("si environement supplémentaire.")]
    public bool environment;

    public float terrainVariation; // Exemple de paramètre spécifique
    // Ajoutez d'autres paramètres spécifiques selon vos besoins
    
    [Tooltip("Rule Tile d'environement associé au biome.")] [CanBeNull]
    public RuleTile environmentRuleTile;
    
}