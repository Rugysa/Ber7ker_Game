using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class RecupereID : MonoBehaviour
{
    private string id ="";
    [SerializeField] private Score score;
    private string key = "pdhumho";

    private void Start()
    {
       int res =score.GetScore();
        string[] args = { res.ToString() , key};
       id = YsaCipher.Main(args);
       GetComponent<TMP_Text>().text = id;
    }
}
