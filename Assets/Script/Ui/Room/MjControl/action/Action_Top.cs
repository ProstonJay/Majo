﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Top : MonoBehaviour {

    public List<GameObject> mj = new List<GameObject>();

    //暗杠
    private int angangMj;
    //明杠
    private int minggangMj;
    //碰牌
    private int pengMj;
    //直杠
    private int zhiGangMj;

    void Awake()
    {
        RoomEvent.AnGangEvent += AnGangEvent;
        RoomEvent.MingGangEvent += MingGangEvent;
        RoomEvent.PengPaiEvent += PengPaiEvent;
        RoomEvent.ZhiGangEvent += ZhiGangEvent;
    }

    //碰牌
    private void PengPaiEvent(string pos, int mj)
    {
        if (pos == "top")
        {
            this.pengMj = mj;
        }

    }

    //直杠
    private void ZhiGangEvent(string pos, int mj, int fangGangPos)
    {
        if (pos == "top")
        {
            this.zhiGangMj = mj;
        }

    }

    //明杠
    private void MingGangEvent(string pos, int mj)
    {
        if (pos == "top")
        {
            this.minggangMj = mj;
        }

    }

    //暗杠
    private void AnGangEvent(string pos, int mj)
    {
        if (pos == "top")
        {
            this.angangMj = mj;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //暗杠
        if (angangMj > 0)
        {
            addAnGang(angangMj);
            angangMj = 0;
        }
        //明杠
        if (minggangMj > 0)
        {
            addMingGang(minggangMj);
            minggangMj = 0;
        }
        //碰牌
        if (pengMj > 0)
        {
            addPeng(pengMj);
            pengMj = 0;
        }
        //直杠
        if (zhiGangMj > 0)
        {
            addZhiGang(zhiGangMj);
            zhiGangMj = 0;
        }
    }

    //直杠
    public void addZhiGang(int mjid)
    {
        int acunt = this.mj.Count;
        //int acunt = GameInfo.Instance.myAcionList.Count;
        int startX = (acunt - 1) * -90 + (acunt - 1) * -30;
        GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_Peng_my")) as GameObject;
        mjCard.transform.SetParent(this.transform, false);
        mjCard.GetComponent<Action_Peng>().setPic("xia_w/xiao_" + mjid.ToString(), 2);
        mjCard.transform.localPosition = new Vector3(startX,0, 0);
        mjCard.transform.localScale = new Vector3(0.8f, 0.8f, 0);
        mjCard.name = "zhiGang";
        mj.Add(mjCard);
        Debug.Log("直杠配音路径" + MajooUtil.getZhiGangViocePath());
        AudioMgr.Instance.SoundPlay(MajooUtil.getZhiGangViocePath(), 1, 0);
    }

    //碰牌
    public void addPeng(int mjid)
    {
        int acunt = this.mj.Count;
        //int acunt = GameInfo.Instance.myAcionList.Count;
        int startX = (acunt - 1) * -90 + (acunt - 1) * -30;
        GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_Peng_my")) as GameObject;
        mjCard.transform.SetParent(this.transform, false);
        mjCard.GetComponent<Action_Peng>().setPic("xia_w/xiao_" + mjid.ToString(),1);
        mjCard.GetComponent<Action_Peng>().setMjid(mjid);
        mjCard.transform.localPosition = new Vector3(startX,0, 0);
        mjCard.transform.localScale = new Vector3(0.8f, 0.8f, 0);
        mjCard.name = "peng";
        mj.Add(mjCard);
        Debug.Log("碰牌配音路径" + MajooUtil.getPengPaiViocePath());
        AudioMgr.Instance.SoundPlay(MajooUtil.getPengPaiViocePath(), 1, 0);
    }


    //明杠
    public void addMingGang(int mjid)
    {
        if (mj.Count > 0)
        {
            foreach (GameObject item in mj)
            {
                if (item.name == "peng")
                {
                    int pengMJ = item.GetComponent<Action_Peng>().getMjid();
                    if (pengMJ == mjid)
                    {
                        item.GetComponent<Action_Peng>().changToMingGang();
                        break;
                    }
                }
            }
        }
        Debug.Log("明杠配音路径" + MajooUtil.getMingGangViocePath());
        AudioMgr.Instance.SoundPlay(MajooUtil.getMingGangViocePath(), 1, 0);
    }

    //暗杠
    public void addAnGang(int mjid)
    {
        int acunt = this.mj.Count;
        //int acunt = GameInfo.Instance.topAcionList.Count;
        int startX = (acunt - 1) * -90 + (acunt - 1) *-30;
        GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_AnGang_my")) as GameObject;
        mjCard.transform.SetParent(this.transform, false);
        mjCard.GetComponent<Action_AnGang>().setPic("xia_w/xiao_" + mjid.ToString());
        mjCard.transform.localPosition = new Vector3(startX, 0, 0);
        mjCard.transform.localScale = new Vector3(0.8f, 0.8f, 0);
        mjCard.name = "gang";
        mj.Add(mjCard);

        Debug.Log("暗杠配音路径" + MajooUtil.getAnGangViocePath());
        AudioMgr.Instance.SoundPlay(MajooUtil.getAnGangViocePath(), 1, 0);
    }


    public void reset()
    {
        if (mj.Count > 0)
        {
            foreach (GameObject item in mj)
            {
                Destroy(item);
            }
        }
        mj.Clear();
    }
    // Use this for initialization
    void Start()
    {
        reset();
        //addAnGang(35);
        //addZhiGang(11);
        //addMingGang(32);
        //addZhiGang(34);
    }
}
