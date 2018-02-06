﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowMouse : MonoBehaviour {

    // Mask used to limit crosshair movement
    // Black pixels should reprensent the accessible area
    public RawImage mask;
    private Texture2D textureMask;
    
    // Array used to have a representation of the mask
    // without having to access pixels of the texture
    // (perfomance reason)
    private int[,] arrayMask;


    private int maskWidth;
    private int maskHeight;

    // Use this for initialization
    void Awake() {
        textureMask = mask.texture as Texture2D;
        maskWidth = Screen.width;
        maskHeight = Screen.height;
        // Ratios used to adapt the mask's texture to the screen
        float ratioX = ((float) textureMask.width) / ((float) maskWidth);
        float ratioY = ((float) textureMask.height) / ((float) maskHeight);
        arrayMask = new int[maskWidth,maskHeight];
        // Filling the array with 1s and 0s, 1 means the area is accessible for the crosshair
        for (int i = 0; i < maskWidth; i++)
        {
            for (int j = 0; j < maskHeight; j++)
            {
                if (textureMask.GetPixel((int)((float)i*ratioX), (int) ((float)j*ratioY)).a > 0)
                {
                    arrayMask[i,j] = 1;
                } else
                {
                    arrayMask[i,j] = 0;
                }
            }
        }
        // Disable the displayed mask
        mask.gameObject.SetActive(false);
        // Disable the default cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        Vector3 pos = Input.mousePosition;

        // Limit cursor position to the screen size
        pos.x = Mathf.Clamp(pos.x, 0, Screen.width-1);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height-1);

        // Check if cursor is out of boundaries
        bool crosshairOutOfBoundaries = (arrayMask[(int)pos.x,(int)pos.y] < 1);
        if (crosshairOutOfBoundaries)
        {
            Vector3 desiredPos = pos;
            bool desiredCrosshairOutOfBoundaries = true;

            // If out of boundaries, place crosshair against them
            while (desiredCrosshairOutOfBoundaries)
            {
                // if crosshair is below the mask, increment its y attribute
                if(desiredPos.y < maskHeight / 2)
                {
                    desiredPos.y += 1;
                } else
                {
                    desiredPos.y -= 1;
                }
                desiredCrosshairOutOfBoundaries = (arrayMask[(int)desiredPos.x,(int)desiredPos.y] < 1);
            }
            // Put the cursor on the found desired position
            gameObject.GetComponent<RectTransform>().position = desiredPos;
        } else
        {
            gameObject.GetComponent<RectTransform>().position = pos;
        }
        
	}
}
