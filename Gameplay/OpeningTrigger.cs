using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OpeningTrigger : MonoBehaviour, IPlayerTriggerable
{
    public GameObject timelineManager;
    public void onPlayerTriggered(playerMovement player)
    {
        GameController.Instance.pauseGame(true);
        timelineManager = GameObject.Find("TimelineManager");
        timelineManager.GetComponent<PlayableDirector>().Play();
        //timelineManager.GetComponent<PlayableDirector>().state == PlayState.Playing
        GameController.Instance.pauseGame(false);
    }


    IEnumerator playCutscene()
    {
        timelineManager = GameObject.Find("TimelineManager");
        timelineManager.GetComponent<PlayableDirector>().Play();
        yield return new WaitForSeconds(2f);
        

    }
    
}
