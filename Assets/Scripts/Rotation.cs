using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private StatsDetail _statsDetail;

    void Start()
    {
         _statsDetail = FindObjectOfType<StatsDetail>();
        var recognizer = new TKPanRecognizer();
        recognizer.gestureRecognizedEvent += (r) =>
        {
            transform.Rotate(transform.up, -0.1f * recognizer.deltaTranslation.x);
            _statsDetail.transform.localScale = Vector3.zero;
        };
        TouchKit.addGestureRecognizer(recognizer);
    }
}
