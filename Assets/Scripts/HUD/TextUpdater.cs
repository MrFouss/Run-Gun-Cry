using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextUpdater : MonoBehaviour
{
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void UpdateText(string c)
    {
        text.text = c;
    }
}
