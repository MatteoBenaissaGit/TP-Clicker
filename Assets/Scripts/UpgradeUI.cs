using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    public Upgrade _upgrade;
    public Image Image;
    public Text Text;
    public Text TextCost;

    public void Initialize(Upgrade upgrade)
    {
        //met les attributs aux upgrades
        _upgrade = upgrade;
        Image.sprite = upgrade.Sprite;
        Text.text = upgrade.Name + System.Environment.NewLine + upgrade.Description;
        TextCost.text = upgrade.Cost + "$";
    }

    public void OnClick()
    {
        maingame.Instance.AddUpgrade(_upgrade);
    }

}
