using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOutBarColorChanger : MonoBehaviour {

    private ProgressBar _progressBar;
    
    public Color SuccessColor;
    public Color FailureColor;

    // Use this for initialization
    void Start () {
        _progressBar = GetComponent<ProgressBar>();
	}
	
	public void SetProgressBarColor(bool success)
    {
        if (success)
        {
            _progressBar.BarColor = SuccessColor;
        }
        else
        {
            _progressBar.BarColor = FailureColor;
        }
    }
}
