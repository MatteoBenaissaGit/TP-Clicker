using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bat4 : MonoBehaviour
{
    #region references d'objets/script
    public maingame maingame;
    public Bat1 bat1;
    public Transform Bat4_Pos;
    public GameObject Visual;
    public Ressources_and_people R_and_P;
    public ScriptArrowTuto arrowtuto;
    public Dialogue_Box _Dialog_box;
    public GameObject people_bloc_upgrade;
    public TextMeshProUGUI people_bloc_text;
    public TextMeshProUGUI number_employee_text;
    public GameObject mais10pp200bloc;
    public GameObject MaxBloc;
    public GameObject PrefabKayou;
    #endregion

    public int level = 0; //niveau du bat
    public int value_to_upgrade = 0; //valeur pour ameliorer
    public int people_need_to_upgrade = 0;
    public int number_of_employee = 0;

    [SerializeField] List<Bat1_Upgrades> Bat4_Upgrades; //usage de la classe Bat1_Upgrade pour modif l'affichage/les vlaeurs

    public TextMeshProUGUI bat4_price_txt; //prix affiché sur bouton
    public TextMeshProUGUI bat4_name; //nom affiché ingame
    public TextMeshProUGUI bat4_update_description; //description affiché sur bouton
    public Image bat4_update_sprite; //image affiché ingame

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
    #endregion

    bool dialog_done = false;

    public Bat3 bat3;

    //fire
    public bool is_on_fire = false;

    private void Start()
    {
        Bat4Update(Bat4_Upgrades[level]); //reference le bat au start avec son niveau 0
        MaxBloc.SetActive(false);
    }

    void Update()
    {
        //tuto launch
        if (R_and_P.people_number >= 200 && bat3.level >= 7 && dialog_done == false)
        {
            _Dialog_box.ActivateBox();
            _Dialog_box.dialog_number = 9;
            _Dialog_box.DialogUpdateCall();
            dialog_done = true;
            StartCoroutine(_Dialog_box.CloseAfterTimer(5f));
        }

        maingame.CheckCanBuy(bat4_price_txt, R_and_P.brick_number, value_to_upgrade); //vérifier si achetable pour afficher texte couleur

        //bloqueur 10ouvrier
        if (bat3.level >= 8)
        {
            mais10pp200bloc.SetActive(false);
        }
        //bloqueur si pas assez de habitants pour améliorer
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
        Debug.Log("NIGGA");
        if (R_and_P.brick_number >= value_to_upgrade)
        {
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
        clic_bloc.SetActive(false);
        level++; //augmente le niveau
        Bat4Update(Bat4_Upgrades[level]); //recupere les valeurs de l'update
        #region action selon le niveau
        if (level == 1) //upgrade du niv1
        {
            StartCoroutine(_Dialog_box.CloseAfterTimer(4f)); //dialogbox se ferme
            //caillou explose
            ShowKayou(Bat4_Pos);
            R_and_P.people_augmentation += 1;
        }
        if (level >= 2)
        {
            ShowKayou(Bat4_Pos);
            bat1.Bat1_clicdamage += 2;
        }
        #endregion
    }

    public void Hit(Transform Hit_Pos)
    {
        //si pas en mode upgrade
        if (isUpgrading == false && is_on_fire == false)
        {
            if (level > 0) //si carrière débloquée
            {
                Visual.transform.DOComplete(); //complete l'animation précédente pour éviter bug
                Visual.transform.DOPunchScale(new Vector3(0.002f, 0.002f, 0), 0.3f); //animation de hit sur le bat
            }
            if (level == 0) //si la carrière n'est pas débloquée
            {
                //animation réduite de hit sur le bat  
                Visual.transform.DOComplete();
                Visual.transform.DOPunchScale(new Vector3(0.001f, 0.001f, 0), 0.5f);
            }
        }
        else if (isUpgrading == true)
        {
            maingame.ShowStar(Bat4_Pos);
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
            Visual.transform.DOComplete(); //complete l'animation précédente pour éviter bug
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

    public void Bat4Update(Bat1_Upgrades _bat2upgrade) //modifier visuelement le bat
    { 
        Visual.GetComponent<Image>().sprite = _bat2upgrade.Sprite; //change le sprite du bat
        if (level > 1)
        {
            value_to_upgrade = (int)(value_to_upgrade * 1.13); //change le prix
        }
        else if (level == 1)
        {
            value_to_upgrade = 250;
        }
        bat4_price_txt.text = (value_to_upgrade).ToString(); //change le prix
        bat4_name.text = _bat2upgrade.Name; //change le nom
        bat4_update_description.text = _bat2upgrade.Description; //change la description de l'update
        bat4_update_sprite.sprite = _bat2upgrade.SpriteUpdate; //change le sprite de l'icone dans l'update
        people_need_to_upgrade = _bat2upgrade.PeopleNeed; //remet la variable du nombre de people dont on a besoin a jour
        people_bloc_text.text = people_need_to_upgrade.ToString() + " habitants requis";
        if (Bat4_Upgrades.Count <= level +1)
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
                go.transform.localPosition = new Vector3(Hit_Pos.localPosition.x + 7f, Hit_Pos.localPosition.y - 2.5f, 1);
                go.transform.DOLocalJump(new Vector3(Hit_Pos.localPosition.x + 7f + (i - 2), Hit_Pos.localPosition.y - Random.Range(1f,2f) - 2.5f, 1), Random.Range(2f, 4f), 1, 0.9f);
                GameObject.Destroy(go, 0.8f);
            }

        }
    }

    public void AddMacon()
    {
        R_and_P.brick_augmentation = (int)(R_and_P.brick_augmentation * 1.5);
        number_of_employee++;
        number_employee_text.text = number_of_employee.ToString();
    }

}
