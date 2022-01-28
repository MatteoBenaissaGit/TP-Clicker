using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Bat1 : MonoBehaviour
{
    #region references d'objets/script
    public maingame maingame;
    public Transform Bat1_Pos;
    public GameObject Visual;
    public Ressources_and_people R_and_P;
    public GameObject PrefabClicBrick;
    public Ouvrier ouvrier;
    public ScriptArrowTuto arrowtuto;
    public Dialogue_Box _Dialog_box;
    public GameObject people_bloc_upgrade;
    public TextMeshProUGUI people_bloc_text;
    public TextMeshProUGUI number_employee_text;
    public GameObject MaxBloc;
    public GameObject PrefabKayou;
    public EtiquetteShower EtiquetteScript;
    #endregion

    public int level = 0; //niveau du bat
    public int value_to_upgrade = 0; //valeur pour ameliorer
    public int people_need_to_upgrade = 0;
    public int number_of_employee = 0;

    [SerializeField] List<Bat1_Upgrades> Bat1_Upgrades; //usage de la classe Bat1_Upgrade pour modif l'affichage/les vlaeurs

    public TextMeshProUGUI bat1_price_txt; //prix affiché sur bouton
    public TextMeshProUGUI bat1_name; //nom affiché ingame
    public TextMeshProUGUI bat1_update_description; //description affiché sur bouton
    public Image bat1_update_sprite; //image affiché ingame

    public int Bat1_clicdamage = 1; //dégats du clic

    bool dialogmaison = false; //pour tuto maison

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
    public GameObject Fire_bloc;
    #endregion

    //fire
    public bool is_on_fire = false;

    private void Start()
    {
        EtiquetteScript.MouseIsOver();
        Bat1Update(Bat1_Upgrades[level]); //reference le bat au start avec son niveau 0
        number_employee_text.text = number_of_employee.ToString();
        MaxBloc.SetActive(false);
    }

    void Update()
    {
        maingame.CheckCanBuy(bat1_price_txt, R_and_P.brick_number, value_to_upgrade); //vérifier si achetable pour afficher texte couleur
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

        //tuto bat2
        if (level == 3 && R_and_P.brick_shown >= 100 && dialogmaison == false)
        {
            //dialog
            _Dialog_box.ActivateBox();
            _Dialog_box.dialog_number = 7;
            _Dialog_box.DialogUpdateCall();
            StartCoroutine(_Dialog_box.CloseAfterTimer(5f)); //dialogbox se ferme
            dialogmaison = true;
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
                upgrade_count_total = Bat1_Upgrades[0].ClicNeed;//met le nombre de clic max
            }
            else
            {
                upgrade_count_total = (int)(upgrade_count_total * 1.2f); //met le nombre de clic max
            }
            MajUpgradeClic();
            clic_bloc.SetActive(true);

            //tuto
            if (level == 0)
            {
                arrowtuto.bat1_buyed = true; //variable pour indiquer a la fleche que le bat est acheter
                arrowtuto.posarrow = 2; //modifie le placage de reference de la fleche
                ouvrier.UnlockOuvrier(); //debloque l'ouvrier
                arrowtuto.CheckPosArrow1(); //change la place de la fleche
                _Dialog_box.dialog_number = 1;
                _Dialog_box.DialogUpdateCall();
                _Dialog_box.BumpBox();
            }
        }
    }

    public void UpgradeDone()
    {
        clic_bloc.SetActive(false);
        level++; //augmente le niveau
        Bat1Update(Bat1_Upgrades[level]); //recupere les valeurs de l'update
        #region action selon le niveau
        if (level == 1) //upgrade du niv1
        {
            //position
            Bat1_Pos.position = new Vector2(Bat1_Pos.position.x, Bat1_Pos.position.y - 0.6f);
            Bat1_Pos.localScale = new Vector2(Bat1_Pos.localScale.x + 0.2f, Bat1_Pos.localScale.y + 0.2f);
            //dialog
            _Dialog_box.dialog_number = 2;
            _Dialog_box.DialogUpdateCall();
            _Dialog_box.BumpBox();
            //caillou explose
            ShowKayou(Bat1_Pos);
        }
        if (level >= 2)
        {
            ShowKayou(Bat1_Pos);
            Bat1_clicdamage += 1;
        }
        #endregion
    }

    public void Hit(Transform Hit_Pos)
    {
        //si pas en mode upgrade
        if (isUpgrading == false && is_on_fire == false)
        {
            //tuto dialog
            if (arrowtuto.bat1_buyed == true && R_and_P.brick_number == 9 && R_and_P.brick_augmentation == 0) //si on a assez pour acheter l'ouvrier
            {
                //dialog
                _Dialog_box.dialog_number++;
                _Dialog_box.DialogUpdateCall();
                _Dialog_box.BumpBox();
                //arrow
                arrowtuto.bat1_used = true; //variable pour indiquer a la fleche que le bat est acheter
                arrowtuto.posarrow = 4; //modifie le placage de reference de la fleche
                arrowtuto.CheckPosArrow1(); //change la place de la fleche    
            }
            //si carrière débloquée
            if (level > 0) 
            {
                Visual.transform.DOComplete(); //complete l'animation précédente pour éviter bug
                Visual.transform.DOPunchScale(new Vector3(0.004f, 0.004f, 0), 0.3f); //animation de hit sur le bat
                R_and_P.AddBrick(Bat1_clicdamage); //valeurs  
                ShowClickBrick(Hit_Pos.transform, Bat1_clicdamage); //animation de clic
                //évite d'avoir 2x une brique par clic
                R_and_P.brick_number_temp += Bat1_clicdamage;
                R_and_P.brick_number_before += Bat1_clicdamage;
            }
            if (level == 0) //si la carrière n'est pas débloquée
            {
                //animation réduite de hit sur le bat  
                Visual.transform.DOComplete();
                Visual.transform.DOPunchScale(new Vector3(0.002f, 0.002f, 0), 0.5f);
            }
        }
        else if (isUpgrading == true)
        {
            //upgrade
            if (level == 0)
                maingame.ShowStar(Bat1_Pos, -0.5f, -2f);
            else
                maingame.ShowStar(Bat1_Pos, -0.5f, 0);
            lerp = 0;
            fill_before = (float)upgrade_count_number / (float)upgrade_count_total;
            upgrade_count_number++;
            fill_goal = (float)upgrade_count_number / (float)upgrade_count_total;
            percent = fill_before;
            MajUpgradeClic();
            //anim
            GameObject go = GameObject.Instantiate(PrefabUpgradeEffect, Hit_Pos, false); //genere l'effet
            go.transform.position = new Vector2(go.transform.position.x - 0.1f, go.transform.position.y + 0.1f);
            if (level >= 1)
            {
                go.transform.position = new Vector2(go.transform.position.x, go.transform.position.y + 0.8f);
            }
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

    public void Bat1Update(Bat1_Upgrades _bat1upgrade) //modifier visuelement le bat
    { 
        Visual.GetComponent<Image>().sprite = _bat1upgrade.Sprite; //change le sprite du bat
        if (level > 1)
        {
            value_to_upgrade = (int)(value_to_upgrade * 1.2); //change le prix
        }
        else if (level == 1)
        {
            value_to_upgrade = 20;
        }
        bat1_price_txt.text = (value_to_upgrade).ToString(); //change le prix
        bat1_name.text = _bat1upgrade.Name; //change le nom
        bat1_update_description.text = _bat1upgrade.Description; //change la description de l'update
        bat1_update_sprite.sprite = _bat1upgrade.SpriteUpdate; //change le sprite de l'icone dans l'update
        people_need_to_upgrade = _bat1upgrade.PeopleNeed; //remet la variable du nombre de people dont on a besoin a jour
        people_bloc_text.text = people_need_to_upgrade.ToString() + " habitants requis";
        if (Bat1_Upgrades.Count <= level +1)
        {
            MaxBloc.SetActive(true);
        }
    }

    public void AddOuvrier() //ajoute un ouvrier
    {
        //dialog tuto
        if (R_and_P.brick_augmentation == 0) //si on a assez pour acheter l'ouvrier
        {
            _Dialog_box.dialog_number = 4;
            _Dialog_box.DialogUpdateCall();
            _Dialog_box.BumpBox();
            StartCoroutine(_Dialog_box.CloseAfterTimer(3.5f)); //dialogbox se ferme
            StartCoroutine(maingame.FireLauncher(10)); //fire après la fin du premier tuto
        }
        R_and_P.brick_augmentation += 1; //ajoute un ouvrier
        number_of_employee++;
        number_employee_text.text = number_of_employee.ToString(); //ajoute un employé sur l'etiquette
        // -> possiblité d'ajouter graphiquement un ouvrier ici
    }

    public void ShowClickBrick(Transform Hit_Pos, int nb_brick) //anim du clic 
    {
        //si il n'y a pas le feu
        if (is_on_fire == false)
        {
            //affiche l'animation
            for (int i = 0; i < nb_brick; i++)
            {
                GameObject go = GameObject.Instantiate(PrefabClicBrick, Hit_Pos, false); //genere la brique qui sort
                //transform et anim de la brique
                go.transform.position = new Vector2(go.transform.position.x , go.transform.position.y - 4f);
                go.transform.DOScale(0, 0f);
                go.transform.DOComplete();
                go.transform.DOScale(0.6f, 0.3f);
                go.transform.DOLocalJump(new Vector3(Hit_Pos.localPosition.x + Random.Range(-5f, 5f), Hit_Pos.localPosition.y - 2f, 1), Random.Range(2f, 4f), 1, 0.9f);
                GameObject.Destroy(go, 0.8f);
            }

        }
    }
    public void ShowKayou(Transform Hit_Pos) //anim du clic 
    {
        //si il n'y a pas le feu
        if (is_on_fire == false)
        {
            //affiche l'animation
            for (int i = 0; i < 5; i++)
            {
                GameObject go = GameObject.Instantiate(PrefabKayou, Hit_Pos, false); //genere la brique qui sort
                //transform et anim de la pierre
                go.transform.DOScale(0, 0f);
                go.transform.DOComplete();
                go.transform.DOScale(0.75f, 0.3f);
                go.transform.localPosition = new Vector3(Hit_Pos.localPosition.x - 0.5f, Hit_Pos.localPosition.y -2, 1);
                go.transform.DOLocalJump(new Vector3(Hit_Pos.localPosition.x + (i - 2), Hit_Pos.localPosition.y - Random.Range(1f,2f), 1), Random.Range(2f, 4f), 1, 0.9f);
                GameObject.Destroy(go, 0.8f);
            }

        }
    }

    public void FireStart()
    {
        is_on_fire = true;
        Visual.GetComponent<Image>().color = new Color(244, 102, 27);
        R_and_P.brick_number_temp = R_and_P.brick_shown;
        R_and_P.brick_number_before = R_and_P.brick_shown;
        R_and_P.brick_number = (int)R_and_P.brick_shown;
        Fire_bloc.SetActive(true);
        Visual.GetComponent<Collider2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void FireEnd()
    {
        EtiquetteScript.first_focus = false;
        is_on_fire = false;
        maingame.FireEnd();
        Visual.GetComponent<Image>().color = new Color(255, 255, 255);
        Fire_bloc.SetActive(false);
        Visual.GetComponent<Collider2D>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

}
