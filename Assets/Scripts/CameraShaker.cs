using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public static CameraShaker Instance;
    private Camera _camera;
   

    void Start()
    {
        Instance = this;
        _camera = GetComponent<Camera>();
    }

    public void Shake(float magnitude, float temps)
    {
        StartCoroutine(ShakerCamera(magnitude, temps));
    }

    IEnumerator ShakerCamera(float magnitude, float temps)
    {
        float time_to_shake = 0f;
        Vector3 init_pos = _camera.transform.position;
        while (time_to_shake < temps)
        {
            Vector3 modif = Random.insideUnitSphere;
            _camera.transform.position = init_pos + magnitude * Time.fixedDeltaTime * modif;
            time_to_shake += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
        _camera.transform.position = init_pos;
    }
}
