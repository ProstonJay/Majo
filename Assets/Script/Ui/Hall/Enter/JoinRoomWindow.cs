using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoinRoomWindow : MonoBehaviour
{

    public Button JoinBtn;
    public Button CloseBtn;
    public Button DeleteBtn;

    public Button numbBnt0;
    public Button numbBnt1;
    public Button numbBnt2;
    public Button numbBnt3;
    public Button numbBnt4;
    public Button numbBnt5;
    public Button numbBnt6;
    public Button numbBnt7;
    public Button numbBnt8;
    public Button numbBnt9;

    public Text RoomIdText;

    // Use this for initialization
    void Start()
    {
        JoinBtn.onClick.AddListener(JoinPressed);
        CloseBtn.onClick.AddListener(ClosePressed);
        DeleteBtn.onClick.AddListener(DeletePressed);

        numbBnt0.onClick.AddListener(numb0Pressed);
        numbBnt1.onClick.AddListener(numb1Pressed);
        numbBnt2.onClick.AddListener(numb2Pressed);
        numbBnt3.onClick.AddListener(numb3Pressed);
        numbBnt4.onClick.AddListener(numb4ressed);
        numbBnt5.onClick.AddListener(numb5Pressed);
        numbBnt6.onClick.AddListener(numb6Pressed);
        numbBnt7.onClick.AddListener(numb7Pressed);
        numbBnt8.onClick.AddListener(numb8Pressed);
        numbBnt9.onClick.AddListener(numb9Pressed);

    }

    public bool isLg()
    {
        bool boo = false;     
        if (RoomIdText.text != "")
        {
            int num = int.Parse(RoomIdText.text);
            if (num < 100000)
            {
                boo = true;
            }
            Debug.Log(num);
        }
        else
        {
            boo = true;
        }
        return boo;
    }

    void numb0Pressed()
    {
    
        if (isLg() == true)
        {
            RoomIdText.text = RoomIdText.text + 0.ToString();
        }
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }
    void numb1Pressed()
    {
        if (isLg() == true)
        {
            RoomIdText.text = RoomIdText.text + 1.ToString();
        }
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }
    void numb2Pressed()
    {
        if (isLg() == true)
        {
            RoomIdText.text = RoomIdText.text + 2.ToString();
        }
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }
    void numb3Pressed()
    {
        if (isLg() == true)
        {
            RoomIdText.text = RoomIdText.text + 3.ToString();
        }
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }
    void numb4ressed()
    {
        if (isLg() == true)
        {
            RoomIdText.text = RoomIdText.text + 4.ToString();
        }
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }
    void numb5Pressed()
    {
        if (isLg() == true)
        {
            RoomIdText.text = RoomIdText.text + 5.ToString();
        }
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }
    void numb6Pressed()
    {
        if (isLg() == true)
        {
            RoomIdText.text = RoomIdText.text + 6.ToString();
        }
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }
    void numb7Pressed()
    {
        if (isLg() == true)
        {
            RoomIdText.text = RoomIdText.text + 7.ToString();
        }
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }
    void numb8Pressed()
    {
        if (isLg() == true)
        {
            RoomIdText.text = RoomIdText.text + 8.ToString();
        }
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }
    void numb9Pressed()
    {
        if (isLg() == true)
        {
            RoomIdText.text = RoomIdText.text + 9.ToString();
        }
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }

    void DeletePressed()
    {
        RoomIdText.text = "";
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
    }


    void JoinPressed()
    {
        AudioMgr.Instance.SoundPlay("anniu", 2f, 2);
        if (RoomIdText.text == "")
        {
            GameEvent.DoMsgEvent("房间号不能为空");
            return;
        }
        Debug.Log("加入的房间号=" + int.Parse(RoomIdText.text));
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
        SocketModel JoinRoomRequset = new SocketModel();
        JoinRoomRequset.SetMainCmd(ProtocolMC.Main_Cmd_HALL);
        JoinRoomRequset.SetSubCmd(ProtocolSC.Sub_Cmd_JoinRoomRqs);
        JoinRoomRequset.SetCommand(0);
        List<string> message = new List<string>();
        message.Add(RoomIdText.text);
        message.Add(GameInfo.Instance.UserID.ToString());
        JoinRoomRequset.SetMessage(message);
        NettySocket.Instance.SendMsg(JoinRoomRequset);//发送这条消息给服务器

    }

    void ClosePressed()
    {
        gameObject.SetActive(false);
        RoomIdText.text = "";
        AudioMgr.Instance.SoundPlay("btnClose", 2f, 2);
    }

    public void showWindow()
    {
        gameObject.SetActive(true);
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
