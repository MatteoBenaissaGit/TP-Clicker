using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bat1 : MonoBehaviour
{
 
    public GameObject Visual;
    public Ressources_and_people R_and_P;
    public int level = 0;
    public int value_to_upgrade = 0;

    [SerializeField] List<Bat1_Upgrades> Bat1_Upgrades;

    public TextMeshProUGUI bat1_price_txt;
    public TextMeshProUGUI bat1_name;

    private void Start()
    {
        Bat1Update(Bat1_Upgrades[level]);
    }

    //upgrading
    public void UpgradeNextLevel()
    {
        Debug.Log("UPGRADE NEXT LEVEL CLIC");
        if (R_and_P.brick_number >= value_to_upgrade)
        {
            R_and_P.brick_number -= value_to_upgrade;
            Debug.Log("UPGRADE NEXT LEVEL");
            level++;
            Bat1Update(Bat1_Upgrades[level]);
        }
    }

    public void Hit(int value)
    {
        //si la carrière n'est pas débloquée
        if (level == 0)
        {
            Visual.transform.DOComplete();
            Visual.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.5f);
        }
        //si elle est débloquée
        if (level > 0)
        {
            //animation de hit sur le bat
            Visual.transform.DOComplete();
            Visual.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f);
            R_and_P.AddBrick(value);
        }
    }

    public void Bat1Update(Bat1_Upgrades _bat1upgrade)
    {
        Debug.Log("UPDATE");
        //change le sprite du bat
        Visual.GetComponent<SpriteRenderer>().sprite = _bat1upgrade.Sprite;
        //change le prix
        bat1_price_txt.text = _bat1upgrade.Price.ToString();
        //change le nom
        bat1_name.text = _bat1upgrade.Name;
    }

}
