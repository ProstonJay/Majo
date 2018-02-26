using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatRoomWindow : MonoBehaviour {

    public Button CreatBtn;
    public Button CloseBtn;

    public Toggle J2Toggle;
    public Toggle J3Toggle;
    public Toggle J4Toggle;

    public Toggle F12Toggle;
    public Toggle F24Toggle;
    public Toggle F36Toggle;

    private int MaxRound;
    private int MaxPoint;

    public Toggle zhuangjia;
    public Toggle qiangGang;
    public Toggle buChihu;

    public int zhuangjiaDouble;
    public int keQiangGang;
    public int buChiXiaoHu;

    // Use this for initialization
    void Start () {
        CreatBtn.onClick.AddListener(CreatPressed);
        CloseBtn.onClick.AddListener(ClosePressed);

        J2Toggle.onValueChanged.AddListener((bool value) => OnJuClick(J2Toggle, value));
        J3Toggle.onValueChanged.AddListener((bool value) => OnJuClick(J3Toggle, value));
        J4Toggle.onValueChanged.AddListener((bool value) => OnJuClick(J4Toggle, value));

        F12Toggle.onValueChanged.AddListener((bool value) => OnFanClick(F12Toggle, value));
        F24Toggle.onValueChanged.AddListener((bool value) => OnFanClick(F24Toggle, value));
        F36Toggle.onValueChanged.AddListener((bool value) => OnFanClick(F36Toggle, value));


        zhuangjia.onValueChanged.AddListener((bool value) => OnZhuangJiaClick(zhuangjia, value));
        qiangGang.onValueChanged.AddListener((bool value) => OnqiangGangClick(qiangGang, value));
        buChihu.onValueChanged.AddListener((bool value) => OnbuChihuClick(buChihu, value));
    }


    //庄家加倍
    public void OnZhuangJiaClick(Toggle toggle, bool ison)
    {
        AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
        if (ison)
        {
            this.zhuangjiaDouble = 1;
        }
        else
        {
            this.zhuangjiaDouble = 0;
        }
    }

    //可抢杠胡
    public void OnqiangGangClick(Toggle toggle, bool ison)
    {
        AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
        if (ison)
        {
            this.keQiangGang = 1;
        }
        else
        {
            this.keQiangGang = 0;
        }
    }

    //1番不能吃胡
    public void OnbuChihuClick(Toggle toggle, bool ison)
    {
        AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
        if (ison)
        {
            this.buChiXiaoHu = 1;
        }
        else
        {
            this.buChiXiaoHu = 0;
        }
    }

    //局数
    public void OnJuClick(Toggle toggle, bool ison)
    {
        if (ison)
        {
            AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
            switch (toggle.name)
            {
                case "2ju":
                    MaxRound = 2;
                    break;
                case "3ju":
                    MaxRound = 4;
                    break;
                case "4ju":
                    MaxRound = 8;
                    break;
                default:
                    break;
            }
        }
    }

    //番数
    public void OnFanClick(Toggle toggle, bool ison)
    {
        if (ison)
        {
            AudioMgr.Instance.SoundPlay("btnSelect", 1f, 2);
            switch (toggle.name)
            {
                case "12fan":
                    MaxPoint = 12;
                    break;
                case "24fan":
                    MaxPoint = 24;
                    break;
                case "36fan":
                    MaxPoint = 36;
                    break;
                default:
                    break;
            }
        }
    }

    //显示
    public void showWindow()
    {
        gameObject.SetActive(true);
        //每次显示,都初始化
        J2Toggle.isOn = true;
        F24Toggle.isOn = true;

        MaxRound = 4;
        MaxPoint = 24;

        zhuangjia.isOn = true;
        qiangGang.isOn = true;
        buChihu.isOn = false;

        zhuangjiaDouble = 1;
        keQiangGang = 1;
        buChiXiaoHu = 0;
    }

    void CreatPressed()
    {
        Debug.Log("开房需要" + MaxRound / 2 + "钻石");
        if (GameInfo.Instance.UserFK <0|| GameInfo.Instance.UserFK < MaxRound/2)
        {
            GameEvent.DoMsgEvent("房卡不足,请联系管理员!");
            return;
        }
        SocketModel CreatRoomRequset = new SocketModel();
        CreatRoomRequset.SetMainCmd(ProtocolMC.Main_Cmd_HALL);//消息的类型为Login
        CreatRoomRequset.SetSubCmd(ProtocolSC.Sub_Cmd_CreatRoomRqs);//动作为请求登录
        CreatRoomRequset.SetCommand(0);
        List<string> message = new List<string>();//这里存的是用户的ID;
        message.Add(GameInfo.Instance.UserID.ToString());
        message.Add(GameInfo.Instance.DeviceID);
        message.Add(MaxRound.ToString());
        message.Add(MaxPoint.ToString());
        message.Add(zhuangjiaDouble.ToString());
        message.Add(keQiangGang.ToString());
        message.Add(buChiXiaoHu.ToString());
        CreatRoomRequset.SetMessage(message);
        NettySocket.Instance.SendMsg(CreatRoomRequset);//发送这条消息给服务器
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
    void ClosePressed()
    {
        gameObject.SetActive(false);
        AudioMgr.Instance.SoundPlay("btnClose", 2f, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

