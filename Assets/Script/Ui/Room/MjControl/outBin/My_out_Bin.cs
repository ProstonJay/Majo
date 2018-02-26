﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class My_out_Bin : MonoBehaviour {

    public void setPic(string mjid)
    {
        Image icon = transform.Find("Image_pic").GetComponent<Image>();
        Sprite sp = Resources.Load("Texture/game/mj/xia_w/xiao_" + mjid, typeof(Sprite)) as Sprite;
        icon.sprite = sp;

        Debug.Log("出牌配音路径"+ MajooUtil.getChuPaiViocePath(mjid));
        AudioMgr.Instance.SoundPlay(MajooUtil.getChuPaiViocePath(mjid), 1, 0);
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
