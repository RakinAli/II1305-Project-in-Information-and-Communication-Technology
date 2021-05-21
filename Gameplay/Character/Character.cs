using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public bool isMoving { get; private set; }
    public float OffsetY { get; private set; } = 0.3f;
    public float moveSpeed = 5f;
    public Vector3 targetPos;

    CharacterAnimator animator;

    private void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
        SetPositionAndSnapToTile(transform.position);
    }

    // Will snap the character to a specific tile to avoid any awkward placements 
    public void SetPositionAndSnapToTile(Vector2 pos)
    {
        pos.x = Mathf.Floor(pos.x) + 0.5f;
        pos.y = Mathf.Floor(pos.y) + 0.3f + OffsetY;

        transform.position = pos;
    }

    public IEnumerator Move(Vector2 moveVec, Action OnMoveOver = null){
         
        animator.MoveX = Mathf.Clamp(moveVec.x, -1f, 1f);
        animator.MoveY = Mathf.Clamp(moveVec.y, -1f, 1f);

        // Saves the input information to targetPos
        targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;

        // Checks if obstacle ahead
        if (!IsWalkable(targetPos))
        {
            yield break;
        }

        isMoving = true;

        while((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            //Debug.Log("Stuck in loop");
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        //Debug.Log("Got out of loop");
        transform.position = targetPos;

        isMoving = false;

        //Question mark means (in combination with "Action OnMoveOver = null" in parameters) that this won't be called if the Action was null
        OnMoveOver?.Invoke();
    }

    public void HandleUpdate()
    {
        animator.IsMoving = isMoving;
    }


    private bool IsPathClear(Vector3 targetPos)
    {
        var diff = targetPos - transform.position;
        var dir = diff.normalized;

        if (Physics2D.BoxCast(transform.position + dir, new Vector2(0.2f, 0.2f), 0f, dir, diff.magnitude - 1, GameLayers.i.ObstacleLayer) == true)
            return false;

        return true;
    }


    private bool IsWalkable(Vector3 targetPos)
    {
        //Creates overlap circle at targetPos and if it finds a game object that is tagged as the Obstacle Layer 
        // then it returns false, meaning that there is an obstacle in the way
        if (Physics2D.OverlapCircle(targetPos, 0.2f, GameLayers.i.ObstacleLayer | GameLayers.i.InteractableLayer) != null)
        {
            return false;
        }
        return true;
    }

    public CharacterAnimator Animator {
        get => animator;
    }
}
