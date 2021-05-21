using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;


public class Fader : MonoBehaviour
{
    //Create reference to image
    Image image;

    // Assign the image
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public IEnumerator FadeIn(float time)
    {
        // The albedo for image is either 1 (on) or 0 (off) and so FadeIn wants the image to be visible and obscuring view
        yield return image.DOFade(1f, time).WaitForCompletion();

    }

    public IEnumerator FadeOut(float time)
    {
        // This is for Fading out and so ending value of the albedo needs to be 0. The time parameter simply tells us how long it shoudl take to do
        yield return image.DOFade(0f, time).WaitForCompletion();
    }
}
