﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOutBar : MonoBehaviour {

    public List<GameObject> mj = new List<GameObject>();

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
    void Start () {
        reset();
        ///////////////
        //List<int> list1 = new List<int>();
        //list1.Add(14); list1.Add(14); list1.Add(15); list1.Add(16); list1.Add(17); list1.Add(24); list1.Add(24);
        //list1.Add(34); list1.Add(34); list1.Add(34); list1.Add(38); list1.Add(44); list1.Add(44); list1.Add(47);
        //list1.Add(14); list1.Add(14); list1.Add(15);
        //for (int i = 0; i < list1.Count; i++)
        //{
        //    string mj = list1[i].ToString();
        //    //最后显示  
        //    chupai(mj);
        //}
    }

    // Update is called once per frame
    void Update () {
     
    }

    public void chupai(string mjid)
    {

        GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_my_out_card")) as GameObject;
        mjCard.transform.SetParent(this.transform, false);
        mjCard.GetComponent<Mj_my_out>().setPic(mjid);
        mj.Add(mjCard);
        if (mj.Count <= 6)
        {
            mjCard.transform.localPosition = new Vector3(mj.Count * 44, 0, 0);
            mjCard.transform.SetSiblingIndex(0);
        }
        else if (mj.Count > 6&& mj.Count <= 12)
        {
            mjCard.transform.localPosition = new Vector3((mj.Count - 6) * 44, -48, 0);
            mjCard.transform.SetSiblingIndex(6);
        }
        else if (mj.Count > 12 && mj.Count <= 18) {
            mjCard.transform.localPosition = new Vector3((mj.Count - 12) * 44, -96, 0);
            mjCard.transform.SetSiblingIndex(12);
        }
        else if (mj.Count > 18 && mj.Count <= 24)
        {
            mjCard.transform.localPosition = new Vector3((mj.Count - 18) * 44, -144, 0);
            mjCard.transform.SetSiblingIndex(18);
        }
       

    }
}