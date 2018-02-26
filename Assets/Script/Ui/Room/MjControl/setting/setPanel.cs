using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class setPanel : MonoBehaviour {

    public Button outBtn;
    public Button loginBtn;
    public Button closeBtn;
    // Use this for initialization
    void Start () {
        outBtn.onClick.AddListener(outBtnBtnPressed);
        loginBtn.onClick.AddListener(loginBtnPressed);
        closeBtn.onClick.AddListener(closeBtntPressed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void outBtnBtnPressed()
    {
        Application.Quit();
    }


    public void loginBtnPressed()
    {
        NettySocket.Instance.Closed();
        SceneManager.LoadScene("login");
    }


    public void closeBtntPressed()
    {
        this.transform.gameObject.SetActive(false);
    }
}
