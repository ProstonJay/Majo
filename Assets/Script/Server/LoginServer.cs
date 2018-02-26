using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginServer  {

    public void handlerSubCmd(SocketModel socketModel)
    {
        switch (socketModel.GetMainCmd())
        {
            case ProtocolSC.Sub_Cmd_LoginRqs:
                loginRqs(socketModel);
            break;
        }
    }

    //返回登录结果
    public void loginRqs(SocketModel socketModel)
    {
        if (socketModel.GetCommand() == 10)
        {
      
            List <String> list = socketModel.GetMessage();
            foreach(string str in list)
            {
                Debug.Log("登录成功," + str);
            }
          
            GameInfo.Instance.UserID = int.Parse(list[0]);
            GameInfo.Instance.UserName = list[1];
            GameInfo.Instance.UserIcon = int.Parse(list[2]);
            GameInfo.Instance.UserFK = int.Parse(list[3]);
            if (GameInfo.Instance.UserIcon > 4)
            {
                GameInfo.Instance.sex = "boy";
            }
            else
            {
                GameInfo.Instance.sex = "girl";
            }

            Debug.Log("登录成功," + "用户房卡=" + GameInfo.Instance.UserFK);
            LoginView.str = "hall";
        }
        else
        {
            Debug.Log("登录失败");
            GameEvent.DoMsgEvent("帐号或密码错误!");
        }
    }
}
