using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackView : MonoBehaviour {

    public Text img_roomId;
    public Image img_tableColor;
    public Text text_round;

    private string tablePath = "";

    private int startFlag;
    private int changeTableColor;

    void Awake()
    {
        GameEvent.GameStartEvent += GameStart;
        //
        RoomEvent.ChangeTableColorEvent += ChangeTableColorEvent;
    }

    //换桌布
    private void ChangeTableColorEvent()
    {
        changeTableColor = 1;
    }

    //开局
    private void GameStart(int value)
    {
        if (value ==1)
        {
            startFlag = 1;
        }

    }

    // Use this for initialization
    void Start () {
        img_roomId.text = "房间号 : "+GameInfo.Instance.roomId;

        changeTable();

    }

    private void changeTable()
    {
        if (PlayerPrefs.GetString("TableColor") == "blue")
        {
            tablePath = "Texture/game/room/background_2";
        }
        else if (PlayerPrefs.GetString("TableColor") == "green")
        {
            tablePath = "Texture/game/room/background_1";
        }
    }

    private void upDateRound()
    {
        text_round.text = "第 " + GameInfo.Instance.round + " 圈";
    }
	
	// Update is called once per frame
	void Update () {
        if (tablePath!="")
        {

            Sprite sp = Resources.Load(tablePath, typeof(Sprite)) as Sprite;
            img_tableColor.sprite = sp;
            tablePath = "";
        }
        if (startFlag == 1)
        {
            upDateRound();
            startFlag = 0;
        }

        if (changeTableColor == 1)
        {
            changeTable();
            changeTableColor = 0;
        }
    }

    //重置数据
    public void resetView()
    {
        text_round.text = "";
    }

}
