﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public List<Rigidbody> listeDeRigidbody;
    List<Vector3> listDeVitesseActuel;
    List<Vector3> listDeVitesseAngulaireActuel;
    List<bool> listeDeUseGravity;

    private GameObject UI;

    public static bool isGamePaused;
    // Start is called before the first frame update
    void Start()
    {
        UI = GameObject.FindGameObjectWithTag("UI");
        isGamePaused = false;
        UI.SetActive(false);

    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (Input.GetButtonDown("PauseButton"))
        {
            if (isGamePaused)
            {
                this.OnGameUnpause();
            }
            else
            {
                this.OnGamePause();
            }
            isGamePaused = !isGamePaused;
        }
    }

    private void OnGamePause() {
        Time.timeScale = 0;
        UI.SetActive(true);
    }

    private void OnGameUnpause() {
        Time.timeScale = 1;
        UI.SetActive(false);
    }

    void pauseGame()
    {
        foreach (var item in listeDeRigidbody)
        {
            item.velocity = new Vector3();
            item.angularVelocity = new Vector3();
            item.useGravity = false;
        }
    }
    void resumeGame()
    {
        for (int i = 0; i < listeDeRigidbody.Count; i++)
        {
            listeDeRigidbody[i].velocity = listDeVitesseActuel[i];
            listeDeRigidbody[i].angularVelocity = listDeVitesseAngulaireActuel[i];
            listeDeRigidbody[i].useGravity = listeDeUseGravity[i];
        }
    }

    void saveVariable()
    {
        listDeVitesseActuel.Clear();
        listDeVitesseAngulaireActuel.Clear();
        listeDeUseGravity.Clear();
        for (int i = 0; i < listeDeRigidbody.Count; i++)
        {
            listDeVitesseActuel.Insert(
                i,
                listeDeRigidbody[i].velocity);
            listDeVitesseAngulaireActuel.Insert(
                i,
                listeDeRigidbody[i].angularVelocity);
            listeDeUseGravity.Insert(
                i,
                listeDeRigidbody[i].useGravity);
        }
    }
    
}
