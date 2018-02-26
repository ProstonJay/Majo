using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : MonoBehaviour {

    public Button helpBtn;
    public Button setBtn;
    public Button emailBtn;
    public Button zhanjiBtn;
    public Button qiutBtn;
    public Button playerBtn;

    public GameObject qiutPanel;
    public GameObject usePanel;

    // Update is called once per frame
    void Update()
    {

    }
    // Use this for initialization
    void Start () {
        helpBtn.onClick.AddListener(helpPressed);
        setBtn.onClick.AddListener(setPressed);
        emailBtn.onClick.AddListener(emailPressed);
        zhanjiBtn.onClick.AddListener(zhanjiPressed);
        qiutBtn.onClick.AddListener(qiutPressed);
        playerBtn.onClick.AddListener(userPressed);

        qiutPanel.transform.gameObject.SetActive(false);
        usePanel.transform.gameObject.SetActive(false);
    }

    //用户
    public void userPressed()
    {
        //usePanel.transform.gameObject.SetActive(true);
        //usePanel.GetComponent<InfoPanel>().showUserPanel();
        usePanel.GetComponent<InfoPanel>().setAvtie();
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }

    //帮助
    public void helpPressed()
    {
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
    //设置
    public void setPressed()
    {
        //setPanel.transform.gameObject.SetActive(true);
        //setPanel.GetComponent<SetView>().showSetPanel();
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
    //邮件
    public void emailPressed()
    {
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
        AudioMgr.Instance.BGMSetVolume(0.1f);
    }
    //战绩
    public void zhanjiPressed()
    {
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
    //退出游戏
    public void qiutPressed()
    {
        qiutPanel.transform.gameObject.SetActive(true);
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
}
