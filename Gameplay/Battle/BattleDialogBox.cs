using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int letterPerSeconds;
    [SerializeField] Color highlightedColor;

    [SerializeField] Text dialogText;
    [SerializeField] Text quizText;
    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;
    [SerializeField] GameObject answerSelector;
    [SerializeField] GameObject correctAnimation;
    [SerializeField] GameObject wrongAnimation;

    [SerializeField] GameObject questionBackground;
    [SerializeField] GameObject quizTime;


    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTexts;
    [SerializeField] List<Text> answerTexts;

    [SerializeField] Text ppText;
    [SerializeField] Text typeText;

    public void SetDialog(string dialog) {

        dialogText.text = dialog;

    }

    public IEnumerator TypeDialog(string dialog) {

        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f/letterPerSeconds);
        }
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator TypeQuiz(string dialog) {

        quizText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            quizText.text += letter;
            yield return new WaitForSeconds(1f/letterPerSeconds);
        }
        yield return new WaitForSeconds(1f);
    }

    public void EnableQuizText(bool enabled) {
        quizText.enabled = enabled;
    }

    public void EnableDialogText(bool enabled) {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled) {
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled) {
        moveSelector.SetActive(enabled);
        // moveDetails.SetActive(enabled);
    }

    public void EnableAnswerSelector(bool enabled) {
        answerSelector.SetActive(enabled);
    }

    public void EnableCorrectAnimation(bool enabled) {
        correctAnimation.SetActive(enabled);
    }

    public void EnableWrongAnimation(bool enabled) {
        wrongAnimation.SetActive(enabled);
    }

    public void EnableQuestionBackground(bool enabled) {
        questionBackground.SetActive(enabled);
    }

    public void EnableQuizTime(bool enabled) {
        quizTime.SetActive(enabled);
    }

    public void UpdateActionSelection(int selectedAction) {
        for(int i = 0; i < actionTexts.Count; ++i) {
            if(i == selectedAction)
                actionTexts[i].color = highlightedColor;
            else
                actionTexts[i].color = Color.black;
        }
    }

    public void UpdateMoveSelection(int selectedMove, Move move) {
        for(int i = 0; i < moveTexts.Count; ++i) {
            if(i == selectedMove)
                moveTexts[i].color = highlightedColor;
            else
                moveTexts[i].color = Color.black;
        }

        // ppText.text = $"PP {move.PP}/{move.Base.PP}";
        // typeText.text = move.Base.Type.ToString();
    }

    public void UpdateAnswerSelection(int selectedMove, Move move) {
        for(int i = 0; i < answerTexts.Count; ++i) {
            if(i == selectedMove)
                answerTexts[i].color = highlightedColor;
            else
                answerTexts[i].color = Color.black;
        }
    }

    // public void SetMoveNames(List<Move> moves) {
    //     for(int i = 0; i < moveTexts.Count; ++i) {
    //         if(i < moves.Count)
    //             moveTexts[i].text = moves[i].Base.Name;
    //         else
    //             moveTexts[i].text = "-";
    //     }
    // }

    public int SetMoveNames(List<Move> moves) {
        int randomInt = Random.Range(0, moves.Count - 4);

        for(int i = 0; i < moveTexts.Count; ++i) {
            if(i < moves.Count)
                moveTexts[i].text = moves[randomInt + i].Base.Name;
            else
                moveTexts[i].text = "-";
        }

        return randomInt;
    }

    public void SetAnswerNames(List<Move> moves, int z) {
        for(int i = 0; i < 4; i++) {
            if(i < moves.Count)
                answerTexts[i].text = moves[4*z+i].Base.Name;
            else
                answerTexts[i].text = "-";
        }
    }

}
