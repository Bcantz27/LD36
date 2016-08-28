using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class MainMenu : MonoBehaviour
{

    public GUISkin guiSkin;
	public float growthSpeed = 40;//40 looks pretty good
	public float sizeModifier;
	public bool flipGrowth;

    //Use this for GUI
    void OnGUI()
    {
		//int fontSize = Convert.ToInt32(sizeModifier) / 10;
		guiSkin.customStyles[0].fontSize = Screen.width / 10;
		guiSkin.button.fontSize = Screen.width / 14;

        GUI.Box(new Rect(0,Screen.height/7,Screen.width,(Screen.height/5) + sizeModifier),"18 Quintillion Castles",guiSkin.customStyles[0]);

		if (GUI.Button(new Rect(Screen.width/2 - Screen.width/8, Screen.height / 1.8f, Screen.width / 4, Screen.height / 5), "Play",guiSkin.button))
		{
			SceneManager.LoadScene("MainGame");
		}
    }
    
	// Use this for initialization
	void Start ()
    {
        if (sizeModifier == null)
        {
			sizeModifier = 0;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (flipGrowth)
		{
			sizeModifier += Time.deltaTime * growthSpeed;
		}
		else
		{
			sizeModifier -= Time.deltaTime * growthSpeed;
		}

		if (sizeModifier > Screen.height / 6)
		{
			flipGrowth = false;
		}
		if (sizeModifier < Screen.height / 20)
		{
			flipGrowth = true;
		}
	}

}
