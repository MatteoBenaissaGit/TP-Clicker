using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class menuscript : MonoBehaviour
{
    public Transform credits;
    public Collider2D window;
    public Transform _window;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //récupère la position du clic
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);
            //action si on clic sur un batiment
            if (hit.collider == window)
            {
                PunchWindow();
            }
        }
    }

    public void PunchWindow()
    {
        _window.DOComplete();
        _window.DOPunchScale(new Vector3(0.1f, 0.2f, 0), 0.4f);
    }

    public void OpenCredits()
    {
        credits.DOComplete();
        credits.DOScaleX(1, 0.2f);
        credits.DOScaleY(0.2f, 0.4f);
        StartCoroutine(launchscaleY(0.2f));
    }

    public IEnumerator launchscaleY(float time)
    {
        yield return new WaitForSeconds(time);
        credits.DOComplete();
        credits.DOScaleY(1, 0.4f);
    }
    public void CloseCredits()
    {
        credits.DOComplete();
        credits.DOScaleX(1f, 0.2f);
        credits.DOScaleY(0.2f, 0.2f);
        StartCoroutine(launchdescaleX(0.2f));
    }
    public IEnumerator launchdescaleX(float time)
    {
        yield return new WaitForSeconds(time);
        credits.DOComplete();
        credits.DOScaleX(0, 0.4f);
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
