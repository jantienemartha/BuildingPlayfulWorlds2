using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralInputManager : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        //Exit game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        //Clear notifications
        else if (Input.GetKeyDown(KeyCode.C))
        {
            UIManager.manager.ClearNotification();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            UIManager.manager.ToggleRecipeNote();
        }
	}
}
