using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This interface makes it so that whenever a class that implements IPlayerTriggerable calls the onPlayerTriggered 
// function that is found on the triggerable object
// 
public interface IPlayerTriggerable 
{
    void onPlayerTriggered(playerMovement player);
    
}
