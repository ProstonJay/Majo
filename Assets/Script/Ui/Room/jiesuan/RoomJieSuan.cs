using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomJieSuan : MonoBehaviour {

    //小结算
    private List<PlayerData> plaerlist;

    //总结算
    private List<PlayerData> zongPlaerlist;

    public GameObject xiaojiesuan;
    public GameObject dajiesuan;

    void Awake()
    {
        GameEvent.JieSuanEvent += JieSuanEvent;
        GameEvent.ZongJieSuanEvent += ZongJieSuanEvent;
    }

    //结算
    private void JieSuanEvent(List<PlayerData> plist)
    {
        plaerlist = plist;

    }

    //总结算
    private void ZongJieSuanEvent(List<PlayerData> plist)
    {
        zongPlaerlist = plist;

    }

    public void reset()
    {
        xiaojiesuan.gameObject.SetActive(false);
        dajiesuan.gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        xiaojiesuan.gameObject.SetActive(false);
        dajiesuan.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (plaerlist != null && plaerlist.Count > 0)
        {
            xiaojiesuan.gameObject.SetActive(true);
            xiaojiesuan.GetComponent<XiaoJieSuan>().showJiesuan(plaerlist);
            plaerlist.Clear();
        }

        if (zongPlaerlist != null && zongPlaerlist.Count > 0)
        {
            dajiesuan.gameObject.SetActive(true);
            dajiesuan.GetComponent<DaJieSuan>().showJiesuan(zongPlaerlist);
            zongPlaerlist.Clear();
        }
    }
}
