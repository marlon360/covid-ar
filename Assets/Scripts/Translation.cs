using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Translation : MonoBehaviour
{

    Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;

        var recognizer = new TKPanRecognizer();

        // when using in conjunction with a pinch or rotation recognizer setting the min touches to 2 smoothes movement greatly
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            recognizer.minimumNumberOfTouches = 2;

        recognizer.gestureRecognizedEvent += (r) =>
        {   
            Vector3 forward = Vector3.ProjectOnPlane(_mainCamera.transform.forward, Vector3.up) * recognizer.deltaTranslation.y;
            Vector3 right = Vector3.ProjectOnPlane(_mainCamera.transform.right, Vector3.up) * recognizer.deltaTranslation.x;
            Vector3 newPos = (forward + right) / 1000;
            transform.position += newPos;
        };

        TouchKit.addGestureRecognizer(recognizer);
    }

}
