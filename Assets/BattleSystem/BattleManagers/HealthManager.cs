using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public BSstatemanager stateManager;

    private float newPlayerHPfill;
    private float newAIHPfill;

    float lerpSpeed = 5f;

    public Image playerHPBar;
    public Text playerHPText;

    public Image aiHPBar;
    public Text aiHPText;

    public int playerCurHP;
    public int aiCurHP;


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

    void Start()
    {
 
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
        //SetUpHealthUI();
        SetUpMonHealth();
        SetUpHealthUI();
    }

    /// <summary>
    /// Health UI Setup on initialize
    /// </summary>
    void SetUpHealthUI()
    {
        newPlayerHPfill = (float) playerCurHP/ stateManager.mon1maxHP;
        playerHPText.text = $"Health: {playerCurHP} / {stateManager.mon1maxHP}";

        newAIHPfill = (float) aiCurHP / stateManager.mon2maxHP;
        aiHPText.text = $"Health: {aiCurHP} / {stateManager.mon2maxHP}";
    }

    /// <summary>
    /// Sets current health on Initialize
    /// </summary>
    void SetUpMonHealth()
    {
        playerCurHP = stateManager.mon1maxHP;
        aiCurHP = stateManager.mon2maxHP;
    }


    /// <summary>
    /// Changes health UI when Damage is received
    /// </summary>
    /// <param name="healthValue"></param>
    private void DamageReceived(int healthValue)
    {
        UpdateFill();
        UpdateHealthText();
    }

    /// <summary>
    /// Changes health UI when Healing is received
    /// </summary>
    /// <param name="healthValue"></param>
    private void HealingReceived(int healthValue)
    {
        UpdateFill();
        UpdateHealthText();
    }


    /// <summary>
    /// Updates the health values for healthbar fill
    /// </summary>
    private void UpdateFill()
    {
        //newPlayerHPfill = (float)stateManager.mon1curHP / stateManager.mon1maxHP;
        //newAIHPfill = (float)stateManager.mon2curHP / stateManager.mon2maxHP;

        newPlayerHPfill = (float) playerCurHP / stateManager.mon1maxHP;
        newAIHPfill = (float) aiCurHP / stateManager.mon2maxHP;
    }

    /// <summary>
    /// Updates health values in text
    /// </summary>
    private void UpdateHealthText()
    {
        //playerHPText.text = $"Health: {stateManager.mon1curHP} / {stateManager.mon1maxHP}";
        //aiHPText.text = $"Health: {stateManager.mon2curHP} / {stateManager.mon2maxHP}";
        playerHPText.text = $"Health: {playerCurHP} / {stateManager.mon1maxHP}";
        aiHPText.text = $"Health: {aiCurHP} / {stateManager.mon2maxHP}";
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
}
