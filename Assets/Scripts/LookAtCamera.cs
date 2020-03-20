using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Camera mainCam;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = new Vector3(mainCam.transform.position.x, transform.position.y, mainCam.transform.position.z);
        transform.LookAt(target, Vector3.up);
    }
}
