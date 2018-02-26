using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class HallView : MonoBehaviour
{

    Button CreatBtn;
    Button JoinBtn;
    public Text text_fk;
    public Text text_name;
    public Image iocn;
    Transform root;
    [SerializeField]
    public CreatRoomWindow CreatWindow;

    public JoinRoomWindow JoinWindow;

    private  string SenceName = "";
    private string newName = "";
    private string picId = "";

    // Use this for initialization
    void Awake()
    {
        GameEvent.SenceEvent += DoSenceEvent;
        GameEvent.StringEvent += reNameEvent;
        GameEvent.PicEvent += rePicEvent;
    }
    // Use this for initialization
    void Start()
    {
        root = this.transform;
        CreatBtn = root.Find("Button_creat").GetComponent<Button>();
        CreatBtn.onClick.AddListener(CreatPressed);
        JoinBtn = root.Find("Button _join").GetComponent<Button>();
        JoinBtn.onClick.AddListener(JoinPressed);

        text_fk.text = " "+GameInfo.Instance.UserFK;
        text_name.text =  GameInfo.Instance.UserName;
        Sprite sp = Resources.Load("Texture/hall/head/head_"+GameInfo.Instance.UserIcon, typeof(Sprite)) as Sprite;
        iocn.sprite = sp;
        //
        AudioMgr.Instance.BGMPlay("Bg/bgm",0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        if (SenceName != "")
        {
            SceneManager.LoadScene(SenceName);
        }

        if (newName != "")
        {
            text_name.text = newName;
            newName = "";
        }

        if (picId != "")
        {
            Sprite sp = Resources.Load("Texture/hall/head/head_" + picId, typeof(Sprite)) as Sprite;
            iocn.sprite = sp;

            picId = "";
        }
    }
    public void CreatPressed()
    {
        CreatWindow.showWindow();
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }
    public void JoinPressed()
    {
        JoinWindow.showWindow();
        AudioMgr.Instance.SoundPlay("btnClick", 1f, 2);
    }

    void DoSenceEvent(string str)
    {
        SenceName = str; 
     
    }

    void reNameEvent(string str)
    {
        newName = str;
    }

    void rePicEvent(string str)
    {
        picId = str;
    }
}
