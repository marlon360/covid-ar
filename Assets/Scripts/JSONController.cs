using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

[System.Serializable]
public class LocationsLoadedEvent : UnityEvent<List<Location>>
{
}

public class JSONController : MonoBehaviour
{

    public string URL;

    public LocationsLoadedEvent OnDataLoaded;
    public List<Location> Locations = new List<Location>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(getData());
    }

    IEnumerator getData() {

        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();
 
        if(www.isNetworkError || www.isHttpError) {
            Debug.Log(www.error);
        }
        else {
            processJSONData(www.downloadHandler.text);
        }
    }

    void processJSONData(string data) {
        JSONData jsonData = JsonUtility.FromJson<JSONData>(data);
        Locations = jsonData.locations;
        OnDataLoaded.Invoke(Locations);
    }
}
