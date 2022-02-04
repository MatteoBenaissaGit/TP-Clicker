using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class menuscript : MonoBehaviour
{
    public SoundManager sound;

    public Transform credits;
    public Collider2D window;
    public Transform _window;

    public Image fondblack;
    bool fade_black = false;
    float color_value = 0.01f;

    public Image textintro;
    bool text_intro = false;
    float textintro_color_value = 0.01f;
    bool text_intro_unshow = false;

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
                sound.TextPopUp();
                PunchWindow();
            }
        }
        if (fade_black == true)
        {
            //diminue petit à petit l'opacité
            Color tmp = fondblack.color;
            tmp.a = color_value;
            fondblack.color = tmp;
            color_value = color_value * 1.05f; //valeur
        }
        if (text_intro == true)
        {
            //diminue petit à petit l'opacité
            Color tmp = textintro.color;
            tmp.a = textintro_color_value;
            textintro.color = tmp;
            textintro_color_value = textintro_color_value * 1.05f; //valeur
        }
        if (text_intro_unshow == true)
        {
            //diminue petit à petit l'opacité
            Color tmp = textintro.color;
            tmp.a = textintro_color_value;
            textintro.color = tmp;
            textintro_color_value = textintro_color_value * 0.95f; //valeur
        }
    }

    public void PunchWindow()
    {
        _window.DOComplete();
        _window.DOPunchScale(new Vector3(0.1f, 0.2f, 0), 0.4f);
    }

    public void OpenCredits()
    {
        sound.ClicUI();
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
        sound.ClicUI();
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
        sound.ClicUI();
        fade_black = true;
        StartCoroutine(TextShow());
    }

    public IEnumerator TextShow()
    {
        yield return new WaitForSeconds(2f);
        text_intro = true;
        StartCoroutine(TextUnshow());
    }

    public IEnumerator TextUnshow()
    {
        yield return new WaitForSeconds(2f);
        text_intro = false;
        text_intro_unshow = true;
        StartCoroutine(ChangeRoom());
    }

    public IEnumerator ChangeRoom()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
