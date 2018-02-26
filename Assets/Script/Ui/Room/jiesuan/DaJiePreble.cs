using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DaJiePreble : MonoBehaviour {

    public Text zhiGangTxt;
    public Text mingGangTxt;
    public Text anGangTxt;

    public Text hupaiGangTxt;
    public Text dianpaoGangTxt;

    public Text goldTxt;

    public Image paoshou;
    public Image dayingjia;

    public Image headIcon;
    public Text playerName;
    public Image fangzhu;

    public void setData(PlayerData pd)
    {
        zhiGangTxt.text = "x " + pd.getZhiGang().ToString();
        mingGangTxt.text = "x " + pd.getMingGang().ToString();
        anGangTxt.text = "x " + pd.getAnGang().ToString();

        hupaiGangTxt.text = "x " + pd.getHuPaicCunt().ToString();
        dianpaoGangTxt.text = "x " + pd.getFangPaoCunt().ToString();

        goldTxt.text =  pd.getWinGold().ToString();

        Sprite sp = Resources.Load("Texture/Hall/head/head_" + pd.getPlayerIcon().ToString(), typeof(Sprite)) as Sprite;
        headIcon.sprite = sp;

        if (pd.getZhuangjia() == 1)
        {
            fangzhu.gameObject.SetActive(true);
        }

        playerName.text = pd.getPlayerName();
    }

    public void setPaoShou()
    {
        paoshou.gameObject.SetActive(true);
    }

    public void setDaYingJia()
    {
        dayingjia.gameObject.SetActive(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
