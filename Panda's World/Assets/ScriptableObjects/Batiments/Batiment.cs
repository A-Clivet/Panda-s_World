using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Batiment", menuName = "Batiment")]
public class Batiment : ScriptableObject
{
    [Tooltip("Nom du Batiment.")]
    public string batimentName;

    [Tooltip("Rule Tile associé au Batiment.")]
    public RuleTile ruleTile;
    
    [Tooltip("Taille associé au Batiment : largeur.")]
    public int sizeX;
    [Tooltip("Taille associé au Batiment : hauteur.")]
    public int sizeY;

    [Tooltip("Prix associé au Batiment.")] 
    public int cost;




}