using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public float min = 0.5f;
    public float max = 2.5f;

    void Start() {
        var recognizer = new TKPinchRecognizer();
			recognizer.gestureRecognizedEvent += ( r ) =>
			{
                Vector3 newScale = transform.localScale + Vector3.one * recognizer.deltaScale;
                if (newScale.x > min && newScale.x < max) {
				    transform.localScale  = newScale;
                }
			};
			TouchKit.addGestureRecognizer( recognizer );
    }

}
