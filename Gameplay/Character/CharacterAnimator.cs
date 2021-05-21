using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    // List of sprites for animations to cycle through
    [SerializeField] List<Sprite> walkDownSprites;
    [SerializeField] List<Sprite> walkUpSprites;
    [SerializeField] List<Sprite> walkWestSprites;
    [SerializeField] List<Sprite> walkEastSprites;




    // Parameters such as MoveX and MoveY in the animator window from tutorial #2
    public float MoveX { get; set; }
    public float MoveY { get; set; }
    public bool IsMoving { get; set; }

    // States - Different animations for the character to play
    SpriteAnimator walkDownAnim;
    SpriteAnimator walkUpAnim;
    SpriteAnimator walkWestAnim;
    SpriteAnimator walkEastAnim;
    
    SpriteAnimator currentAnim;
    bool wasPreviouslyMoving;

    // Reference to sprite renderer
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        walkDownAnim = new SpriteAnimator(walkDownSprites, spriteRenderer); // No need to pass third argument as the default frameRate is already established in SpriteAnimator
        walkUpAnim = new SpriteAnimator(walkUpSprites, spriteRenderer);
        walkWestAnim = new SpriteAnimator(walkWestSprites, spriteRenderer);
        walkEastAnim = new SpriteAnimator(walkEastSprites, spriteRenderer);

        currentAnim = walkDownAnim;
    }

    private void Update()
    {
        var prevAnim = currentAnim;     // Variable used to check if animation selection has switched

        // Checks movement and assigns the proper animation to the spriteAnimator
        if (MoveX == 1)
        {
            currentAnim = walkEastAnim;
        }
        else if (MoveX == -1)
        {
            currentAnim = walkWestAnim;
        }
        else if (MoveY == 1)
        {
            currentAnim = walkUpAnim;
        }
        else if (MoveY == -1)
        {
            currentAnim = walkDownAnim;
        }

        // Will reset the timer, frame and sprite if the current anim and previous one are different, meaning that a change has been made in the animation selection
        if (currentAnim != prevAnim || IsMoving != wasPreviouslyMoving)
        {
            currentAnim.Start();
        }

        // Will only cycle through sprites (HandleUpdate) in the list when IsMoving is true
        if (IsMoving == true)
        {
            currentAnim.HandleUpdate();
        }
        // If it is not moving then it will sprite will be set to first in its list (idle sprite)
        else{
            spriteRenderer.sprite = currentAnim.Frames[0];
        }

        wasPreviouslyMoving = IsMoving;
    }

}
