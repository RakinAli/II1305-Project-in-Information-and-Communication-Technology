using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.Playables;
public class StartPortal : MonoBehaviour, IPlayerTriggerable
{
    //Set to -1 to avoid errors
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] DestinationIdentifier destinationPortal;
    playerMovement player;
    Fader fader;
    public PlayableDirector timeline;
    private GameObject timelineObject;
    [SerializeField] PlayableAsset sceneToPlay;

    private void Start()
    {
        fader = FindObjectOfType<Fader>();
    }
    public void onPlayerTriggered(playerMovement player)
    {
        this.player = player;
        Debug.Log("hi");
        StartCoroutine(StartGameScene());
    }

    IEnumerator StartGameScene()
    {

        // Tells unity to not destroy this portal when loading a new scene and that allows the entire code
        // here to be run since it won't get destroyed halfway through
        DontDestroyOnLoad(gameObject);

        timelineObject = GameObject.Find("TimelineManager");
        timeline = timelineObject.GetComponent<PlayableDirector>();
        
        GameController.Instance.StartCutscene(timelineObject);

        GameController.Instance.pauseGame(true);

        yield return fader.FadeIn(0.5f);

        // Loads scene that is specified in sceneToLoad
        yield return SceneManager.LoadSceneAsync(sceneToLoad);



        GameController.Instance.pauseGame(false);

        
        timeline.Play(sceneToPlay);
        // Need to manually destroy this portal since we carried it with us to the next scene
        Destroy(gameObject);
    }


    // These will allow us to connect specific portals to each other from scene to scene.
    // Portal B will only go to other Portals that are listed as B. So it will automatically seek out Portal B
    // in whatever scene that is being loaded and will ignore Portals A, C, D. One can list as many letters as you want here, it just gives you options.
    public enum DestinationIdentifier { A, B, C, D }

}
