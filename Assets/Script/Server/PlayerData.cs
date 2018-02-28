using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;//注意要用到这个dll
[ProtoContract]
public class PlayerData  {

    [ProtoMember(1)]
    private int userId;
    [ProtoMember(2)]
    private string playerName;
    [ProtoMember(3)]
    private int playerIcon;//头像
    [ProtoMember(4)]
    private int zhuangjia;//1 是  0 否
    [ProtoMember(5)]
    private int hupai;//0 否  1 自摸 2 吃胡
    [ProtoMember(6)]
    private List<int> handlist;//手牌
    [ProtoMember(7)]
    private List<Action> actionlist;//碰,吃
    [ProtoMember(8)]
    private List<int> outlist;//打出的牌
    [ProtoMember(9)]
    private int winGold;//输赢金币
    [ProtoMember(10)]
    private int zhiGang;//直杠次数
    [ProtoMember(11)]
    private int mingGang;//直杠次数
    [ProtoMember(12)]
    private int anGang;//直杠次数
    [ProtoMember(13)]
    private int huPaicCunt;//胡牌次数
    [ProtoMember(14)]
    private int fangPaoCunt;//放炮次数


    // public PlayerData(string pn, int zj, int hupai, List<int> hl, List<int> al, List<int> ol = null)
    //public PlayerData()
    //{
    //    Debug.Log("PlayerData 没有初始化数据11111111111111111111111111111111111111111111111111111111111111111111111111111111111");
    //}

    public PlayerData(int uid=0, string pn="", int pi=0, int zj=0, int hupai=0, List<int> hl = null, List<Action> al=null, List<int> ol = null, int wg = 0, int zg = 0, int mg = 0, int ag = 0, int hp = 0, int fp = 0)
    {
        this.userId = uid;
        this.playerName = pn;
        this.playerIcon = pi;
        this.zhuangjia = zj;
        this.hupai = hupai;
        this.handlist = hl;
        this.actionlist = al;
        this.outlist = ol;
        this.winGold = wg;
        this.zhiGang = zg;
        this.mingGang = mg;
        this.anGang = ag;
        this.huPaicCunt = hp;
        this.fangPaoCunt = fp;
    }

    public int getPlayerIcon()
    {
        return playerIcon;
    }
    public void setPlayerIcon(int value)
    {
        this.playerIcon = value;
    }

    public int getZhiGang()
    {
        return zhiGang;
    }
    public void setZhiGang(int value)
    {
        this.zhiGang = value;
    }
    public int getMingGang()
    {
        return mingGang;
    }
    public void setMingGang(int value)
    {
        this.mingGang = value;
    }
    public int getAnGang()
    {
        return anGang;
    }
    public void setAnGang(int value)
    {
        this.anGang = value;
    }
    public int getHuPaicCunt()
    {
        return huPaicCunt;
    }
    public void setHuPaicCunt(int value)
    {
        this.huPaicCunt = value;
    }
    public int getFangPaoCunt()
    {
        return fangPaoCunt;
    }
    public void setFangPaoCunt(int value)
    {
        this.fangPaoCunt = value;
    }

    public int getWinGold()
    {
        return winGold;
    }
    public void setWinGold(int wg)
    {
        this.winGold = wg;
    }

    public int getUserId()
    {
        return userId;
    }
    public void setUserId(int uid)
    {
        this.userId = uid;
    }

    public string getPlayerName()
    {
        return playerName;
    }

    public void setPlayerName(string pname)
    {
        this.playerName = pname;
    }

    public int getZhuangjia()
    {
        return zhuangjia;
    }
    public void setZhuangjia(int zj)
    {
        this.zhuangjia = zj;
    }

    public int getHupai()
    {
        return hupai;
    }
    public void setHupai(int hp)
    {
        this.hupai = hp;
    }


    public List<int> gethandlist()
    {
        return handlist;
    }


    public void sethandlist(List<int> list)
    {
        this.handlist = list;
    }

    public List<Action> getactionlist()
    {
        return actionlist;
    }


    public void setactionlist(List<Action> list)
    {
        this.actionlist = list;
    }

    public List<int> getoutlist()
    {
        return outlist;
    }


    public void setoutlist(List<int> list)
    {
        this.outlist = list;
    }
}
