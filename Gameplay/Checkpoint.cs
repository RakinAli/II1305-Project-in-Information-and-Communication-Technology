using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Checkpoint : MonoBehaviour
{
    public bool[] progressArray;
    public GameObject Grenn;
    public int chapterToUnlock;
    public PlayableDirector timeline;
    public GameObject timelineObject;

    public void Unlock()
    {
        progressArray[chapterToUnlock] = true;
    }

    public void StartEnding()
    {
        Grenn = GameObject.Find("Dr Grenn");
        Grenn.SetActive(false);
        timelineObject = GameObject.Find("EndingTimeline");
        GameController.Instance.StartCutscene(timelineObject);
        timeline.Play();
        
    }
}
