using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class maingame : MonoBehaviour
{
    public List<MonsterInfos> Monsters;

    public List<Upgrade> Upgrades;
    public GameObject PrefabUpgradeUI;
    public GameObject ParentUpgrades;
    List<Upgrade> _unlockedUpgrades = new List<Upgrade>();
    public RectTransform scrollview2;

    int _currentMonster;
    public Monster Monster;
    public GameObject PrefabHitPoint;

    public RectTransform _scrollcontent;

    int i = 0;

    float _timerAutoDamage = 0f;

    public int _clicdammage = 1;

    public static maingame Instance;

    private void Awake()
    {
        //set le monstre au premier de la liste
        Monster.SetMonster(Monsters[_currentMonster]);
        //place correctement le scroll pour qu'il démarre au top
        _scrollcontent.pivot = new Vector2(0, 0.85f);
        //singleton
        Instance = this;
    }

    private void Start()
    {
        Monster.SetMonster(Monsters[_currentMonster]);

        //boucle pour afficher les upgrades
        /*foreach(var upgrade in Upgrades)
        {
            GameObject go = GameObject.Instantiate(PrefabUpgradeUI, ParentUpgrades.transform, false);
            go.transform.localPosition = new Vector3(scrollview2.localPosition.x, (i * -80),0);
            i++;
            go.GetComponent<UpgradeUI>().Initialize(upgrade);
        }*/
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //récupère la position du clic
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
            //action si on clic sur le monstre
            if (hit.collider != null)
            {
                Hit(_clicdammage, Monster);
            }

            //si le monstre n'a plus de points de vie -> change le monstre
            NextMonsterCheck();
        }

        //attaque auto
        _timerAutoDamage += Time.deltaTime;
        //update toute les secondes
        if (_timerAutoDamage >= 1.0f)
        {
            _timerAutoDamage = 0;
            //met les dégars nécéssaire pour chaque update
            foreach(var upgrade in _unlockedUpgrades)
            {
                Hit(upgrade.DPS, Monster);
            }
        }

    }

    public void NextMonsterCheck()
    {
        //si le monstre n'a plus de points de vie -> change le monstre
        if (Monster.IsAlive() == false)
        {
            _currentMonster++;
            Monster.SetMonster(Monsters[_currentMonster]);
        }
    }

    public void Hit(int damage, Monster monster)
    {
        //met les dégats au monstre via sa fonction Hit à lui
        monster.Hit(damage);
        //affiche mes dégats
        GameObject go = GameObject.Instantiate(PrefabHitPoint, monster.Canvas.transform, false);
        go.GetComponent<TextMesh>().text = damage.ToString();
        go.transform.localPosition = UnityEngine.Random.insideUnitCircle * 180;
        go.transform.localPosition = monster.transform.localPosition;
        go.transform.DOLocalMoveY(8, 0.8f);
        go.GetComponent<Text>().DOFade(0, 0.8f);
        GameObject.Destroy(go, 0.8f);
        //si le monstre n'a plus de points de vie -> change le monstre
        NextMonsterCheck();
    }

    public void AddUpgrade(Upgrade upgrade)
    {
        _unlockedUpgrades.Add(upgrade);
    }

}
