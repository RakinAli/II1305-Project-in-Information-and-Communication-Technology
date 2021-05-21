using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Character character;
    const float offsetY = 0.3f;
    private Vector2 input;

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void HandleUpdate()
    {
        if(!character.isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            // Remove Diagonal Movement
            if(input.x != 0)
            {
                input.y = 0;
            }

            //As long as input is not 0 (as in a button is being pressed) then it will call function every update
            if (input != Vector2.zero)
            {
                StartCoroutine(character.Move(input, OnMoveOver));
            }

        }

        character.HandleUpdate();

        // If space is pressed then interact function is called
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }

    }

    void Interact()
    {
        var facingDir = new Vector3(character.Animator.MoveX, character.Animator.MoveY);
        var interactPos = transform.position + facingDir;

        Debug.Log("Hello");

        // Will assign collider a gameobject if the overlap circle detects an object in the InteractableLayer
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.InteractableLayer);

        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }

    }

    

    private void OnMoveOver()
    {
        //Checks area character is on to find triggerables
        var colliders = Physics2D.OverlapCircleAll(transform.position - new Vector3(0, offsetY), 0.2f, GameLayers.i.TriggerableLayers);
    
        foreach(var collider in colliders)
        {
            var triggerable = collider.GetComponent<IPlayerTriggerable>();

            // If it is not null then that means the Game Object has an ITriggerable interface
            if (triggerable != null)
            {
                // Will run the "onPlayerTriggered" method that is on whatever the triggerable object is
                // Example: Triggerable is a patch of tall grass with pokemon inside, when character moves over, it
                // will call the "onPlayerTriggered" function on the Long grass object and pass this object as an argument
                // The tall grass version of the "onPlayerTriggered" method makes it so that you enter battle mode and fight a pokemon
                character.Animator.IsMoving = false;
                triggerable.onPlayerTriggered(this);
                break;
            }
        }
    
    
    
    }


    public Character Character => character;

   
    

}
