using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DaJieSuan : MonoBehaviour {

    public GameObject jieSuanPanel1;
    public GameObject jieSuanPanel2;
    public GameObject jieSuanPanel3;
    public GameObject jieSuanPanel4;

    public Button leaveBtn;

    //结算
    private List<PlayerData> plaerlist;

    // Use this for initialization
    void Start () {
        leaveBtn.onClick.AddListener(leavePressed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void showJiesuan(List<PlayerData> plaerlist)
    {
        int fangpao = 0;
        GameObject paoshou = null;
        int gold = 0;
        GameObject dayingjia = null;
        for (int i = 0; i < plaerlist.Count; i++)
        {
            PlayerData pd = plaerlist[i];
            if (i == 0)
            {
                if(pd.getFangPaoCunt()> fangpao)
                {
                    paoshou = jieSuanPanel1;
                }
                if (pd.getWinGold() > gold)
                {
                    dayingjia = jieSuanPanel1;
                }
                jieSuanPanel1.GetComponent<DaJiePreble>().setData(pd);
            }
            else if (i == 1)
            {
                if (pd.getFangPaoCunt() > fangpao)
                {
                    paoshou = jieSuanPanel2;
                }
                if (pd.getWinGold() > gold)
                {
                    dayingjia = jieSuanPanel2;
                }
                jieSuanPanel2.GetComponent<DaJiePreble>().setData(pd);
            }
            else if (i == 2)
            {
                if (pd.getFangPaoCunt() > fangpao)
                {
                    paoshou = jieSuanPanel3;
                }
                if (pd.getWinGold() > gold)
                {
                    dayingjia = jieSuanPanel3;
                }
                jieSuanPanel3.GetComponent<DaJiePreble>().setData(pd);
            }
            else if (i == 3)
            {
                if (pd.getFangPaoCunt() > fangpao)
                {
                    paoshou = jieSuanPanel4;
                }
                if (pd.getWinGold() > gold)
                {
                    dayingjia = jieSuanPanel4;
                }
                jieSuanPanel4.GetComponent<DaJiePreble>().setData(pd);
            }
            if(i== plaerlist.Count - 1)
            {
                paoshou.GetComponent<DaJiePreble>().setPaoShou();
                dayingjia.GetComponent<DaJiePreble>().setDaYingJia();
            }
        }
    }
    //离开房间
    public void leavePressed()
    {
        GameInfo.Instance.resetAll();
        SceneManager.LoadScene("hall");
    }
}
