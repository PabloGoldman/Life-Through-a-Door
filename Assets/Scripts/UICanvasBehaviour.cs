using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasBehaviour : MonoBehaviour
{
    Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }


    private void LateUpdate()
    {
        transform.rotation = cam.transform.rotation;
    }

}
