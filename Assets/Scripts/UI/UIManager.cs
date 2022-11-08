using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text subspaceDisruptionValueText;

    public Image disruption, weaponImage;

    public List<Sprite> weaponIconList;

    public ShipUI shipUI;

    public CanvasGroup loseUI;
    public TMP_Text finText;
    public float loseUIOpacity;
    public bool showLose;

    public void Update()
    {
        disruption.fillAmount = (GameManager.subspaceDisruptionSystem.subspaceDisruptionTargetValue / 100f);

        if (showLose)
        {
            loseUIOpacity += 2 * Time.deltaTime;
            loseUIOpacity = Mathf.Min(loseUIOpacity, 1);
            loseUI.alpha = loseUIOpacity;
        }
    }

    public void LOSE()
    {
        GameManager.player.gameObject.SetActive(false);
        showLose = true;
        loseUI.gameObject.SetActive(true);
    }

    public void WIN()
    {
        GameManager.player.gameObject.SetActive(false);
        finText.text = "You have Destroyed the Imperial Fortress... \nYou will embark on another journey in the Deep Space.";
        showLose = true;
        loseUI.gameObject.SetActive(true);
        AudioListener.pause = true;
    }

}
