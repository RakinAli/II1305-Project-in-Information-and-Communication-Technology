using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Playables;

public class EndOfOpeningCutsceneSceneChange : MonoBehaviour
{
    [SerializeField] playerMovement player;
    public PlayableDirector timeline;
    
    
    
    
    void OnEnable()
    {
        
        GameController.Instance.pauseGame(true);        
        
        SceneManager.LoadScene("Kistan Intro", LoadSceneMode.Single);

        GameController.Instance.pauseGame(false);

        GameController.Instance.setFreeroam();

        //timeline.Stop();
    }

    
        
    

}
