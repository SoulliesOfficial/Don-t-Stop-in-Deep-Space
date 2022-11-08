using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubspaceDisruptionSystem : MonoBehaviour
{
    public float subspaceDisruptionValue, subspaceDisruptionTargetValue;
    public float initialValue;
    public SubspaceDisruptionValueParts subspaceDisruptionValueParts;
    void Start()
    {
        initialValue = 20;
        subspaceDisruptionValueParts = new SubspaceDisruptionValueParts(0, 0, 0, 0);
        subspaceDisruptionValue = 20;
    }

    void Update()
    {
        CalculateSubspaceDisruptionValue();
        subspaceDisruptionValueParts.Decay(2 * Time.deltaTime,2);

        if(subspaceDisruptionValue <= 0)
        {
            GameManager.uiManager.LOSE();
        }
    }

    public void CalculateSubspaceDisruptionValue()
    {
        subspaceDisruptionTargetValue = initialValue + subspaceDisruptionValueParts.CalculateTargetValue();
        subspaceDisruptionValue = Mathf.Lerp(subspaceDisruptionValue, subspaceDisruptionTargetValue, 5 * Time.deltaTime);
        GameManager.uiManager.subspaceDisruptionValueText.color = new Color(1, 1 - (subspaceDisruptionValue / 50), 1 - (subspaceDisruptionValue / 50));
        GameManager.uiManager.subspaceDisruptionValueText.text = subspaceDisruptionValue.ToString("F1");

    }
}

[System.Serializable]
public class SubspaceDisruptionValueParts
{
    public float playerMovement;
    public float playerAttackIntensity;
    public float playerFunction;
    public float enemyIntensity;

    public float subspaceDisruptionTargetValue;

    public SubspaceDisruptionValueParts(float playerMovement, float playerAttackIntensity, float playerFunction, float enemyIntensity)
    {
        this.playerMovement = playerMovement;
        this.playerAttackIntensity = playerAttackIntensity;
        this.playerFunction = playerFunction;
        this.enemyIntensity = enemyIntensity;
    }

    public float CalculateTargetValue()
    {
        return this.playerMovement + this.playerAttackIntensity + this.playerFunction + this.enemyIntensity;
    }

    public void Decay(float decayValue, float offset)
    {
        this.playerMovement -= decayValue;
        this.playerAttackIntensity -= (decayValue * Mathf.Pow(this.playerAttackIntensity, 1/offset));
        this.playerFunction -= decayValue;
        this.enemyIntensity -= decayValue;

        Func<float, float> bottom = (arg) => arg < 0 ? 0 : arg;

        this.playerMovement = bottom(this.playerMovement);
        this.playerAttackIntensity = bottom(this.playerAttackIntensity);
        this.playerFunction = bottom(this.playerFunction);
        this.enemyIntensity = bottom(this.enemyIntensity);
    }
}