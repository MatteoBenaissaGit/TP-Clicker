using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bat1 : MonoBehaviour
{
    public maingame maingame;
    public Transform Bat1_Pos;

    public GameObject Visual;
    public Ressources_and_people R_and_P;
    public int level = 0;
    public int value_to_upgrade = 0;

    [SerializeField] List<Bat1_Upgrades> Bat1_Upgrades;

    public TextMeshProUGUI bat1_price_txt;
    public TextMeshProUGUI bat1_name;
    public TextMeshProUGUI bat1_update_description;
    public Image bat1_update_sprite;

    public int Bat1_clicdamage = 1;

    private void Start()
    {
        Bat1Update(Bat1_Upgrades[level]);
    }

    //upgrading
    public void UpgradeNextLevel()
    {
        if (R_and_P.brick_number >= value_to_upgrade)
        {
            R_and_P.brick_number -= value_to_upgrade;
            level++;
            Bat1Update(Bat1_Upgrades[level]);
            if (level == 2) //upgrade du niv2
            {
                R_and_P.people_augmentation += 1;
            }
            if (level == 3) //upgrade du niv3
            {
                Bat1_clicdamage += 1;
            }
        }
    }

    public void Hit()
    {
        //showdamage
        if (level > 0)
        {
            maingame.ShowDamage(Bat1_Pos, Bat1_clicdamage);
        }
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
            R_and_P.AddBrick(Bat1_clicdamage);
        }
    }

    public void Bat1Update(Bat1_Upgrades _bat1upgrade)
    {
        //change le sprite du bat
        Visual.GetComponent<SpriteRenderer>().sprite = _bat1upgrade.Sprite;
        //change le prix
        bat1_price_txt.text = _bat1upgrade.Price.ToString();
        value_to_upgrade = _bat1upgrade.Price;
        //change le nom
        bat1_name.text = _bat1upgrade.Name;
        //change la description de l'update
        bat1_update_description.text = _bat1upgrade.Description;
        //change le sprite de l'icone dans l'update
        bat1_update_sprite.sprite = _bat1upgrade.SpriteUpdate;
    }

    public void AddOuvrier() //ajoute un ouvrier
    {
        R_and_P.brick_augmentation += 1;
        //ajoute graphiquement un ouvrier
    }

}
