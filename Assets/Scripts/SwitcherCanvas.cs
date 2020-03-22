using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitcherCanvas : MonoBehaviour
{

    public GameObject Globe;
    public GameObject Chart;

    public Text ButtonText;

    public void Toggle() {
        if (Globe.activeInHierarchy) {
            Globe.SetActive(false);
            Chart.SetActive(true);
            ButtonText.text = "Globe";
        } else {
            Globe.SetActive(true);
            Chart.SetActive(false);
            ButtonText.text = "Chart";
        }
    }
}
