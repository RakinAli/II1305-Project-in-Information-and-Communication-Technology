using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Playables;
public class IntroEnder : MonoBehaviour
{
    void OnEnable()
    {
        
        GameController.Instance.pauseGame(true);        
        
        SceneManager.LoadScene("Kistan 2.0", LoadSceneMode.Single);

        GameController.Instance.pauseGame(false);

        GameController.Instance.setFreeroam();

        //timeline.Stop();
    }
}
