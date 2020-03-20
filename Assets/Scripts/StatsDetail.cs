using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsDetail : MonoBehaviour
{
    public Text CountryText;
    public Text ConfirmedText;
    public Text RecoveredText;
    public Text DeathsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetLocation(Location location) {
        CountryText.text = location.country;
        if (location.province != "") {
            CountryText.text += " (" + location.province + ")";
        }
        ConfirmedText.text = location.latest.confirmed.ToString();
        RecoveredText.text = location.latest.recovered.ToString();
        DeathsText.text = location.latest.deaths.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
