using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This simply updates the camera's position to be the same as the player + some offset in every update
public class playerfollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
  
    void Update () 
    {
        transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
    }
}
