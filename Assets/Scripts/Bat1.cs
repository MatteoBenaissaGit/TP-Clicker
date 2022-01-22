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
    #endregion

    public int level = 0; //niveau du bat
    public int value_to_upgrade = 0; //valeur pour ameliorer
    public int people_need_to_upgrade = 0;

    [SerializeField] List<Bat1_Upgrades> Bat1_Upgrades; //usage de la classe Bat1_Upgrade pour modif l'affichage/les vlaeurs

    public TextMeshProUGUI bat1_price_txt; //prix affiché sur bouton
    public TextMeshProUGUI bat1_name; //nom affiché ingame
    public TextMeshProUGUI bat1_update_description; //description affiché sur bouton
    public Image bat1_update_sprite; //image affiché ingame

    public int Bat1_clicdamage = 1; //dégats du clic


    private void Start()
    {
        Bat1Update(Bat1_Upgrades[level]); //reference le bat au start avec son niveau 0
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
    }

    public void UpgradeNextLevel() //passe au niveau suivant
    {
        if (R_and_P.brick_number >= value_to_upgrade)
        {
            R_and_P.brick_number -= value_to_upgrade; //enleve le prix au nb de brick
            level++; //augmente le niveau
            Bat1Update(Bat1_Upgrades[level]); //recupere les valeurs de l'update
            #region action selon le niveau
            if (level == 1) //upgrade du niv1
            {
                arrowtuto.bat1_buyed = true; //variable pour indiquer a la fleche que le bat est acheter
                arrowtuto.posarrow = 2; //modifie le placage de reference de la fleche
                ouvrier.UnlockOuvrier(); //debloque l'ouvrier
                arrowtuto.CheckPosArrow1(); //change la place de la fleche
                //dialog
                _Dialog_box.dialog_number++;
                _Dialog_box.DialogUpdateCall();
                _Dialog_box.BumpBox();
            }
            if (level == 2) //upgrade du niv2
            {
                R_and_P.people_augmentation += 1;
            }
            if (level == 3) //upgrade du niv3
            {
                Bat1_clicdamage += 1;
            }
            if (level == 4) //upgrade du niv4
            {
                R_and_P.people_augmentation += 3;
            }
            if (level == 5) //upgrade du niv4
            {
                R_and_P.brick_multiplier += 1;
            }
            #endregion
        }
    }

    public void Hit(Transform Hit_Pos)
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
        if (level > 0) //si carrière débloquée
        {
            Visual.transform.DOComplete(); //complete l'animation précédente pour éviter bug
            Visual.transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f); //animation de hit sur le bat
            R_and_P.AddBrick(Bat1_clicdamage); //valeurs  
            ShowClickBrick(Hit_Pos.transform); //animation de clic
        }
        if (level == 0) //si la carrière n'est pas débloquée
        {
            //animation réduite de hit sur le bat  
            Visual.transform.DOComplete();
            Visual.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.5f);
        }
    }

    public void Bat1Update(Bat1_Upgrades _bat1upgrade) //modifier visuelement le bat
    { 
        Visual.GetComponent<SpriteRenderer>().sprite = _bat1upgrade.Sprite; //change le sprite du bat
        bat1_price_txt.text = _bat1upgrade.Price.ToString(); //change le prix
        value_to_upgrade = _bat1upgrade.Price; //change le prix
        bat1_name.text = _bat1upgrade.Name; //change le nom
        bat1_update_description.text = _bat1upgrade.Description; //change la description de l'update
        bat1_update_sprite.sprite = _bat1upgrade.SpriteUpdate; //change le sprite de l'icone dans l'update
        people_need_to_upgrade = _bat1upgrade.PeopleNeed; //remet la variable du nombre de people dont on a besoin a jour
        people_bloc_text.text = people_need_to_upgrade.ToString() + " habitants requis";
    }

    public void AddOuvrier() //ajoute un ouvrier
    {
        //dialog tuto
        if (R_and_P.brick_augmentation == 0) //si on a assez pour acheter l'ouvrier
        {
            _Dialog_box.dialog_number++;
            _Dialog_box.DialogUpdateCall();
            _Dialog_box.BumpBox();
            StartCoroutine(_Dialog_box.CloseAfterTimer(3.5f));
        }
        R_and_P.brick_augmentation += 1; //ajoute un ouvrier
        // -> possiblité d'ajouter graphiquement un ouvrier ici
    }

    public void ShowClickBrick(Transform Hit_Pos) //anim du clic 
    {
        //affiche l'animation
        for (int i = 0; i< Bat1_clicdamage; i++)
        {
            GameObject go = GameObject.Instantiate(PrefabClicBrick, Hit_Pos, false); //genere la brique qui sort
            //transform et anim de la brique
            go.transform.DOScale(0, 0f);
            go.transform.DOComplete();
            go.transform.DOScale(0.75f, 0.2f);
            go.transform.localPosition = new Vector3(Hit_Pos.localPosition.x+0.5f, Hit_Pos.localPosition.y, 1);
            go.transform.DOLocalJump(new Vector3(Hit_Pos.localPosition.x + Random.Range(-5f,5f), Hit_Pos.localPosition.y-2f, 1), Random.Range(2f,4f), 1, 0.9f);
            GameObject.Destroy(go, 0.8f);
        }
    }

}
