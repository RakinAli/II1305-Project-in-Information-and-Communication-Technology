using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class Portal : MonoBehaviour, IPlayerTriggerable
{

    //Set to -1 to avoid errors
    [SerializeField] int sceneToLoad = -1;
    [SerializeField] public Transform spawnPoint;
    [SerializeField] DestinationIdentifier destinationPortal;
    playerMovement player;
    Fader fader;

    private void Start()
    {
        fader = FindObjectOfType<Fader>();
    }
    public void onPlayerTriggered(playerMovement player)
    {
        this.player = player;
        StartCoroutine(SwitchScene());
    }

    IEnumerator SwitchScene()
    {
        // Tells unity to not destroy this portal when loading a new scene and that allows the entire code
        // here to be run since it won't get destroyed halfway through
        DontDestroyOnLoad(gameObject);

        GameController.Instance.pauseGame(true);

        yield return fader.FadeIn(0.5f);

        // Loads scene that is specified in sceneToLoad
        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        // Will return the first portal found in the new scene excluding itself
        var destPortal = FindObjectsOfType<Portal>().First(x => x != this && x.destinationPortal == this.destinationPortal);
        
        // Snaps character to correct tile
        player.Character.SetPositionAndSnapToTile(destPortal.spawnPoint.position);

        yield return fader.FadeOut(0.5f);

        GameController.Instance.pauseGame(false);
        
        // Need to manually destroy this portal since we carried it with us to the next scene
        Destroy(gameObject);
    }


    // These will allow us to connect specific portals to each other from scene to scene.
    // Portal B will only go to other Portals that are listed as B. So it will automatically seek out Portal B
    // in whatever scene that is being loaded and will ignore Portals A, C, D. One can list as many letters as you want here, it just gives you options.
    public enum DestinationIdentifier { A, B, C, D }
}
