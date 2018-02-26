﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopControlBar : MonoBehaviour {

    public List<GameObject> mjList = new List<GameObject>();

    //暗杠
    private int angangMj;
    //明杠
    private int minggangMj;
    //碰牌
    private int pengMj;
    //直杠
    private int zhiGangMj;
    //吃胡
    private int chihuMj;
    private string chihuPos;
    //自摸
    private int zimoMj;
    private string zimoPos;
    //流局
    private int liujuFlag;

    private List<int> handList = new List<int>();

    void Awake()
    {
        RoomEvent.AnGangEvent += AnGangEvent;
        RoomEvent.MingGangEvent += MingGangEvent;
        RoomEvent.PengPaiEvent += PengPaiEvent;
        RoomEvent.ZhiGangEvent += ZhiGangEvent;
        RoomEvent.ChiHuEvent += ChiHuEvent;
        RoomEvent.ZiMoEvent += ZiMoEvent;
        RoomEvent.LiuJuEvent += LiuJuEvent;
    }

    //流局
    private void LiuJuEvent(List<PlayerData> list)
    {
        liujuFlag = 1;
        Debug.Log("list 数量 = " + list.Count);
        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log("top 找位置 = " + list[i].getUserId());
            //Debug.Log("left leftPostion =" + GameInfo.Instance.leftPostion + "读取的位置 i=" + i + "取的位置 list[i].getUserId()=" + list[i].getUserId());
            if (list[i].getUserId() == GameInfo.Instance.topPostion)
            {
                //Debug.Log("left 自摸 手牌数量=" + list[i].gethandlist().Count);
                handList = list[i].gethandlist();
                Debug.Log("top 手牌 数量 = " + handList.Count);
                handList.Reverse();
            }
        }

    }

    //自摸
    private void ZiMoEvent(string pos, int mj, List<PlayerData> list)
    {
        if (mj > 0)
        {
            this.zimoMj = mj;
            this.zimoPos = pos;
            for (int i = 0; i < list.Count; i++)
            {
                //Debug.Log("top topPostion =" + GameInfo.Instance.topPostion + "读取的位置 i=" + i + "取的位置 list[i].getUserId()=" + list[i].getUserId());
                if (list[i].getUserId() == GameInfo.Instance.topPostion)
                {
                   // Debug.Log("top 自摸 手牌数量=" + list[i].gethandlist().Count);
                    handList = list[i].gethandlist();
                   
                }
            }
        }

    }

    //吃胡
    private void ChiHuEvent(string pos, int mj, int fangpao, List<PlayerData> list)
    {
        if (mj > 0)
        {
            this.chihuMj = mj;
            this.chihuPos = pos;
            for (int i = 0; i < list.Count; i++)
            {
                //Debug.Log("top topPostion =" + GameInfo.Instance.topPostion + "读取的位置 i=" + i + "取的位置 list[i].getUserId()=" + list[i].getUserId());
                if (list[i].getUserId() == GameInfo.Instance.topPostion)
                {
                    //Debug.Log("top 吃胡 手牌数量=" + list[i].gethandlist().Count);
                    handList = list[i].gethandlist();
                }
            }
        }

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
        //流局
        if (liujuFlag > 0)
        {
            liuJu();
            liujuFlag = 0;
        }
        //自摸
        if (zimoMj > 0)
        {
            ziMo(zimoMj, zimoPos);
            zimoMj = 0;
            zimoPos = "";
        }
        //吃胡
        if (chihuMj > 0)
        {
            chiHu(chihuMj, chihuPos);
            chihuMj = 0;
            chihuPos = "";
        }
        //暗杠
        if (angangMj > 0)
        {
            anGang(angangMj);
            angangMj = 0;
        }
        //明杠
        if (minggangMj > 0)
        {
            mingGang(minggangMj);
            minggangMj = 0;
        }
        //碰牌
        if (pengMj > 0)
        {
            pengPai(pengMj);
            pengMj = 0;
        }
        //直杠
        if (zhiGangMj > 0)
        {
            zhiGang(zhiGangMj);
            zhiGangMj = 0;
        }

    }

    //流局
    public void liuJu()
    {
        if (this.handList == null || this.handList.Count == 0)
        {
            return;
        }
        StartCoroutine(ShowA());
    }

    //自摸
    public void ziMo(int mj, string pos)
    {
        if (this.handList == null || this.handList.Count == 0)
        {
            return;
        }
        StartCoroutine(ShowA());
    }

    //吃胡
    public void chiHu(int mj, string pos)
    {
        if (this.handList == null || this.handList.Count == 0)
        {
            return;
        }
        StartCoroutine(ShowA());
    }

    private IEnumerator ShowA()
    {
        yield return new WaitForSeconds(3);
        //要把牌推倒,如果是当前位置胡了,要把胡的牌放到摸牌的地方
        this.reset();
        for (int i = 0; i < handList.Count; i++)
        {
            GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_top_out_card")) as GameObject;
            mjCard.transform.localPosition = new Vector3(i * 47 - 7, -80, 0);
            mjCard.transform.SetParent(this.transform, false);
            mjCard.GetComponent<Mj_top_out>().setPic(handList[i].ToString());
            mjCard.transform.localScale = new Vector3(1f, 1f, 0);
            mjCard.transform.SetSiblingIndex(0);
            mjCard.name = i.ToString();
            mjList.Add(mjCard);
        }
        handList.Clear();
    }

    //明杠
    private void mingGang(int mj)
    {
        //deletMj(1);
    }

    //暗杠
    private void anGang(int mj)
    {
        deletMj(3);
    }

    //碰牌
    private void pengPai(int mj)
    {
        deletMj(3);
    }

    //直杠
    private void zhiGang(int mj)
    {
        deletMj(3);
    }

    public void reset()
    {
        if (mjList.Count > 0)
        {
            foreach (GameObject item in mjList)
            {
                Destroy(item);
            }
        }
        mjList.Clear();
    }

    // Use this for initialization
    void Start () {
        //for (int i = 0; i < 1; i++)
        //{
        //    GameObject mjCard = Instantiate(Resources.Load("Prefab/gameobject_mj_top")) as GameObject;
        //    mjCard.transform.localPosition = new Vector3(i * 46, 0, 0);
        //    mjCard.transform.SetParent(this.transform, false);
        //    mjCard.transform.SetSiblingIndex(0);
        //    mjCard.name = i.ToString();
        //    mjList.Add(mjCard);
        //}
        //for (int i = 0; i < 1; i++)
        //{
        //    GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_top_out_card")) as GameObject;
        //    mjCard.transform.localPosition = new Vector3(i * 47-7, -80, 0);
        //    mjCard.transform.SetParent(this.transform, false);
        //    mjCard.GetComponent<Mj_top_out>().setPic("23");
        //    mjCard.transform.localScale = new Vector3(1f, 1f, 0);
        //    mjCard.transform.SetSiblingIndex(0);
        //    mjCard.name = i.ToString();
        //    mjList.Add(mjCard);
        //}
    }

    ///初始化发牌
    /// </summary>
    public void initHandMj()
    {
        if (GameInfo.Instance.topPostion > 0 && GameInfo.Instance.topName != "")
        {
            for (int i = 0; i < 13; i++)
            {  
                GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_mj_top")) as GameObject;
                mjCard.transform.localPosition = new Vector3(i * 46, 0, 0);
                mjCard.transform.SetParent(this.transform, false);
                mjCard.name = i.ToString();
                mjList.Add(mjCard);
            }
        }
    }
    /// <summary>
    /// 出牌
    /// </summary>
    /// <param name="CardName"></param>
      //删除牌
    public void deletMj(int value)
    {
        for (int i = 0; i < value; i++)
        {
            Destroy(mjList[0]);
            mjList.Remove(mjList[0]);
        }
        ResetPostion();
    }

    //重置牌位置
    private void ResetPostion()
        {
            int j = 0;
            foreach (GameObject card in mjList)
            {
                card.transform.localPosition = new Vector3(j * 46, 0, 0);
            j++;
            }
        }
}