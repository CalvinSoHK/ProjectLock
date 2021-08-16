using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mon.MonData;
public class MonUIManager : MonoBehaviour
{
    public BSstatemanager stateManager;

    private float newPlayerHPfill;
    private float newAIHPfill;

    float lerpSpeed = 5f;

    public Image playerHPBar;
    public Text playerHPText;
    public Text playerMonName;

    public Image aiHPBar;
    public Text aiHPText;
    public Text aiMonName;

    private void OnEnable()
    {

        DamageManager.damageEvent += DamageReceived;
        ItemManager.healEvent += HealingReceived;
    }

    private void OnDisable()
    {

        DamageManager.damageEvent -= DamageReceived;
        ItemManager.healEvent -= HealingReceived;
    }


    void Update()
    {
        UpdateFillLerp();
    }


    /// <summary>
    /// SetsUp Health Bar UIs
    /// Used in BSinitialize
    /// </summary>
    public void SetUp()
    {
        SetUpHealthUI();
        SetUpNameUI();
        SetUpMoveText();
    }

    /// <summary>
    /// Health UI Setup on initialize
    /// </summary>
    void SetUpHealthUI()
    {
        newPlayerHPfill = (float)stateManager.healthManager.playerCurHP / stateManager.healthManager.playerMaxHP;
        playerHPText.text = $"Health: {stateManager.healthManager.playerCurHP} / {stateManager.healthManager.playerMaxHP}";

        newAIHPfill = (float) stateManager.healthManager.aiCurHP / stateManager.healthManager.aiMaxHP;
        aiHPText.text = $"Health: {stateManager.healthManager.aiCurHP} / {stateManager.healthManager.aiMaxHP}";
    }

    /// <summary>
    /// Sets up Mon Names in UI
    /// </summary>
    void SetUpNameUI()
    {
        playerMonName.text = stateManager.playerCurMonster.baseMon.name;
        aiMonName.text = stateManager.aiCurMonster.baseMon.name;
    }


   /// <summary>
   /// Changes health UI when Damage is received
   /// </summary>
   /// <param name="monster"></param>
   /// <param name="healthValue"></param>
    private void DamageReceived(MonIndObj monster, int healthValue)
    {
        UpdateFill(monster, healthValue);
        UpdateHealthText(monster, healthValue);
    }

   /// <summary>
   /// Changes health UI when Healing is received
   /// </summary>
   /// <param name="monster"></param>
   /// <param name="healthValue"></param>
    private void HealingReceived(MonIndObj monster, int healthValue)
    {
        UpdateFill(monster, healthValue);
        UpdateHealthText(monster, healthValue);
    }


    /// <summary>
    /// Updates the health values for healthbar fill
    /// </summary>
    private void UpdateFill(MonIndObj monster, int healthValue)
    {
        if (monster == stateManager.playerCurMonster)
        {
            newPlayerHPfill = (float) healthValue / stateManager.healthManager.playerMaxHP;
        } else if (monster == stateManager.aiCurMonster)
        { 
           newAIHPfill = (float) healthValue / stateManager.healthManager.aiMaxHP;
        }
    }

    /// <summary>
    /// Updates health values in text
    /// </summary>
    private void UpdateHealthText(MonIndObj monster, int healthValue)
    {
        if (monster == stateManager.playerCurMonster)
        {
            playerHPText.text = $"Health: {healthValue} / {stateManager.healthManager.playerMaxHP}";
        } else if (monster == stateManager.aiCurMonster)
        {
            aiHPText.text = $"Health: {healthValue} / {stateManager.healthManager.aiMaxHP}";
        }
    }

    /// <summary>
    /// Lerps the health bar UI
    /// Runs in Update()
    /// </summary>
    private void UpdateFillLerp()
    {
        playerHPBar.fillAmount = Mathf.Lerp(playerHPBar.fillAmount, newPlayerHPfill, Time.deltaTime * lerpSpeed);
        aiHPBar.fillAmount = Mathf.Lerp(aiHPBar.fillAmount, newAIHPfill, Time.deltaTime * lerpSpeed);
    }

    /// <summary>
    /// Sets up player moves text 1-4
    /// </summary>
    private void SetUpMoveText()
    {
        for (int i = 0; i < 4; i++)
        {
            if (stateManager.playerCurMonster.moveSet.GetMove(i) != null)
            {
                stateManager.dialogueText.movesText[i].text = stateManager.playerCurMonster.moveSet.GetMove(i).name;
            }
            else
            {
                stateManager.dialogueText.movesText[i].text = "----";
            }
        }
    }
}
