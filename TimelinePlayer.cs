using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelinePlayer : MonoBehaviour
{
    public Dialog cutsceneDialogue;
    public GameObject dialogueCheck;
    public GameObject[] dialogues;
    public PlayableDirector timeline;
    
    public void HandleUpdate()
    {
        if (dialogueCheck.activeSelf)
        {
            dialogues = GameObject.FindGameObjectsWithTag("Dialogue");
            foreach(GameObject transcript in dialogues)
            {
                if (transcript.activeSelf && transcript.GetComponent<Transcript>().isDone == false)
                {
                    //Pausing the timeline
                    
                    timeline = this.GetComponent<PlayableDirector>();
                    timeline.playableGraph.GetRootPlayable(0).SetSpeed(0);

                    cutsceneDialogue = transcript.GetComponent<Transcript>().text;
                    transcript.GetComponent<Transcript>().isDone = true;
                    StartCoroutine(DialogManager.Instance.ShowDialog(cutsceneDialogue, true));

                    

                }
            }
        }
        

    }
}
