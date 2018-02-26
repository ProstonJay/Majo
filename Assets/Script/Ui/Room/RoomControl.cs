using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoomControl : MonoBehaviour
{
    public GameObject backView;
    public GameObject startView;
    public GameObject mjView;
    public GameObject readyView;
    public GameObject actionView;
    public GameObject jiesuanView;


    private int startFlag ;

    //摸的什么牌
    private int MP_mj ;

    //出牌玩家位置
    private int CP_positon ;
    //出的什么牌
    private int CP_mj ;
    //是否保留
    private bool isKeep;

    //dri 方向指示
    private int dir;
    private int clearOutMj;

    //打牌区添加打出的牌
    private string outMJid = "";
    private string outPos = "";

    //重置
    private int resetFlag;

    void Awake()
    {
        GameEvent.GameStartEvent+= GameStart;
        GameEvent.MoPaiEvent += MoPai;
        GameEvent.ChuPaiEvent += ChuPai;
        GameEvent.PlayEvent += addOutMj;
        GameEvent.ReSetRoomEvent += ReSetRoom;
        RoomEvent.PlayerDictionEvent += SetDir;
        //

    }

    //重置
    private void ReSetRoom()
    {
        resetFlag = 1;
    }

    //打牌区添加打出的牌
    private void addOutMj(string pos,string mjid)
    {
        outPos = pos;
        outMJid = mjid;
    }

    //出牌
    private void ChuPai(int pos, int data,bool boo)
    {
        this.CP_positon = pos;
        this.CP_mj = data;
        this.isKeep = boo;
    }
    //摸牌
    private void MoPai(int data)
    {
        this.MP_mj = data;
       
    }
    //开局
    private void GameStart(int type)
    {
        if (type == 1)
        {
            startFlag = 1;
        }

    }

    //方向指示
    private void SetDir(int pos,int clearMj)
    {
        dir = pos;
        clearOutMj = clearMj;
    }

    private void setStartView(bool boo)
    {
        startView.gameObject.SetActive(boo);
    }

    private void setMjView(bool boo)
    {
        mjView.gameObject.SetActive(boo);
    }

    private void setReadyView(bool boo)
    {
        readyView.gameObject.SetActive(boo);
    }

    private void setActionView(bool boo)
    {
        actionView.gameObject.SetActive(boo);
    }

    private void setJieSuanView(bool boo)
    {
        jiesuanView.gameObject.SetActive(boo);
    }


    void Start()
    {
        reset();
    }

    public void reset()
    {
        backView.GetComponent<BackView>().resetView();
        startView.GetComponent<RoomView>().reset();
        setStartView(false);
        mjView.GetComponent<CardView>().reset();
        setMjView(false);
        readyView.GetComponent<ReadyView>().reset();
        setReadyView(true);
        actionView.GetComponent<RoomAction>().reset();
        setActionView(false);
        jiesuanView.GetComponent<RoomJieSuan>().reset();
        //setJieSuanView(false);
        ///
    }

	
	// Update is called once per frame
	void Update () {
        if (startFlag == 1)
        {
            setReadyView(false);
            setStartView(true);
            setMjView(true);
            setActionView(true);
            mjView.GetComponent<CardView>().setStartHandMj();
            startView.GetComponent<RoomView>().initPlayerInfo();
            startFlag = 0;
        }

        if (MP_mj > 0)
        {
            mjView.GetComponent<CardView>().moPai(MP_mj, MP_mj);
            MP_mj = 0;
        }


        if (CP_positon > 0)
        {
            actionView.GetComponent<RoomAction>().showChuPai(CP_positon, CP_mj, isKeep);
            CP_positon = 0;
            CP_mj = 0;
            isKeep = false;
        }

        if (dir > 0)
        {
            startView.GetComponent<RoomView>().setCurrentPos(dir);
            //clearOutMj 如果为0 就是不做处理
            if (clearOutMj ==1)//pass
            {
                actionView.GetComponent<RoomAction>().hideall();//PASS 清除牌，添加出的牌
            }
            else if(clearOutMj == 2)//碰 直杠 
            {
                actionView.GetComponent<RoomAction>().clearOutMj();//PASS 清除牌，不添加出的牌
            }
            //else
            //{
            //    actionView.GetComponent<RoomAction>().addOutMj();//只通知添加打的牌，不清除打的牌
            //}
            clearOutMj = 0;
             dir = 0;
        }

        if (outMJid !=""&& outPos!="")
        {
            mjView.GetComponent<CardView>().addMjToOutBar(outPos, outMJid);
            outMJid = "";
            outPos = "";
        }

        if (resetFlag>0)
        {
            reset();
            resetFlag = 0;
        }
    }
}
