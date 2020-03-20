using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnSphere : MonoBehaviour
{

    public Transform SpherePosition;
    public float Radius;

    public float lat;
    public float lon;

    // Start is called before the first frame update
    void Start()
    {   
        Quaternion oldRotation = SpherePosition.parent.rotation;
        SpherePosition.parent.rotation = Quaternion.identity;

        float latRadian = lat * Mathf.Deg2Rad;
        float longRadian = lon * Mathf.Deg2Rad;

        transform.position = SpherePosition.position + new Vector3(Radius * Mathf.Cos(latRadian) * Mathf.Cos(longRadian),
                                                                    Radius * Mathf.Sin(latRadian),
                                                                    Radius * Mathf.Cos(latRadian) * Mathf.Sin(longRadian));

        transform.LookAt(SpherePosition.position + (transform.position - SpherePosition.position) * 2);
        SpherePosition.parent.rotation = oldRotation;
    }

}
