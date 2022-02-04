using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip achat;
    public AudioClip alerte;
    public AudioClip clic_bat;
    public AudioClip clic_incendie;
    public AudioClip clic_UI;
    public AudioClip explosion;
    public AudioClip end_incendie_and_upgrade;
    public AudioClip incendie;
    public AudioClip music;
    public AudioClip textpop;

    public AudioSource musicmanager;
    public AudioSource incendiemanager;
    public AudioSource clicsmanager;
    public AudioSource explosionmanager;
    public AudioSource achatsmanager;
    public AudioSource textmanager;
    public AudioSource upgrademanager;
    public AudioSource firemusic;

    public void ClicCarrière()
    {
        clicsmanager.clip = clic_bat;
        clicsmanager.Play();
    }
    public void ClicUI()
    {
        clicsmanager.clip = clic_UI;
        clicsmanager.Play();
    }
    public void SoundUpgrade()
    {
        upgrademanager.clip = end_incendie_and_upgrade;
        upgrademanager.Play();
    }
    public void ClicAchats()
    {
        achatsmanager.clip = achat;
        achatsmanager.Play();
    }

    public void TextPopUp()
    {
        textmanager.clip = textpop;
        textmanager.Play();
    }

    public void ClicFire()
    {
        textmanager.clip = textpop;
        textmanager.Play();
    }

    public void FireExplosion()
    {
        explosionmanager.clip = explosion;
        explosionmanager.Play();
    }

    public void FireSound()
    {
        firemusic.clip = incendie;
        firemusic.Play();
    }
    public void FireSoundStop()
    {
        firemusic.Stop();
    }
}

