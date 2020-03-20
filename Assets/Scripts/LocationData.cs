using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationData : MonoBehaviour
{
    public Location location;
    public Transform Globe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (location != null) {
            float zScale = Mathf.Max(0.0001f, 2f * Mathf.Log10(location.latest.confirmed * 0.008f));
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, zScale), Time.deltaTime * 0.5f);
        }
    }
}
