using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationManager : MonoBehaviour
{

    public GameObject LocationPrefab;
    public Transform Globe;
    public float Threshold;

    private JSONController _json;
    private List<GameObject> _locationObjects = new List<GameObject>(); 

    void Start() {
        _json = FindObjectOfType<JSONController>();
        CreateLocations(_json.Locations);
        _json.OnDataLoaded.AddListener(CreateLocations);

    }

    public void CreateLocations(List<Location> locations) {
        _removeLocations();
        foreach (Location location in locations)
        {   
            if (location.latest.confirmed > Threshold) {
                GameObject locationInstance = GameObject.Instantiate(LocationPrefab);
                locationInstance.transform.SetParent(Globe.parent);

                PlaceOnSphere spherePostion = locationInstance.GetComponent<PlaceOnSphere>();
                spherePostion.lon = float.Parse(location.coordinates.longitude);
                spherePostion.lat = float.Parse(location.coordinates.latitude);
                spherePostion.SpherePosition = Globe;

                LocationData locationData = locationInstance.GetComponent<LocationData>();
                locationData.location = location;
                locationData.Globe = Globe;

                _locationObjects.Add(locationInstance);
            }

        }
    }

    private void _removeLocations() {
        foreach (GameObject locationObject in _locationObjects) {
            GameObject.Destroy(locationObject);
        }
    }
    

}
