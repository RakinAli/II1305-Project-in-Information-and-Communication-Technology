using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Blocker : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int chapterReq;
    private bool isLoaded;

    public void Start()
    {
        isLoaded = false;
    }
    public void Update()
    {
        if(!isLoaded)
        {
            player = GameObject.Find("Player");
        
            if(player.GetComponent<Checkpoint>().progressArray[chapterReq] == true)
            {
                this.gameObject.SetActive(false);
                isLoaded = true;
                
            } 
        }
        
        
    }

    
}
