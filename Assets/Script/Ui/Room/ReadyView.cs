using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyView : MonoBehaviour {

    public Image img_my;
    public Image img_right;
    public Image img_top;
    public Image img_left;

    public GameObject obj_my;
    public GameObject obj_right;
    public GameObject obj_top;
    public GameObject obj_left;

    private int newplayerPos = 0;

    void Awake()
    {
        GameEvent.PlayerEnterRoomEvent += PlayerEnterRoom;
    }

    public void reset()
    {
        img_my.gameObject.SetActive(false);
        img_right.gameObject.SetActive(false);
        img_top.gameObject.SetActive(false);
        img_left.gameObject.SetActive(false);
    }

    private void PlayerEnterRoom(int pos ,string name)
    {
        Debug.Log("wanjia的位置=" + pos+ "wanjia的名字=" + name);
        newplayerPos = pos;
    }

    private void addNewPlayer()
    {
        switch (TryGetLocPos(GameInfo.Instance.positon, newplayerPos))
        {
            case "right":
                obj_right.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.rightIcon)); ;
                obj_right.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.rightName);
                img_right.gameObject.SetActive(true);
                break;
            case "top":
                obj_top.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.topIcon));
                obj_top.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.topName);
                img_top.gameObject.SetActive(true);
                break;
            case "left":
                obj_left.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.leftIcon));
                obj_left.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.leftName);
                img_left.gameObject.SetActive(true);
                break;
        }
    }

    private string TryGetLocPos(int myPos,int othPos)
    {
        string str = "";
        switch (myPos)
        {
            case 1:
                if (othPos == 2){str = "right"; }else if (othPos == 3){str = "top"; }else if (othPos == 4){str = "left";}
                break;
            case 2:
                if (othPos == 1) { str = "left"; } else if (othPos == 3) { str = "right"; } else if (othPos == 4) { str = "top"; }
                break;
            case 3:
                if (othPos == 1) { str = "top"; } else if (othPos == 2) { str = "left"; } else if (othPos == 4) { str = "right"; }
                break;
            case 4:
                if (othPos == 1) { str = "right"; } else if (othPos == 2) { str = "top"; } else if (othPos == 3) { str = "left"; }
                break;
        }
        return str;
    }

        // Use this for initialization
    void Start () {
     
        //刷新自己座位信息
        obj_my.GetComponent<headInfo>().SetHeadIcon(GameInfo.Instance.UserIcon);
        obj_my.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.UserName);
        img_my.gameObject.SetActive(true);
        //再刷新其他座位信息
        if (GameInfo.Instance.rightPositon > 0)
        {
            obj_right.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.rightIcon));
            obj_right.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.rightName);
            img_right.gameObject.SetActive(true);
        }
        if (GameInfo.Instance.topPostion > 0)
        {
            obj_top.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.topIcon));
            obj_top.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.topName);
            img_top.gameObject.SetActive(true);
        }
        if (GameInfo.Instance.leftPostion > 0)
        {
            obj_left.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.leftIcon));
            obj_left.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.leftName);
            img_left.gameObject.SetActive(true);
        }
        //这里延迟通知服务器准备好了
        Invoke("setReady", 2);
    }

    private void setReady()
    {
        SocketModel ReadyRequset = new SocketModel();
        ReadyRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        ReadyRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_READY);
        ReadyRequset.SetCommand(0);
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        ReadyRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(ReadyRequset);//发送这条消息给服务器
    }


    // Update is called once per frame
    void Update () {
        if (newplayerPos > 0)
        {
            addNewPlayer();
            newplayerPos = 0;
        }
	}
}
