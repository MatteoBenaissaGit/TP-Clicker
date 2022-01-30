using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class logoanim : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ScaleUp());
    }

    public IEnumerator ScaleUp()
    {
        yield return new WaitForSeconds(1f);
        transform.DOComplete();
        transform.DOScale(new Vector3(1.1f, 1.1f, 1), 1f);
        StartCoroutine(ScaleDown());
    }

    public IEnumerator ScaleDown()
    {
        yield return new WaitForSeconds(1f);
        transform.DOComplete();
        transform.DOScale(new Vector3(0.9f, 0.9f, 1), 1f);
        StartCoroutine(ScaleUp());
    }
}
