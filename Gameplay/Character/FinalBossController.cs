using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class FinalBossController : MonoBehaviour, Interactable
{
    [SerializeField] GameObject player;
    [SerializeField] Dialog dialog;
    [SerializeField] public bool hasFight;
    [SerializeField] Gubbe thisUnit;
    [SerializeField] int chapterUnlock;
    [SerializeField] bool hasCutscene;
    public GameObject timelineObject;
    private PlayableDirector timeline;
    [SerializeField] PlayableAsset sceneToPlay;
    
    public void Interact()
    {
        
        
        StartCoroutine(DialogManager.Instance.ShowDialog(dialog, hasFight, player, thisUnit));
        

        timelineObject = GameObject.Find("EndingTimeline");
        GameController.Instance.StartCutscene(timelineObject);
        timeline = timelineObject.GetComponent<PlayableDirector>();
            
        timeline.Play();
        this.gameObject.SetActive(false);
    }
}
