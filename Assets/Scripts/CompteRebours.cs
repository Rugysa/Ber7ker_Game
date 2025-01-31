using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompteRebours : MonoBehaviour
{
    public float time =1000f;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
        time = time + 1f;
    }
    IEnumerator timer()
    {
        while (time > 0)
        {
            time--;
            yield return new WaitForSeconds(1f);
            GetComponent<TMP_Text>().text = string.Format("{0:0}:{1:00}", Mathf.Floor (time/60), time%60);
        }

        if (time == 0)
        {
            player.GetComponent<Health>().Die();
        }
    }

    public void SetTime (float tps)
    {
        time += tps;
    }
}
