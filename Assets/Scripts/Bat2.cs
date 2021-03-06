using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bat2 : MonoBehaviour
{
    public SoundManager soundmanager;

    #region references d'objets/script
    public maingame maingame;
    public Bat1 bat1;
    public Transform Bat2_Pos;
    public GameObject Visual;
    public Ressources_and_people R_and_P;
    public ScriptArrowTuto arrowtuto;
    public Dialogue_Box _Dialog_box;
    public GameObject carrierreniv3_bloc_upgrade;
    public GameObject people_bloc_upgrade;
    public TextMeshProUGUI people_bloc_text;
    public GameObject MaxBloc;
    public GameObject PrefabKayou;
    #endregion

    public int level = 0; //niveau du bat
    public int value_to_upgrade = 0; //valeur pour ameliorer
    public int people_need_to_upgrade = 0;

    [SerializeField] List<Bat1_Upgrades> Bat2_Upgrades; //usage de la classe Bat1_Upgrade pour modif l'affichage/les vlaeurs

    public TextMeshProUGUI bat2_price_txt; //prix affich? sur bouton
    public TextMeshProUGUI bat2_name; //nom affich? ingame
    public TextMeshProUGUI bat2_update_description; //description affich? sur bouton
    public Image bat2_update_sprite; //image affich? ingame

    #region upgrade
    public GameObject UpgradeBar;
    public TextMeshProUGUI UpgradeBar_Text;
    public bool isUpgrading = false;
    public int upgrade_count_number, upgrade_count_total;
    public GameObject clic_bloc;
    public Image filler;
    float lerp = 0f, duration = 0.2f;
    float percent = 0f;
    float fill_before = 0f, fill_goal = 0f;
    public GameObject PrefabUpgradeEffect;
    public MaisonetteScript maisonette;
    #endregion

    bool dialog_done = false;

    //fire
    public bool is_on_fire = false;

    private void Start()
    {
        Bat2Update(Bat2_Upgrades[level]); //reference le bat au start avec son niveau 0
        MaxBloc.SetActive(false);
    }

    void Update()
    {
        if (bat1.level == 3)
        {
            carrierreniv3_bloc_upgrade.SetActive(false);
            if (R_and_P.brick_number >= 250 && dialog_done == false)
            {
                _Dialog_box.ActivateBox();
                _Dialog_box.dialog_number = 7;
                _Dialog_box.DialogUpdateCall();
                dialog_done = true;
                StartCoroutine(_Dialog_box.CloseAfterTimer(4f));
            }
        }
        maingame.CheckCanBuy(bat2_price_txt, R_and_P.brick_number, value_to_upgrade); //v?rifier si achetable pour afficher texte couleur
        //bloqueur si pas assez de habitants pour am?liorer
        if (R_and_P.people_shown < people_need_to_upgrade)
        {
            people_bloc_upgrade.SetActive(true);
        }
        if (R_and_P.people_shown > people_need_to_upgrade)
        {
            people_bloc_upgrade.SetActive(false);
        }

        //filler upgrade dynamique
        lerp += Time.deltaTime / duration;
        percent = (float)Mathf.Lerp((float)fill_before, (float)fill_goal, lerp);
        filler.fillAmount = percent; //filler de la bar

        //fire
        if (is_on_fire == true)
        {
            Visual.GetComponent<Image>().color = new Color32(255, 131, 33, 255);
        }
    }

    public void MajUpgradeClic()
    {
        UpgradeBar_Text.text = $"{upgrade_count_number} / {upgrade_count_total}";    //text de la barre
    }
    public void UpgradeNextLevel() //passe au niveau suivant
    {
        if (R_and_P.brick_number >= value_to_upgrade)
        {
            soundmanager.ClicAchats();

            R_and_P.brick_number -= value_to_upgrade; //enleve le prix au nb de brick
            //met en mode upgrade
            isUpgrading = true;    //bool qui dit qu'on est en mode upgrade
            UpgradeBar.SetActive(true);    //affichage de la barre
            UpgradeBar.transform.DOScale(1f, 0.2f);
            upgrade_count_number = 0; //reset le nombre de clic
            percent = 0f;
            fill_goal = 0f;
            fill_before = 0f;
            if (level == 0) //si niveau 0 = met le nb de base
            {
                upgrade_count_total = 10;//met le nombre de clic max
            }
            else
            {
                upgrade_count_total = (int)(upgrade_count_total * 1.2f); //met le nombre de clic max
            }
            MajUpgradeClic();
            clic_bloc.SetActive(true);
        }
    }

    public void UpgradeDone()
    {
        soundmanager.SoundUpgrade();
        //shake
        StartCoroutine(maingame.camerashake.Shake(.2f, .1f));
        clic_bloc.SetActive(false);
        level++; //augmente le niveau
        Bat2Update(Bat2_Upgrades[level]); //recupere les valeurs de l'update
        #region action selon le niveau
        if (level == 1) //upgrade du niv1
        {
            //caillou explose
            ShowKayou(Bat2_Pos);
            R_and_P.people_augmentation += 1;
        }
        if (level >= 2)
        {
            ShowKayou(Bat2_Pos);
            R_and_P.people_augmentation += 1;
            maisonette.MaisonetteNext();
        }
        #endregion
    }

    public void Hit(Transform Hit_Pos)
    {
        //si pas en mode upgrade
        if (isUpgrading == false && is_on_fire == false)
        {
            if (level > 0) //si carri?re d?bloqu?e
            {
                Visual.transform.DOComplete(); //complete l'animation pr?c?dente pour ?viter bug
                Visual.transform.DOPunchScale(new Vector3(0.002f, 0.002f, 0), 0.3f); //animation de hit sur le bat
            }
            if (level == 0) //si la carri?re n'est pas d?bloqu?e
            {
                //animation r?duite de hit sur le bat  
                Visual.transform.DOComplete();
                Visual.transform.DOPunchScale(new Vector3(0.001f, 0.001f, 0), 0.5f);
            }
        }
        else if (isUpgrading == true)
        {
            //shake
            StartCoroutine(maingame.camerashake.Shake(.1f, .05f));
            maingame.ShowStar(Bat2_Pos, 2f, 2f);
            //upgrade
            lerp = 0;
            fill_before = (float)upgrade_count_number / (float)upgrade_count_total;
            upgrade_count_number++;
            fill_goal = (float)upgrade_count_number / (float)upgrade_count_total;
            percent = fill_before;
            MajUpgradeClic();
            //anim
            GameObject go = GameObject.Instantiate(PrefabUpgradeEffect, Hit_Pos, false); //genere l'effet
            go.transform.position = new Vector2(go.transform.position.x + 0.15f, go.transform.position.y +0.2f);
            Visual.transform.DOComplete(); //complete l'animation pr?c?dente pour ?viter bug
            Visual.transform.DOPunchScale(new Vector3(0.004f, 0.004f, 0), 0.3f); //animation de hit sur le bat
            //upgrade se fait si assez de clic
            if (upgrade_count_number >= upgrade_count_total)
            {
                //upgrade done
                isUpgrading = false;
                UpgradeBar.SetActive(false);
                UpgradeBar.transform.DOScale(0f, 0.2f);
                UpgradeDone();
            }
        }
    }

    public void Bat2Update(Bat1_Upgrades _bat2upgrade) //modifier visuelement le bat
    { 
        Visual.GetComponent<Image>().sprite = _bat2upgrade.Sprite; //change le sprite du bat
        if (level > 1)
        {
            value_to_upgrade = (int)(value_to_upgrade * 1.2); //change le prix
        }
        else if (level == 1)
        {
            value_to_upgrade = 20;
        }
        bat2_price_txt.text = (value_to_upgrade).ToString(); //change le prix
        bat2_name.text = _bat2upgrade.Name; //change le nom
        bat2_update_description.text = _bat2upgrade.Description; //change la description de l'update
        bat2_update_sprite.sprite = _bat2upgrade.SpriteUpdate; //change le sprite de l'icone dans l'update
        people_need_to_upgrade = _bat2upgrade.PeopleNeed; //remet la variable du nombre de people dont on a besoin a jour
        people_bloc_text.text = people_need_to_upgrade.ToString() + " habitants requis";
        if (Bat2_Upgrades.Count <= level +1)
        {
            MaxBloc.SetActive(true);
        }
    }

    public void ShowKayou(Transform Hit_Pos) //anim du clic 
    {
        //si il n'y a pas le feu
        if (is_on_fire == false)
        {
            //affiche l'animation
            for (int i = 0; i < 5; i++)
            {;
                GameObject go = GameObject.Instantiate(PrefabKayou, Hit_Pos, false); //genere la brique qui sort
                //transform et anim de la pierre
                go.transform.DOScale(0, 0f);
                go.transform.DOComplete();
                go.transform.DOScale(0.75f, 0.3f);
                go.transform.localPosition = new Vector3(Hit_Pos.localPosition.x + 3f, Hit_Pos.localPosition.y + 2f, 1);
                go.transform.DOLocalJump(new Vector3(Hit_Pos.localPosition.x + (i - 2), Hit_Pos.localPosition.y - Random.Range(1f,2f), 1), Random.Range(2f, 4f), 1, 0.9f);
                GameObject.Destroy(go, 0.8f);
            }

        }
    }

    public void FireStart()
    {
        is_on_fire = true;
        Visual.GetComponent<Image>().color = new Color(244, 102, 27);
    }

    public void FireEnd()
    {
        is_on_fire = false;
        maingame.FireEnd();
        Visual.GetComponent<Image>().color = new Color(255, 255, 255);
    }

}
