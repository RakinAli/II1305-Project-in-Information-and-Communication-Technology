using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCheck : MonoBehaviour
{
    [SerializeField] int req1;
    [SerializeField] int req2;
    [SerializeField] GameObject player;
    private bool bossStart;

    public void Start()
    {
        bossStart = false;
    }
    public void Update()
    {
        player = GameObject.Find("Player");
        if (player.GetComponent<Checkpoint>().progressArray[req1] == true 
            && 
            player.GetComponent<Checkpoint>().progressArray[req2] == true
            &&
            bossStart == false)
        {
            GetComponent<NPCController>().hasFight = true;
            bossStart = true;
        }
    }
}
