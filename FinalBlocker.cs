using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class FinalBlocker : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] int chapterReq;
    [SerializeField] int chapterReq2;
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
        
            if(player.GetComponent<Checkpoint>().progressArray[chapterReq] == true 
            &&
            player.GetComponent<Checkpoint>().progressArray[chapterReq2] == true)
            {
                this.gameObject.SetActive(false);
                isLoaded = true;
                
            } 
        }
        
        
    }
}
