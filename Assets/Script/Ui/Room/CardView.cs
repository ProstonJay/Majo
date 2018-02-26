using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour {

    public const int CHI_GUO_过牌 = 0;
    public  const int CHI_PENG_碰牌 = 1;
    public  const int CHI_GANG_杠牌 = 2   ;
    public  const int CHI_HU_糊牌 = 3;
    public const int CHI_MINGGANG_明杠 = 4;
    public const int CHI_ANGANG_暗杠 = 5;
    public const int CHI_GANG_抢杠 = 6;
    public const int CHI_ZIMO_自摸 = 7;
    public const int CHI_ZHIGANGKAIHUA_直杠开花 = 8;
    public const int CHI_MINGGANGKAIHUA_明杠开花 = 9;
    public const int CHI_ANGANGKAIHUA_暗杠开花 = 10;

    public GameObject myControlBar;
    public GameObject rightControlBar;
    public GameObject topControlBar;
    public GameObject leftControlBar;

    public GameObject myOutBar;
    public GameObject rightOutBar;
    public GameObject topOutBar;
    public GameObject leftOutBar;

    public GameObject myActionBar;
    public GameObject rightActionBar;
    public GameObject topActionBar;
    public GameObject leftActionBar;


    //重置
    public void reset()
    {
        //
        myControlBar.GetComponent<MyControlBar>().resetAll();
        rightControlBar.GetComponent<RightControlBar>().reset();
        topControlBar.GetComponent<TopControlBar>().reset();
        leftControlBar.GetComponent<LeftControlBar>().reset();
        //
        myOutBar.GetComponent<MyOutBar>().reset();
        rightOutBar.GetComponent<RightOutBar>().reset();
        topOutBar.GetComponent<TopOutBar>().reset();
        leftOutBar.GetComponent<LeftOutBar>().reset();
        //
        myActionBar.GetComponent<Action_My>().reset();
        rightActionBar.GetComponent<Action_Right>().reset();
        topActionBar.GetComponent<Action_Top>().reset();
        leftActionBar.GetComponent<Action_Left>().reset();

    }

    // Use this for initialization
    void Start()
    {
        reset();
    }

    public void setStartHandMj()
    {
        myControlBar.GetComponent<MyControlBar>().initHandMj();
        rightControlBar.GetComponent<RightControlBar>().initHandMj();
        topControlBar.GetComponent<TopControlBar>().initHandMj();
        leftControlBar.GetComponent<LeftControlBar>().initHandMj();
    }

    //摸牌
    public void moPai(int pos, int data)
    {
        if (data > 0)
        {
            myControlBar.GetComponent<MyControlBar>().MoPai(data);
        }
   
    }

    public void addMjToOutBar(string pos,string mjid)
    {
        switch (pos)
        {
            case "bot":
                myOutBar.GetComponent<MyOutBar>().chupai(mjid);
                break;
            case "right":
                rightOutBar.GetComponent<RightOutBar>().chupai(mjid);
                break;
            case "top":
                topOutBar.GetComponent<TopOutBar>().chupai(mjid);
                break;
            case "left":
                leftOutBar.GetComponent<LeftOutBar>().chupai(mjid);
                break;
        }
    }




    // Update is called once per frame
    void Update () {
		
	}
}
