using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text subspaceDisruptionValueText;

    public Image life, energy, disruption, weaponImage;

    public List<Sprite> weaponIconList;

    public ShipUI shipUI;

    public void Update()
    {
        life.fillAmount = (GameManager.player.life / 100f);
        energy.fillAmount = (GameManager.player.energy / 100f);
        disruption.fillAmount = (GameManager.subspaceDisruptionSystem.subspaceDisruptionTargetValue / 100f);
    }
}
