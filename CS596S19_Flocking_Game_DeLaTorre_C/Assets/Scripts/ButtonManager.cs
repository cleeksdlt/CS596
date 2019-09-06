using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {

    public GameObject menuList;
    public Button quitButton;

	// Use this for initialization
	void Start () {
      
	}

    public void Toggle_Changed(bool val) {
        menuList.SetActive(val);
    }

    public void QuitGame() {
        Application.Quit();
    }


}
