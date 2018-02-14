using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScaler : MonoBehaviour
{

    // Script used to scale a RawImage to fit the screen
    // Use this for initialization
    void Start()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
