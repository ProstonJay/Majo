using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallServer  {

        public void handlerSubCmd(SocketModel socketModel)
        {
            switch (socketModel.GetSubCmd())
            {
                case ProtocolSC.Sub_Cmd_CreatRoomRqs:
                    CreatRoomRqs(socketModel);
                    break;
                case ProtocolSC.Sub_Cmd_JoinRoomRqs:
                    JoinRoomRqs(socketModel);
                    break;
                case ProtocolSC.Sub_Cmd_HALL_CHANGENAME:
                    reNameRqs(socketModel);
                    break;
                case ProtocolSC.Sub_Cmd_GAME_CHANGEICON:
                    rePicRqs(socketModel);
                    break;
        }
        }

        //修改昵称
        public void reNameRqs(SocketModel socketModel)
        {
            GameInfo.Instance.UserName = socketModel.GetMessage()[1];
            GameEvent.DoMsgTipEvent("修改昵称成功!");
            GameEvent.DoStringEvent(GameInfo.Instance.UserName);
        }

        //修改头像
        public void rePicRqs(SocketModel socketModel)
        {
            GameInfo.Instance.UserIcon = int.Parse(socketModel.GetMessage()[1]);
            if (GameInfo.Instance.UserIcon > 4)
            {
                GameInfo.Instance.sex = "boy";
            }
            else
            {
                GameInfo.Instance.sex = "girl";
            }
            GameEvent.DoMsgTipEvent("修改头像成功!");
            GameEvent.DoPicEvent(GameInfo.Instance.UserIcon.ToString());
        }

       //加入开房结果
       public void JoinRoomRqs(SocketModel socketModel)
       {
        Debug.Log("收到加入房间返回结果"+ socketModel.GetCommand());
        if (socketModel.GetCommand() == 14)
        {
            GameInfo.Instance.roomId = socketModel.GetData()[0];
            GameInfo.Instance.positon = socketModel.GetData()[1];
            GameInfo.Instance.myGold = socketModel.GetData()[2];
            GameInfo.Instance.maxRound = socketModel.GetData()[3];
            GameInfo.Instance.maxPoint = socketModel.GetData()[4];

            List<String> list = socketModel.GetMessage();
            for(int i = 0; i <list.Count; i++)
            {
                if ((i+1) % 4== 0)
                {
                    GameInfo.Instance.addNewPlayer(int.Parse(list[i-3]), list[i-2], list[i-1], int.Parse(list[i]));
                    Debug.Log("有玩家="+i);
                }
            }
            Debug.Log("加入房间成功");
            GameEvent.DoSenceEvent ("game");
        }
        else {
            GameEvent.DoMsgEvent("加入房间失败!");
            Debug.Log("加入房间失败!");
        }
    }

        //返回开房结果
        public void CreatRoomRqs(SocketModel socketModel)
    {
        if (socketModel.GetCommand() == 11)
        {

            List<int> list = socketModel.GetData();
            GameInfo.Instance.roomId = list[0];
            GameInfo.Instance.positon = list[1];
            GameInfo.Instance.myGold = list[2];
            GameInfo.Instance.maxRound = list[3];
            GameInfo.Instance.maxPoint = list[4];
            GameEvent.DoSenceEvent("game");
            Debug.Log("开房成功,房间ID=" + GameInfo.Instance.roomId);
            Debug.Log("开房成功,我的位置=" + GameInfo.Instance.positon);
            Debug.Log("开房成功,我的金币=" + GameInfo.Instance.myGold);
            Debug.Log("开房成功,最大圈数=" + GameInfo.Instance.maxRound);
            Debug.Log("开房成功,最大番数=" + GameInfo.Instance.maxPoint);
        }
        else
        { 
            Debug.Log("开房失败 socketModel.GetComman="+ socketModel.GetCommand());
            GameEvent.DoMsgEvent("开房失败!");
        }
    }
}
