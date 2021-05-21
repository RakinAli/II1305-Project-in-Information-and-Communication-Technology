using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public enum BattleState { Start, PlayerAnswer, PlayerAction, PlayerMove, EnemyMove, Busy}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleHUD playerHud;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHUD enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    BattleState state;
    int currentAction;
    int currentMove;
    int startIndex;
    int currentQuestion;
	int currentAnswer;
    Gubbe enemy;
    [SerializeField] Gubbe player;



    public void StartBattle(Gubbe enemy)
    {

        this.enemy = enemy;



        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle() {

        currentQuestion = 0;
        player.Init();
        playerUnit.setup(player);
        enemyUnit.setup(enemy);
        playerHud.SetData(playerUnit.gubbe);
        enemyHud.SetData(enemyUnit.gubbe);


        yield return dialogBox.TypeDialog($"Get ready for {enemyUnit.gubbe.Base.Name}'s test!");
        yield return new WaitForSeconds(1f);

        // dialogBox.SetMoveNames(playerUnit.gubbe.Moves);


        PlayerAction();
    }

    void PlayerAction() {
        dialogBox.EnableDialogText(true);
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an action"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove() {
        startIndex = dialogBox.SetMoveNames(playerUnit.gubbe.Moves);
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableDialogText(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;
        var move = playerUnit.gubbe.Moves[startIndex + currentMove];
        yield return dialogBox.TypeDialog($"{playerUnit.gubbe.Base.Name} used {move.Base.Name}");
        yield return new WaitForSeconds(1f);

        bool isFainted = enemyUnit.gubbe.TakeDamage(move, playerUnit.gubbe);
        yield return enemyHud.UpdateHP();

        if ( isFainted)
        {
            yield return dialogBox.TypeDialog($"You passed {enemyUnit.gubbe.Base.Name}'s test. Nicely done!");
            enemyUnit.gubbe.IsBeat = true;
            yield return new WaitForSeconds(2f);
            GameController.Instance.EndBattle(true);
        }
        else{
            StartCoroutine(EnemyMove());
        }

    }

    IEnumerator PerformPlayerAnswer()
    {
        state = BattleState.Busy;
        dialogBox.EnableDialogText(true);

		var move = enemyUnit.gubbe.Answers[(currentQuestion++)*4+currentAnswer];

		if (move.Base.Power > 0) {
            yield return dialogBox.TypeDialog("How did you think here?????");
            dialogBox.EnableWrongAnimation(true);
            FindObjectOfType<AudioManager>().Play("what");
        }
        else {
            yield return dialogBox.TypeDialog("Correct!");
            dialogBox.EnableCorrectAnimation(true);
            FindObjectOfType<AudioManager>().Play("correct");
        }

        yield return new WaitForSeconds(3f);

		if(currentQuestion	==	enemyUnit.gubbe.Base.LearnableMoves.Count) {
            currentQuestion = 0;
        }

        bool isFainted = playerUnit.gubbe.TakeDamage(move, enemyUnit.gubbe);
        yield return playerHud.UpdateHP();

        //stänger av animation
        dialogBox.EnableCorrectAnimation(false);
        dialogBox.EnableWrongAnimation(false);

        if ( isFainted)
        {
            yield return dialogBox.TypeDialog($"You failed {enemyUnit.gubbe.Base.Name}'s test. Give it another shot!");

            yield return new WaitForSeconds(2f);
            GameController.Instance.EndBattle(false);
        }
        else{
            PlayerAction();
        }
        dialogBox.EnableQuizText(false);
        dialogBox.EnableQuestionBackground(false);
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;
        // ("currentQuestion:");
        // (currentQuestion);
		dialogBox.SetAnswerNames(enemyUnit.gubbe.Answers, currentQuestion);
        var move = enemyUnit.gubbe.GetNextQuestion(currentQuestion);


        yield return dialogBox.TypeDialog($"{enemyUnit.gubbe.Base.Name} hands you the test's next part.");
        dialogBox.EnableQuizTime(true);

        yield return new WaitForSeconds(2f);
        dialogBox.EnableQuizTime(false);

        dialogBox.EnableQuizText(true);
        dialogBox.EnableQuestionBackground(true);
        yield return dialogBox.TypeQuiz($"{move.Base.Name}");
        yield return new WaitForSeconds(1f);

        //open player answer menu
        dialogBox.EnableDialogText(false);
        dialogBox.SetDialog("");
        dialogBox.EnableAnswerSelector(true);
        state = BattleState.PlayerAnswer;

    }

    public void HandleUpdate() {
        if(state ==  BattleState.PlayerAction) {
            HandleActionSelection();
        }
        else if(state == BattleState.PlayerMove) {
            HandleMoveSelection();
        }
        else if(state == BattleState.PlayerAnswer) {
            HandleAnswerSelection();
        }

    }

    void HandleActionSelection() {
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if(currentAction < 1) {
                ++currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if(currentAction > 0) {
                --currentAction;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);

        if(Input.GetKeyDown(KeyCode.Z)) {
            if (currentAction == 0) {
                PlayerMove();
            }
            // else if (currentAction == 1) {
            //
            // }
            // else if (currentAction == 2) {

            // }
            else if (currentAction == 1) {
				GameController.Instance.EndBattle(false);
            }
        }
    }

    // void HandleMoveSelection() {
    //     if (Input.GetKeyDown(KeyCode.RightArrow)) {
    //         if(currentMove < playerUnit.gubbe.Moves.Count - 1) {
    //             ++currentMove;
    //
    //         }
    //     }
    //     else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
    //         if(currentMove > 0) {
    //             --currentMove;
    //
    //         }
    //     }
    //     else if (Input.GetKeyDown(KeyCode.DownArrow)) {
    //         if(currentMove < playerUnit.gubbe.Moves.Count - 2) {
    //             currentMove += 2;
    //
    //         }
    //     }
    //     else if (Input.GetKeyDown(KeyCode.UpArrow)) {
    //         if(currentMove > 1) {
    //             currentMove -= 2;
    //
    //         }
    //     }

    void HandleMoveSelection() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if(currentMove < 4 - 1) {
                ++currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if(currentMove > 0) {
                --currentMove;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if(currentMove < 4 - 2) {
                currentMove += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if(currentMove > 1) {
                currentMove -= 2;
            }
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.gubbe.Moves[currentMove]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);

            StartCoroutine(PerformPlayerMove());
        }

    }

    // void HandleAnswerSelection() {
    //     if (Input.GetKeyDown(KeyCode.RightArrow)) {
    //         if(currentAnswer < enemyUnit.gubbe.Answers.Count - 1) {
    //             ++currentAnswer;
    //         }
    //     }
    //     else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
    //         if(currentAnswer > 0) {
    //             --currentAnswer;
    //         }
    //     }
    //     else if (Input.GetKeyDown(KeyCode.DownArrow)) {
    //         if(currentAnswer < enemyUnit.gubbe.Answers.Count - 2) {
    //             currentAnswer += 2;
    //         }
    //     }
    //     else if (Input.GetKeyDown(KeyCode.UpArrow)) {
    //         if(currentAnswer > 1) {
    //             currentAnswer -= 2;
    //         }
    //     }

    void HandleAnswerSelection() {
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if(currentAnswer < 4 - 1) {
                ++currentAnswer;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            if(currentAnswer > 0) {
                --currentAnswer;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            if(currentAnswer < 4 - 2) {
                currentAnswer += 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            if(currentAnswer > 1) {
                currentAnswer -= 2;
            }
        }

        dialogBox.UpdateAnswerSelection(currentAnswer, enemyUnit.gubbe.Answers[currentAnswer]);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableAnswerSelector(false);

            StartCoroutine(PerformPlayerAnswer());
        }

    }

}
