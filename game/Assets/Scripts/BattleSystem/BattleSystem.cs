using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, RUN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject Rifle;
    private GameObject player;
    private GameObject enemy;

    public BattleState state;

    Unit enemyUnit;
    EnemyBar enemyHP;

    public BattleHUD battleHUD;
    private Animator playerAnim;

    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(SetupBattle()); // Wrap function to start a co-routine
    }

    IEnumerator SetupBattle() // IEnumerator lets us use the WaitForSeconds function
    {
        // Setting up the battle\
        battleHUD.buttonVisibility(false);
        player = Instantiate(playerPrefab);
        playerAnim = player.GetComponent<Animator>();

        // Get animation controller
        if (PlayerStats.HasRifle)
        {
            Instantiate(Rifle);
            PlayerActions.HoldRifle();
            playerAnim.runtimeAnimatorController = anim2 as RuntimeAnimatorController;
        }
        else
        {
            playerAnim.runtimeAnimatorController = anim1 as RuntimeAnimatorController;
        }

        enemy = Instantiate(enemyPrefab);
        enemyUnit = enemy.GetComponent<Unit>();

        string text;

        if (PlayerStats.HasAdvantage)
        {
            text = "You took " + enemyUnit.unitName + " by surprised!";
            enemyUnit.currentHP -= 10;
        }
        else 
        {
            text = enemyUnit.unitName + " has engaged in battle!";
        }

        enemyHP = enemy.GetComponent<EnemyBar>(); // Set enemy health bar
        enemyHP.setEnemy(enemyUnit);
        battleHUD.SetHUD(text);

        //Start the battle
        yield return new WaitForSeconds(2f); // Seconds to pause before action
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        playerAnim.SetTrigger("Idle");
        battleHUD.dialogueText.text = "Choose an action";
        battleHUD.buttonVisibility(true);
    }

    IEnumerator EnemyTurn()
    {
        bool isDead;

        // Enemy Attack
        enemyUnit.AttackRoutine();
        battleHUD.dialogueText.text = enemyUnit.message;

        isDead = PlayerActions.TakeDamage(enemyUnit.dmg);
        battleHUD.setHP();
        enemyUnit.GoIdle();
        if (PlayerStats.Blocking)
        {
            PlayerStats.Blocking = false;
        }

        yield return new WaitForSeconds(2f);
        

        if (isDead)
        {
            state = BattleState.LOST;
            StartCoroutine(EndBattle());
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    IEnumerator EndBattle()
    {
        PlayerStats.HasAdvantage = false;

        if (enemy.CompareTag("Boss"))
        {
            if (state == BattleState.WON)
            {
                enemyUnit.Dead();
                battleHUD.dialogueText.text = "Congratulations! You've beaten the demo";
                yield return new WaitForSeconds(2f);
                battleHUD.dialogueText.text = "Returning to menu!";
                yield return new WaitForSeconds(2f);
                PlayerActions.getExp(15);
                SceneManager.LoadScene("MainMenu");
            }
            else if (state == BattleState.LOST)
            {
                playerAnim.SetTrigger("Death");
                battleHUD.dialogueText.text = "You were defeated!";

                yield return new WaitForSeconds(2f);
                GameController.clearList();
                PlayerActions.Reset();
                SceneManager.LoadScene("MainMenu");
            }
        }

         if (state == BattleState.WON)
        {
            enemyUnit.Dead();
            battleHUD.dialogueText.text = "Battle Won!";

            yield return new WaitForSeconds(2f);
            PlayerActions.getExp(15);
            SceneManager.LoadScene("TutorialLevel");
        }
        else if (state == BattleState.LOST)
        {
            playerAnim.SetTrigger("Death");
            battleHUD.dialogueText.text = "You were defeated!";

            yield return new WaitForSeconds(2f);
            GameController.clearList();
            PlayerActions.Reset();
            SceneManager.LoadScene("MainMenu");
        }
        else if (state == BattleState.RUN)
        {
            battleHUD.dialogueText.text = "You ran away!";

            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("TutorialLevel");
        }
    }

    IEnumerator PlayerAttack()
    {
        // Damage the enemy
        playerAnim.SetTrigger("Attack");
        bool isDead = enemyUnit.TakeDamage(PlayerStats.Power); // Check if attack kills enemy
        enemyHP.setHP(enemyUnit.currentHP);
        battleHUD.dialogueText.text = "Your attack did " + PlayerStats.Power + " damage!";

        yield return new WaitForSeconds(2f);

        // Check if the enemy is dead
        if (isDead)
        {
            state = BattleState.WON;
            StartCoroutine(EndBattle());
        } 
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        // Change state based on what happened
    }

    IEnumerator PlayerHeal()
    {
        bool cast = PlayerActions.Heal(5);

        if (cast)
        {
            battleHUD.setHP();
            battleHUD.setMP();
            battleHUD.dialogueText.text = "You healed by 5 points";
            battleHUD.buttonVisibility(false);

            yield return new WaitForSeconds(2f);

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {
            battleHUD.dialogueText.text = "Not Enough mana!";
            yield return new WaitForSeconds(1f);

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    IEnumerator PlayerBlock()
    {
        PlayerStats.Blocking = true;
        battleHUD.dialogueText.text = "Bracing for impact";
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator PlayerRun()
    {
        battleHUD.dialogueText.text = "You're trying to escape";
        yield return new WaitForSeconds(2f);

        if (enemy.CompareTag("Boss"))
        {
            battleHUD.dialogueText.text = "You can't run away!";
            yield return new WaitForSeconds(2f);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

        else
        {
            state = BattleState.RUN;
            StartCoroutine(EndBattle());
        }
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack());
        battleHUD.buttonVisibility(false);
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerHeal());
    }

    public void OnBlockButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerBlock());
        battleHUD.buttonVisibility(false);
    }

    public void OnRunButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerRun());
        battleHUD.buttonVisibility(false);
    }
}