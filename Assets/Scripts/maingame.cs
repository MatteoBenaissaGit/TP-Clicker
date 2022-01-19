using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class maingame : MonoBehaviour
{

    public RectTransform scrollview2;
    public GameObject PrefabHitPoint;
    public RectTransform _scrollcontent;

    public Bat1 bat1;
    [SerializeField] Collider2D BAT1_ref;

    public static maingame Instance;

    private void Awake()
    {
        //singleton
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //récupère la position du clic
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
            //action si on clic sur le monstre
            if (hit.collider == BAT1_ref)
            {
                bat1.Hit(hit.transform);
            }
        }
    }

    public void ShowBrick(Transform Pos, int value)
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

}
