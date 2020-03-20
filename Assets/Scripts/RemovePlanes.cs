using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class RemovePlanes : MonoBehaviour
{
    ARPlaneManager _planeManager;

    void Start() {
        _planeManager = GetComponent<ARPlaneManager>();
    }

    public void RemoveAllPlanes() {
       foreach (var plane in _planeManager.trackables)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
