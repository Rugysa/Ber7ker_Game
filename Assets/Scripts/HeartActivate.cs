using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartActivate : MonoBehaviour

{

    [SerializeField] private float tps_spawn = 20f;
    [SerializeField] private GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(tps_spawn);
        heart.SetActive(true);
        StartCoroutine(Spawn());
    }

}
