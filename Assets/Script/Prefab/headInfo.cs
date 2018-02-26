using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class headInfo : MonoBehaviour {

    public Image img_icon;
    public Text txt_name;
    public Image img_fangzhu;
    public Image img_zhuangjia;
    public Image img_gold;
    public Text txt_gold;

    // Use this for initialization
    void Start () {
        //img_fangzhu.gameObject.SetActive(false);
        //img_zhuangjia.gameObject.SetActive(false);
        //img_gold.gameObject.SetActive(false);
        //txt_gold.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void SetHeadIcon(int iconId)
    {
        Sprite sp = Resources.Load("Texture/Hall/head/head_"+ iconId.ToString(), typeof(Sprite)) as Sprite;
        img_icon.sprite = sp;
    }

    public void SetPlayerName(string str)
    {
        txt_name.text = str;
    }

    public void setPlayerGold(int value)
    {
        txt_gold.text = value.ToString();
    }

    public void setHost(int value)
    {
        if (value == 1)
        {
            img_fangzhu.gameObject.SetActive(true);
        }
        else
        {
            img_fangzhu.gameObject.SetActive(false);
        }
    }

    public void setZj(int value)
    {
        Debug.Log("设置庄家 传入位置="+ value+"当前庄家="+ GameInfo.Instance.zhuangjia);
        if (value == GameInfo.Instance.zhuangjia)
        {
            img_zhuangjia.transform.gameObject.SetActive(true);
        }
        else
        {
            img_zhuangjia.transform.gameObject.SetActive(false);
        }
    }
}
