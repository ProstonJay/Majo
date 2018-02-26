using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XiaoJieSuan : MonoBehaviour {

    public GameObject myHead;
    public GameObject rightHead;
    public GameObject topHead;
    public GameObject leftHead;

    public Button nextGameBtn;

    public GameObject myNum;
    public GameObject rightNum;
    public GameObject topNum;
    public GameObject leftNum;

    //结算
    private List<PlayerData> plaerlist;

    public void showJiesuan(List<PlayerData> plaerlist )
    {
        for (int i=0;i< plaerlist.Count; i++)
        {
            PlayerData pd = plaerlist[i];
            if (pd.getUserId() == GameInfo.Instance.positon)
            {
                myHead.GetComponent<headInfo>().SetHeadIcon(GameInfo.Instance.UserIcon);
                myHead.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.UserName);
                myNum.GetComponent<Number>().showNumber(pd.getWinGold(),true);
                myHead.GetComponent<headInfo>().setZj(pd.getUserId());
                Debug.Log("myHead 金币=" + pd.getWinGold());
            }
            else
            {
                string pos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon,pd.getUserId());
                Debug.Log("位置="+ pos + "金币=" + pd.getWinGold());
                switch (pos)
                {
                    case "right":
                            rightHead.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.rightIcon));
                            rightHead.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.rightName);
                            rightHead.GetComponent<headInfo>().setZj(pd.getUserId());
                            rightNum.GetComponent<Number>().showNumber(pd.getWinGold(), true);
                        break;
                    case "top":
                            topHead.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.topIcon));
                            topHead.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.topName);
                            topHead.GetComponent<headInfo>().setZj(pd.getUserId());
                            topNum.GetComponent<Number>().showNumber(pd.getWinGold(), true);
                        break;
                    case "left":
                            leftHead.GetComponent<headInfo>().SetHeadIcon(int.Parse(GameInfo.Instance.leftIcon));
                            leftHead.GetComponent<headInfo>().SetPlayerName(GameInfo.Instance.leftName);
                            leftHead.GetComponent<headInfo>().setZj(pd.getUserId());
                            leftNum.GetComponent<Number>().showNumber(pd.getWinGold(), true);
                        break;
                }

            }
          
        }
    }

    // Use this for initialization
    void Start () {
        nextGameBtn.onClick.AddListener(nextGamePressed);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nextGamePressed()
    {
        GameEvent.DoReSetRoom();
        setReady();
        GameInfo.Instance.reset();
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
}
