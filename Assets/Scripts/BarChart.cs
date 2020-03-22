using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarChart : MonoBehaviour
{   
    public GameObject BarPrefab;
    public GameObject CountryCanvas;

    public Color MaxColor;
    public Color MinColor;

    private JSONController _json;

    private List<GameObject> _locationObjects = new List<GameObject>(); 
    
    void Start() {
        _json = FindObjectOfType<JSONController>();
        CreateLocations(_json.Locations);
        _json.OnDataLoaded.AddListener(CreateLocations);
    }

    public void CreateLocations(List<Location> locations) {
        _removeLocations();

        GameObject parentObject = GameObject.Instantiate(new GameObject());

        Vector3 currentPostion = Vector3.zero;
        foreach (Location location in locations) {
            if (location.latest.confirmed > 1000) {
                GameObject canvas = GameObject.Instantiate(CountryCanvas);
                canvas.transform.parent = parentObject.transform;
                canvas.transform.position = currentPostion + new Vector3(-28, 0.5f, 0);
                canvas.transform.rotation = Quaternion.Euler(90,0,180);
                canvas.GetComponentInChildren<Text>().text = location.country;
                if (location.province != "") {
                    canvas.GetComponentInChildren<Text>().text += ", " + location.province;
                }
                CreateLocationChart(location, currentPostion, parentObject);
                currentPostion += new Vector3(0,0,8);
            }
        }
        parentObject.transform.parent = transform;
        parentObject.transform.position = Vector3.zero;
        transform.localScale = Vector3.one * 0.008f;
    }

    private void _removeLocations() {
        foreach (GameObject locationObject in _locationObjects) {
            GameObject.Destroy(locationObject);
        }
        transform.localScale = Vector3.one;
    }

    public void CreateLocationChart(Location location, Vector3 startPos, GameObject parent) {

        Vector3 currentPostion = startPos;
        float lastYPos = 0f;
        foreach (TimelineEntry entry in location.timelines.confirmed.GetHistory())
        {
            GameObject barObject = GameObject.Instantiate(BarPrefab);
            barObject.transform.parent = parent.transform;
            float newYPos = entry.value / 500f;
            float difference = lastYPos - newYPos;
            barObject.transform.position = currentPostion + new Vector3(0,newYPos,0);
            barObject.transform.localScale = new Vector3(1, -Mathf.Max(0.0001f, Mathf.Abs(difference)), 1);


            Color color = Color.Lerp(MinColor, MaxColor, Mathf.Clamp01(entry.value / 20000f));
            barObject.GetComponentInChildren<Renderer>().material.SetColor("_BaseColor", color);

            currentPostion += new Vector3(1,0,0);
            lastYPos = newYPos;
        }

    }
}
