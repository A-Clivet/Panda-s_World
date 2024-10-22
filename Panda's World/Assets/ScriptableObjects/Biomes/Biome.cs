using System.Collections.Generic;
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

    // Fonctionnalité à ajouter si besoin / systeme de biome plus complexe
    
    // [Header("Environnement spécifiques au biome")]
    // [Tooltip("si environement supplémentaire.")]
    // public bool environment;
    //
    // [Range(1, 100)]
    // public int terrainVariation; // Exemple de paramètre spécifique
    //
    // [Tooltip("Rule Tile d'environement associé au biome.")] [CanBeNull]
    // public RuleTile environmentRuleTile;
    
    [Header("Ressources")]
    public Ressource[] ressources; // Liste des ressources associées à ce biome
    
}