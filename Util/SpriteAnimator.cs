using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
    This class is for creating animations in various sprites by providing a list of sprites to swtich between and then rendering 
    things in that list iteratively.
*/
public class SpriteAnimator 
{
    SpriteRenderer spriteRenderer;      // Actually renders the selected sprite
    List<Sprite> frames;    // Contains the list of sprites that the animation will cycle through
    float frameRate;    // Controls the pace at which the sprites change
    float timer;    //  Keeps track of time
    int currentFrame;   // int to hold the current frame

    // Constructor 
    public SpriteAnimator(List<Sprite> frames, SpriteRenderer spriteRenderer, float frameRate=0.16f)
    {
        this.frames = frames;
        this.spriteRenderer = spriteRenderer;
        this.frameRate = frameRate;
    }


    public void Start()
    {
        currentFrame = 0;
        timer = 0;
        spriteRenderer.sprite = frames[0];
    }

    public void HandleUpdate()
    {
        timer += Time.deltaTime;    // increments timer every frame

        // If statement will switch to the next sprite when the timer has exceeded the stated frameRate. 
        if (timer > frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Count;   // Using mod in case the current frame is the last frame and then goes back to first sprite
            spriteRenderer.sprite = frames[currentFrame];   // Sets sprite to the sprite of the current frame index in the list
            timer -= frameRate;     // Resets the timer

        }
    }

    public List<Sprite> Frames
    {
        get { return frames; }
    }




}
