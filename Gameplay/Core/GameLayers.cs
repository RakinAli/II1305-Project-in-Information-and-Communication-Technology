using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask portalLayer;
    [SerializeField] LayerMask cutsceneLayer;

    public static GameLayers i { get; set; }

    private void Awake()
    {
        i = this;
    }
    
    public LayerMask ObstacleLayer
    {
        get => obstacleLayer;
    }

    public LayerMask InteractableLayer
    {
        get => interactableLayer;
    }

    public LayerMask PortalLayer
    {
        get => portalLayer;
    }

    public LayerMask CutsceneLayer
    {
        get => cutsceneLayer;
    }

    public LayerMask TriggerableLayers 
    {
        get => portalLayer | cutsceneLayer;
    }


}
