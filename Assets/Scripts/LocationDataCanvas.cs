using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationDataCanvas : LocationData
{

    public Image Panel;
    public Text Text;

    private Camera _cam;
    private StatsDetail _statsDetail;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;

        _statsDetail = FindObjectOfType<StatsDetail>();
    }

    public void ShowDetails() {
        _statsDetail.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        _statsDetail.SetLocation(location);
        _statsDetail.transform.rotation = transform.rotation;
        _statsDetail.transform.position = transform.position;
        _statsDetail.transform.Translate(0.1f, 0, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        if (location != null) {
            Text.text = "" + location.latest.confirmed + "";
            float scale = Mathf.Max(0.01f, 0.2f * Mathf.Log10(location.latest.confirmed * 0.02f));
            float distanceToCamera = Vector3.Distance(_cam.transform.position, Globe.position);

            if (distanceToCamera < 0.8) {
                Text.gameObject.SetActive(true);
            } else {
                Text.gameObject.SetActive(false);
            }

            distanceToCamera = Mathf.Max(Mathf.Min(distanceToCamera, 1f), 0.4f);
            scale *= distanceToCamera;
            Panel.transform.localScale = new Vector3(scale, scale, 1f);

            Panel.color = Color.Lerp(new Color32(255, 247, 0, 168), new Color32(255, 80, 0, 168), Mathf.Clamp(Mathf.Log10(location.latest.confirmed) / 5f, 0f, 1f));
            //Panel.color = Color.Lerp(Color.yellow, Color.red, Mathf.Clamp(location.latest.confirmed / 50000f, 0f, 1f));
        }
    }
}
