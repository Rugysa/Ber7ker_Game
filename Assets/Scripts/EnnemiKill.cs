using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnnemiKill : MonoBehaviour
{
    private int nb_ennemi = 0;

    private void FixedUpdate()
    {
        GetComponent<TMP_Text>().text = nb_ennemi.ToString();
    }


    public void SetNbEnnemi()
    {
        nb_ennemi += 1;
    }
}
