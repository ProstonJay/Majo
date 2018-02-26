using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

    public static GameInfo Instance = null;

    /// <summary>
    /// 全局用户数据
    /// </summary>
    public string DeviceID; //设备唯一ID
    public int UserID; //游戏唯一ID
    public string UserName; //昵称
    public int UserIcon; //头像
    public int UserFK; //房卡
    public string sex;// boy girl

    /// <summary>
    /// 游戏房间数据
    /// </summary>
    public int roomId; //房间号
    public int maxRound; //最大圈数
    public int maxPoint; //最大番数
    public int zhuangjia; //当前庄家的位置(1-4)
    public int round; //当前圈数
    public int mjLeft; //剩余牌数量

    /// <summary>
    /// 游戏内个人数据
    /// </summary>
    public int positon ; //位置(1234--东南西北)
    public int myGold;//金币数量
    public List<int> myHandMj = new List<int>();//从大到小
    public List<Action> myAcionList = new List<Action>();//操作牌型的列表
    //每回合临时缓存
    public List<Action> pengList = new List<Action>();//碰牌牌型的列表
    public List<Action> gangList = new List<Action>();//杠牌牌型的列表

    /// <summary>
    /// 右边玩家数据
    /// </summary>
    public int rightPositon;
    public string rightName;
    public string rightIcon;
    public int rightGlod;
    public List<Action> rightAcionList = new List<Action>();//操作牌型的列表

    /// <summary>
    /// 对家玩家数据
    /// </summary>
    public int topPostion;
    public string topName;
    public string topIcon;
    public int topGold;
    public List<Action> topAcionList = new List<Action>();//操作牌型的列表

    /// <summary>
    /// 左边玩家数据
    /// </summary>
    public int leftPostion;
    public string leftName;
    public string leftIcon;
    public int leftGold;
    public List<Action> leftAcionList = new List<Action>();//操作牌型的列表

    //是否在操作回合,只有在自己摸牌的时候才能操作
    public Boolean PlayFlag;

    //小结算重置
    public void reset()
    {
        mjLeft = 0;
        myHandMj.Clear();
        PlayFlag = false;
        zhuangjia = 0;
        pengList.Clear();
        gangList.Clear();
        myAcionList.Clear();
        rightAcionList.Clear();
        topAcionList.Clear();
        leftAcionList.Clear();
        myHandMj.Clear();
    }

    //大结算重置
    public void resetAll()
    {
        roomId = 0;
        maxRound = 0;
        maxPoint = 0;
        round = 0;
        positon = 0;
        myGold = 0;
        //
        rightPositon = 0;
        rightName = "";
        rightIcon = "";
        rightGlod = 0;
        //
        topPostion = 0;
        topName = "";
        topIcon = "";
        topGold = 0;
        //
        leftPostion = 0;
        leftName = "";
        leftIcon = "";
        leftGold = 0;
        //
        reset();
    }


    //新玩家进入房间
    public void addNewPlayer(int othpos, string pname,string icon,int glod)
    {
        switch (TryGetLocPos(positon, othpos))
        {
            case "right":
                rightPositon = othpos;
                rightName = pname;
                rightIcon = icon;
                rightGlod = glod;
                break;
            case "top":
                topPostion = othpos;
                topName = pname;
                topIcon = icon;
                topGold = glod;
                break;
            case "left":
                leftPostion = othpos;
                leftName = pname;
                leftIcon = icon;
                leftGold = glod;
                break;
        }
    }

    //玩家碰牌操作
    public void addPengPai(int pos, Action act)
    {
        string sendPos = "";
        if (positon == pos)
        {
            myAcionList.Add(act);
            sendPos = "bot";
        }
        else
        {
            switch (TryGetLocPos(positon, pos))
            {
                case "right":
                    rightAcionList.Add(act);
                    sendPos = "right";
                    break;
                case "top":
                    topAcionList.Add(act);
                    sendPos = "top";
                    break;
                case "left":
                    leftAcionList.Add(act);
                    sendPos = "left";
                    break;
            }
        }
        RoomEvent.DoPengPai(sendPos, act.getValue());
    }

    //玩家明杠操作
    public void addMingGang(int pos,int mj)
    {
        string sendPos = "";
        List<Action> alist = null;
        if (positon == pos)
        {
            alist = myAcionList;
            sendPos = "bot";
        }
        else
        {
            switch (TryGetLocPos(positon, pos))
            {
                case "right":
                    alist = rightAcionList;
                    sendPos = "right";
                    break;
                case "top":
                    alist = topAcionList;
                    sendPos = "top";
                    break;
                case "left":
                    alist = leftAcionList;
                    sendPos = "left";
                    break;
            }
        }

        //找到明杠的碰牌,修改为明杠类型
        for (int i = 0; i < alist.Count; i++)
        {
            if (alist[i].getActionType() == CardView.CHI_PENG_碰牌)
            {
                int pengMj = alist[i].getValue();
                if (pengMj == mj)
                {
                    alist[i].setActionType(CardView.CHI_MINGGANG_明杠);
                    alist[i].getActionData().Add(mj);
                    break;
                }
            }
        }
        //
        RoomEvent.DoMingGang(sendPos, mj);
    }
       //玩家暗杠操作
    public void addAnGang(int pos ,Action act)
    {
        string sendPos = "";
        if (positon == pos)
        {
            myAcionList.Add(act);
            sendPos = "bot";
        }
        else
        {
            switch (TryGetLocPos(positon, pos))
            {
                case "right":
                    rightAcionList.Add(act);
                    sendPos = "right";
                    break;
                case "top":
                    topAcionList.Add(act);
                    sendPos = "top";
                    break;
                case "left":
                    leftAcionList.Add(act);
                    sendPos = "left";
                    break;
            }
        }
        RoomEvent.DoAnGang(sendPos, act.getValue());
    }

    //玩家直杠
    public void addZhiGang(int pos, Action act, int fangGangPos)
    {
        string sendPos = "";
        if (positon == pos)
        {
            myAcionList.Add(act);
            sendPos = "bot";
            //
            this.myGold += 200;
        }
        else
        {
            switch (TryGetLocPos(positon, pos))
            {
                case "right":
                    rightAcionList.Add(act);
                    sendPos = "right";
                    break;
                case "top":
                    topAcionList.Add(act);
                    sendPos = "top";
                    break;
                case "left":
                    leftAcionList.Add(act);
                    sendPos = "left";
                    break;
            }
        }
        RoomEvent.DoZhiGang(sendPos, act.getValue(), fangGangPos);
    }

    public string TryGetLocPos(int myPos, int othPos)
    {
        string str = "";
        switch (myPos)
        {
            case 1:
                if (othPos == 2) { str = "right"; } else if (othPos == 3) { str = "top"; } else if (othPos == 4) { str = "left"; }
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

    //摸牌放入手牌
    public void putMjtoHandList(int mj)
    {
        myHandMj.Add(mj);
    }

    //出牌,重新排序手牌
    public void sortHandList(int mj)
    {
        for(int i = 0; i < myHandMj.Count; i++)
        {
            if (myHandMj[i] == mj)
            {
                myHandMj.RemoveAt(i);
            }
        }
        myHandMj.Sort((x, y) => -x.CompareTo(y));
    }

    //删除
    public void deleteArrInList(int mj,int count)
    {
        int repeat = 0;
        for (int i = 0; i < myHandMj.Count; i++)
        {
            if (myHandMj[i].Equals( mj))
            {
                myHandMj.RemoveAt(i);
                repeat++;
                i--;
            }
            if (repeat == count)
            {
                break;
            }
        }
        myHandMj.Sort((x, y) => -x.CompareTo(y));
    }

    //删除一个数组里的指定数量的数字
    public  void deleteIntWithNum(List<int> mjList, int value, int repeat)
    {
        //System.out.println("需要删除的数字"+value);
        //System.out.println("传入的数组="+mjList);
        //System.out.println("重复次数="+repeat);
        int count = 1;


        for (int i = 0; i < mjList.Count; i++)
        {
            if (mjList[i]==value)
            {
                //System.out.println("找到"+count+"个相同的="+i);
                mjList.RemoveAt(i);
                count++;
                i--;
            }
            if (count > repeat)
            {
                break;
            }

        }
        //System.out.println("删除完后的数组="+mjList);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.transform.gameObject);
        }
        else
        {
            if (Instance != this)
            {
                Destroy(transform.gameObject);
            }
        }
    }

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
