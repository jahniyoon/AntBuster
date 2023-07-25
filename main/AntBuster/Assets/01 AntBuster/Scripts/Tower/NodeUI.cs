using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeUI : MonoBehaviour
{
    public GameObject ui;

    public TMP_Text upgradeCost;
    public Button upgradeButton;

    public TMP_Text sellAmount;

    private Node target;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();

        if (!target.isUpgraded)
        {
            upgradeCost.text = "$" + GameInfo.towerUpgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        sellAmount.text = "$" + (GameInfo.towerCost / 2);

        ui.SetActive(true);
    }

    public void Hide()
    {
        Audio audio = FindObjectOfType<Audio>();
        audio.ClickSound();
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        Audio audio = FindObjectOfType<Audio>();
        audio.ClickSound();
        target.UpgradeTower();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        Audio audio = FindObjectOfType<Audio>();
        audio.ClickSound();
        target.SellTower();
        BuildManager.instance.DeselectNode();
    }
}
