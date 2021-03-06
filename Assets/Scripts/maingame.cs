using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class maingame : MonoBehaviour
{
    //son
    public SoundManager soundmanager;

    public GameObject PrefabArrowTuto;
    [HideInInspector] public bool arrow1 = true;

    public GameObject fire;
    public FireScript firescript;
    public bool is_on_fire = false;

    public RectTransform scrollview2;
    public GameObject PrefabHitPoint;
    public RectTransform _scrollcontent;

    public Bat1 bat1;
    [SerializeField] Collider2D BAT1_ref;
    public Bat2 bat2;
    [SerializeField] Collider2D BAT2_ref;
    public Bat3 bat3;
    [SerializeField] Collider2D BAT3_ref;
    public Bat4 bat4;
    [SerializeField] Collider2D BAT4_ref;
    public Bat5 bat5;
    [SerializeField] Collider2D BAT5_ref;
    public Bat6 bat6;
    [SerializeField] Collider2D BAT6_ref;

    public static maingame Instance;

    public Buttons buttons;

    public GameObject PrefabStar;

    public CameraShake camerashake;

    private void Awake()
    {
        //singleton
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartingArrow(2));
    }

    public IEnumerator StartingArrow(float time)
    {
        yield return new WaitForSeconds(time);
        PrefabArrowTuto.SetActive(true);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && buttons.actualselect == 0)
        {
            //récupère la position du clic
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
            //action si on clic sur un batiment
            if (hit.collider == BAT1_ref)
            {
                soundmanager.ClicCarrière();
                bat1.Hit(hit.transform);
            }
            if (hit.collider == BAT2_ref)
            {
                soundmanager.ClicUI();
                bat2.Hit(hit.transform);
            }
            if (hit.collider == BAT3_ref)
            {
                soundmanager.ClicUI();
                bat3.Hit(hit.transform);
            }
            if (hit.collider == BAT4_ref)
            {
                soundmanager.ClicUI();
                bat4.Hit(hit.transform);
            }
            if (hit.collider == BAT5_ref)
            {
                soundmanager.ClicUI();
                bat5.Hit(hit.transform);
            }
            if (hit.collider == BAT6_ref)
            {
                soundmanager.ClicUI();
                bat6.Hit(hit.transform);
            }
        }
        //desactive la fleche si le bool arrow1 est false
        if (arrow1 == false)
        {
            PrefabArrowTuto.SetActive(false);
        }
    }

    public void ShowBrick(Transform Pos, int value) //fait apparaitre une brique qui sort du bat
    {
        //affiche mes dégats
        GameObject go = GameObject.Instantiate(PrefabHitPoint, Pos, false);
        go.GetComponent<TextMeshProUGUI>().text = value.ToString();
        go.transform.localPosition = Pos.localPosition;
        go.transform.localPosition = UnityEngine.Random.insideUnitCircle * 1f;
        go.transform.DOLocalMoveY(8, 0.8f);
        //go.GetComponent<TextMeshProUGUI.>().DOFade(0, 0.8f);
        GameObject.Destroy(go, 0.8f);
    }

    public void CheckCanBuy(TextMeshProUGUI _text, int value_number, int value_neaded) //text en rouge quand pas possible d'acheter
    {
        if (value_number >= value_neaded)
        {
            Color tmp = _text.color;
            tmp.r = 0f;
            _text.color = tmp;
        }
        else
        {
            Color tmp = _text.color;
            tmp.r = 255f;
            _text.color = tmp;
        }
    }

    public IEnumerator FireLauncher(float time)
    {
        yield return new WaitForSeconds(time);
        //tuto fire
        if (firescript.tuto_done == false)
        {
            if (bat1.isUpgrading == false) //empeche de bruler un bat qui upgrade
            {
                soundmanager.FireExplosion();
                fire.transform.position = new Vector3(bat1.transform.position.x, bat1.transform.position.y, 0);
                fire.transform.DOScale(new Vector3(4, 4, 1), 0.4f);
                bat1.FireStart();
                //dialog
                bat1._Dialog_box.dialog_number = 5;
                bat1._Dialog_box.DialogUpdateCall();
                bat1._Dialog_box.ActivateBox();
            }
            else
            {
                StartCoroutine(FireLauncher(5)); //fire relaunch
            }
        }
        else //si tuto déjà fait
        {
            int selectbat = Random.Range(1, 1);
            //SELECTION DU FEU SELON LE BATIMENT
            if (selectbat == 1) //si batiment 1
            {
                if (bat1.isUpgrading == false) //empeche de bruler un bat qui upgrade
                {
                    soundmanager.FireExplosion();
                    fire.transform.position = new Vector3(bat1.transform.position.x + 1f, bat1.transform.position.y + 0.5f, 0);
                    fire.transform.DOScale(new Vector3(4, 4, 1), 0.4f);
                    bat1.FireStart();
                }
                else
                {
                    StartCoroutine(FireLauncher(Random.Range(15, 30))); //fire relaunch
                }
            }
        }
    }

    public void FireEnd()
    {
        //destroy le feu
        fire.transform.DOScale(new Vector3(0, 0, 1), 0.4f);
        StartCoroutine(FireLauncher(Random.Range(90, 120))); //fire relaunch

    }

    public void ShowStar(Transform Hit_Pos, float xx, float yy) //anim du clic 
    {
        //affiche l'animation
        for (int i = 0; i < Random.Range(3, 6); i++)
        {
            GameObject go = GameObject.Instantiate(PrefabStar, Hit_Pos, false); //genere la brique qui sort
            go.transform.localPosition = new Vector3(Hit_Pos.localPosition.x + xx, Hit_Pos.localPosition.y + yy, 1);
            //transform et anim de la pierre
            go.transform.DOScale(0, 0f);
            go.transform.DOComplete();
            go.transform.DOScale(0.25f, 0.3f);
            go.transform.DOLocalJump(new Vector3(Hit_Pos.position.x + Random.Range(-4,4), Hit_Pos.position.y - Random.Range(-1f, 2f), 1), Random.Range(1f, 3f), 1, 0.9f);
            GameObject.Destroy(go, 0.8f);
        }

    }

    public void GoToMenu()
    {
        soundmanager.ClicUI();
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

}
