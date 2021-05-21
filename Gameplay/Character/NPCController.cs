using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] GameObject player;
    [SerializeField] Dialog dialog;
    [SerializeField] Dialog beatenDialog;
    [SerializeField] public bool hasFight;
    [SerializeField] Gubbe thisUnit;
    [SerializeField] int chapterUnlock;
    [SerializeField] bool hasCutscene;
    public GameObject timelineObject;
    private PlayableDirector timeline;
    [SerializeField] private string nameOfTimeline;
    [SerializeField] PlayableAsset sceneToPlay;

    public void Interact()
    {

        if(hasFight)
        {
            player = GameObject.Find("Player");
            player.GetComponent<Checkpoint>().chapterToUnlock = chapterUnlock;

            //turns off hasFight and changes boss dialogue after battle is won
            if(thisUnit.IsBeat) {
               hasFight = false;
               dialog = beatenDialog;
           }

            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, hasFight, player, thisUnit));
        }
        else if(hasCutscene)
        {
            timelineObject = GameObject.Find(nameOfTimeline);
            GameController.Instance.StartCutscene(timelineObject);
            timeline = timelineObject.GetComponent<PlayableDirector>();
            hasCutscene = false;
            timeline.Play();
        }
        else{
            player = GameObject.Find("Player");
            player.GetComponent<Checkpoint>().chapterToUnlock = chapterUnlock;

            //turns off hasFight and changes boss dialogue after battle is won
            if(thisUnit.IsBeat) {
               hasFight = false;
               dialog = beatenDialog;
           }

            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, hasFight, player, thisUnit));
        }
    }
}
