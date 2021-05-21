using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int letterPerSeconds;
    public bool hasCutscene;
    public PlayableDirector timeline;
    private GameObject timeManager;
    public event Action OnShowDialog;
    public event Action OnCloseDialogFight;
    public event Action OnCloseDialogNoFight;
    public event Action OnCloseDialogCutscene;

    //fighting variables
    GameObject player;
    Dialog dialog;
    public bool hasFight;
    Gubbe thisUnit;
    int chapterUnlock;


    public static DialogManager Instance { get; private set;}

    private void Awake()
    {
        Instance = this;
    }

    // Dialog dialog;
    int currentLine = 0;
    bool isTyping;

    public IEnumerator ShowDialog(Dialog dialog, bool hasCutscene)
    {

        yield return new WaitForEndOfFrame();

        this.hasCutscene = hasCutscene;
        OnShowDialog?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public IEnumerator ShowDialog(Dialog dialog, bool _hasFight, GameObject _player, Gubbe _thisUnit)
    {
        this.hasFight = _hasFight;

        this.player = _player;
        this.thisUnit = _thisUnit;
        hasCutscene = false;

        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        this.dialog = dialog;
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[0]));
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            ++currentLine;
            if(currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogBox.SetActive(false);
                if(hasFight)
                {
                    OnCloseDialogFight?.Invoke();
                    hasFightStart();
                }
                else{
                    if (hasCutscene)
                    {
                        OnCloseDialogCutscene?.Invoke();
                    }
                    else
                    {
                        OnCloseDialogNoFight?.Invoke();
                    }
                }

            }
        }
    }

    public IEnumerator TypeDialog(string dialog)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSeconds);
        }
        isTyping = false;


    }

    public void hasFightStart() {
        hasCutscene = false;
        player = GameObject.Find("Player");
        hasFight = false;
        
        thisUnit.Init();
        GameController.Instance.StartBattle(thisUnit);
    }



}
