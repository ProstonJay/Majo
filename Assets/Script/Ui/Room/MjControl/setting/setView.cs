using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setView : MonoBehaviour {

    public Button setBtn;
    public GameObject setPanel;

    public Button settingBtn;
    public GameObject settingPanel;

    // Use this for initialization
    void Start () {
        setBtn.onClick.AddListener(setPressed);
        settingBtn.onClick.AddListener(settingPressed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void settingPressed()
    {
        settingPanel.GetComponent<SetView>().setAvtie();
    }

    public void setPressed()
    {
        setPanel.SetActive(true);
    }
}
