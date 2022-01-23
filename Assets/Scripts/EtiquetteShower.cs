using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EtiquetteShower : MonoBehaviour
{
    public Transform Etiquette;
    public BoxCollider2D Collider_Etiquette;
    public Collider2D Collider_Fire;
    public Transform ScrollRect;
    public Transform FocusCamObject;
    bool isfocusing = false;

    private void Start()
    {
        MouseIsOver();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
            if (hit.collider == Collider_Etiquette)
            {
                MouseIsOver();
                isfocusing = true;
            }
            else if (hit.collider == Collider_Fire)
            {
                isfocusing = true;
            }
            else if (hit.collider != Collider_Fire)
            {
                MouseIsExit();
            }

        }
        if (isfocusing == true)
        {
            FocusCam();
            isfocusing = false;
        }
    }

    public void FocusCam()
    {
        ScrollRect.DOMove(FocusCamObject.position, 0.5f);
    }

    public void MouseIsOver()
    {
        Etiquette.DOScale(1f, 0.2f);
    }
    public void MouseIsExit()
    {
        Etiquette.DOScale(0f, 0.2f);
    }
}
