using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public enum GameState { freeRoam, paused, battle, Dialog, cutscene}

public class GameController : MonoBehaviour
{
    [SerializeField] playerMovement playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] Camera freeRoamCam;
    [SerializeField] Checkpoint playerProgress;
    [SerializeField] TimelinePlayer cutsceneManager;
    
    public GameState state;
    GameState stateBeforePause;


    public static GameController Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        state = GameState.freeRoam;

        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };

        DialogManager.Instance.OnCloseDialogFight += () =>
        {
            if (state == GameState.Dialog)
            {
                state = GameState.battle;
            }
                                
                
        };

        DialogManager.Instance.OnCloseDialogNoFight += () =>
        {
            if (state == GameState.Dialog)
            {
                state = GameState.freeRoam;
            }
                                
                
        };

        DialogManager.Instance.OnCloseDialogCutscene += () =>
        {
            if (state == GameState.Dialog)
            {
                state = GameState.cutscene;
                cutsceneManager.GetComponent<PlayableDirector>().playableGraph.GetRootPlayable(0).SetSpeed(1);
            }
                                
                
        };
    }

    public void StartCutscene(GameObject timelineManager)
    {
        cutsceneManager = timelineManager.GetComponent<TimelinePlayer>();
        state = GameState.cutscene;
    }

    public void setFreeroam()
    {
        state = GameState.freeRoam;
    }

    public void StartBattle(Gubbe enemy)
    {
        
        state = GameState.battle;
        battleSystem.gameObject.SetActive(true);
        freeRoamCam.gameObject.SetActive(false);
        var _enemy = enemy;
        battleSystem.StartBattle(_enemy);
    }

    public void EndBattle(bool won)
    {
        state = GameState.freeRoam;
        battleSystem.gameObject.SetActive(false);
        freeRoamCam.gameObject.SetActive(true);

        if(won == true)
        {
            if(playerProgress.chapterToUnlock != -1)
            {
                
                playerProgress.Unlock(); 
                if(playerProgress.progressArray[11] == true)
                {
                    playerProgress.StartEnding();
                }
            }
        }
        else{
            playerProgress.chapterToUnlock = -1;
        }

        
    }

    public void pauseGame(bool pause)
    {
        if (pause)
        {
            stateBeforePause = state;
            state = GameState.paused;
        }
        else
        {
            state = stateBeforePause;
        }
    }

    public void setHasFight(bool hasFight)
    {
        hasFight = false;
    }

    private void Update()
    {
        // So the playerMovement update will only occur if the gamestate is set as freeRoam
        if (state == GameState.freeRoam)
        {
            playerController.HandleUpdate();
        }
        else if ( state == GameState.battle)
        {
            battleSystem.HandleUpdate();
        }
        else if(state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (state == GameState.cutscene)
        {
            cutsceneManager.HandleUpdate();
        }
    }
}
