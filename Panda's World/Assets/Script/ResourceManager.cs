using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager resourcesManager { get; set; }
    
    public Dictionary<Resource,Vector2Int> ResourcesPos = new Dictionary<Resource, Vector2Int>(); // faire une list pour chaques ressources

    public void AddResource(Resource resource, Vector2Int position)
    {
        
    }
}