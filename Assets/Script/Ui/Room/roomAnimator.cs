using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class roomAnimator : MonoBehaviour
{

    public GameObject myNum;
    public GameObject rightNum;
    public GameObject topNum;
    public GameObject leftNum;

    public Button nextBtn;

    public int duijukaishi;

    public Image imgHuType;

    //碰牌
    private string pengPos = "";
    //自摸
    private string zimoPos = "";
    //杠牌
    private string gangPos = "";
    //吃胡
    private string chihuPos = "";
    //流局
    private int liuju;

    //
    private List<PlayerData> plist = new List<PlayerData>();


    //自摸胡类型
    private int zimohuType;
    //
    private int gangType;
    private string gangPostion;
    private int fangGangPos;

    void Awake()
    {
        GameEvent.GameStartEvent += GameStart;
        RoomEvent.PengPaiEvent += PengPaiEvent;
        RoomEvent.ZiMoEvent += ZiMoEvent;
        RoomEvent.ZhiGangEvent += ZhiGangEvent;
        RoomEvent.AnGangEvent += AnGangEvent;
        RoomEvent.MingGangEvent += MingGangEvent;
        RoomEvent.ChiHuEvent += ChiHuEvent;
        RoomEvent.LiuJuEvent += LiuJuEvent;
    }

    //流局动画
    private void LiuJuEvent(List<PlayerData> list)
    {
        liuju = 1;
        this.plist = list;
    }

    //开局动画
    private void GameStart(int type)
    {
        if (type == 1)
        {
            duijukaishi = 1;
        }

    }

    //碰牌动画
    private void PengPaiEvent(string pos, int mj)
    {
        if (pos != "")
        {
            this.pengPos = pos;
        }

    }

    //直杠动画
    private void ZhiGangEvent(string pos, int mj, int fangGangPos)
    {
        if (pos != "")
        {
            this.gangPos = pos;
            this.gangType = 1;
            this.fangGangPos = fangGangPos;
        }

    }

    //明杠动画
    private void MingGangEvent(string pos, int mj)
    {
        if (pos != "")
        {
            this.gangPos = pos;
            this.gangType = 2;

        }

    }

    //直杠动画
    private void AnGangEvent(string pos, int mj)
    {
        if (pos != "")
        {
            this.gangPos = pos;
            this.gangType = 3;
        }

    }

    //自摸动画
    private void ZiMoEvent(string pos, int mj, List<PlayerData> list)
    {
        Debug.Log("自摸动画 pos="+ pos+ "plist.Count = " + plist.Count);
        this.zimoPos = pos;
        this.plist = list;
        if(plist!=null&& plist.Count > 0)
        {
            zimohuType = plist[0].getHupai();
        }

    }


    //吃胡动画
    private void ChiHuEvent(string pos, int mj, int fangpao, List<PlayerData> list)
    {
        Debug.Log("自摸动画 pos=" + pos + "plist.Count = " + plist.Count);
        this.chihuPos = pos;
        this.plist = list;

    }


    // Use this for initialization
    void Start () {
        nextBtn.onClick.AddListener(nextPressed);
        nextBtn.gameObject.SetActive(false);
    }

    private void nextPressed()
    {
        Debug.Log("获取结果");
        //获取结果
        SocketModel endRequset = new SocketModel();
        endRequset.SetMainCmd(ProtocolMC.Main_Cmd_ROOM);
        endRequset.SetSubCmd(ProtocolSC.Sub_Cmd_GAME_END);
        endRequset.SetCommand(0);
        List<int> datalist = new List<int>();
        datalist.Add(GameInfo.Instance.roomId);//房间号
        datalist.Add(GameInfo.Instance.positon);//位置
        endRequset.SetData(datalist);
        NettySocket.Instance.SendMsg(endRequset);//发送这条消息给服务器
    //
        nextBtn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //开局
        if (duijukaishi == 1)
        {
            playStartAnimator();
            duijukaishi = 0;
        }

        //碰牌
        if (pengPos != "")
        {
            playPengAnimator(pengPos);
            pengPos = "";
        }

        //杠牌
        if (gangPos != "")
        {
            playGangAnimator(gangPos);
            gangPos = "";
        }

        //自摸
        if (zimoPos != "")
        {
            playZiMoAnimator(zimoPos);
            zimoPos = "";
        }

        //吃胡
        if (chihuPos != "")
        {
            playChiHuAnimator(chihuPos);
            chihuPos = "";
        }
        //流局
        if (liuju >0)
        {
            playLiuJuAnimator();
            liuju = 0;
        }
    }

    //流局动画
    private void playLiuJuAnimator()
    {
        StartCoroutine(showHuNumber());
        StartCoroutine(roundEnd());
    }

        //吃胡动画
    private void playChiHuAnimator(string pos)
    {
        GameObject ani = Instantiate(Resources.Load("Prefab/Image_Animator_hupai")) as GameObject;
        ani.transform.SetParent(this.transform, false);
        //ani.GetComponent<GameAnimator>().Play();
        gangPostion = pos;
        switch (pos)
        {
            case "bot":
                ani.transform.localPosition = new Vector3(0, -120, 0);
                break;
            case "right":
                ani.transform.localPosition = new Vector3(270, 40, 0);
                break;
            case "top":
                ani.transform.localPosition = new Vector3(0, 200, 0);
                break;
            case "left":
                ani.transform.localPosition = new Vector3(-270, 40, 0);
                break;
        }

        StartCoroutine(showHuNumber());
        StartCoroutine(roundEnd());
        Debug.Log("吃胡配音路径" + MajooUtil.getChiHuViocePath());
        AudioMgr.Instance.SoundPlay(MajooUtil.getChiHuViocePath(), 1, 0);

    }

    //自摸动画
    private void playZiMoAnimator(string pos)
    {
        GameObject ani = Instantiate(Resources.Load("Prefab/Image_Animator_zimo")) as GameObject;
        ani.transform.SetParent(this.transform, false);
        //ani.GetComponent<GameAnimator>().Play();
        switch (pos)
        {
            case "bot":
                ani.transform.localPosition = new Vector3(0, -120, 0);
                break;
            case "right":
                ani.transform.localPosition = new Vector3(270, 40, 0);
                break;
            case "top":
                ani.transform.localPosition = new Vector3(0, 200, 0);
                break;
            case "left":
                ani.transform.localPosition = new Vector3(-270, 40, 0);
                break;
        }
        Debug.Log("延迟显示结算数字");
        StartCoroutine(showHuNumber());
        Debug.Log("延迟显示结果按键");
        StartCoroutine(roundEnd());
        if (this.zimohuType == 12)//抢杠胡
        {
            AudioMgr.Instance.SoundPlay(MajooUtil.getChiHuViocePath(), 1, 0);
        }
        else
        {
            AudioMgr.Instance.SoundPlay(MajooUtil.getZiMoViocePath(), 1, 0);
        }
        zimohuType = 0;
    }

    //碰牌动画
    private void playPengAnimator(string pos)
    {
        GameObject ani = Instantiate(Resources.Load("Prefab/Image_Animator_peng")) as GameObject;
        ani.transform.SetParent(this.transform, false);
        //ani.GetComponent<GameAnimator>().Play();
        switch (pos)
        {
            case "bot":
                ani.transform.localPosition = new Vector3(0, -120, 0);
                break;
            case "right":
                ani.transform.localPosition = new Vector3(270, 40, 0);
                break;
            case "top":
                ani.transform.localPosition = new Vector3(0, 200, 0);
                break;
            case "left":
                ani.transform.localPosition = new Vector3(-270, 40, 0);
                break;
        }
        //mjCard.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    //杠牌动画
    private void playGangAnimator(string pos)
    {
        GameObject longjuanfeng = Instantiate(Resources.Load("Prefab/Image_Animator_longjuanfeng")) as GameObject;
        longjuanfeng.transform.SetParent(this.transform, false);
        gangPostion = pos;
        switch (pos)
        {
            case "bot":
                longjuanfeng.transform.localPosition = new Vector3(20, -30, 0);
                break;
            case "right":
                longjuanfeng.transform.localPosition = new Vector3(260, 150, 0);
                break;
            case "top":
                longjuanfeng.transform.localPosition = new Vector3(40, 280, 0);
                break;
            case "left":
                longjuanfeng.transform.localPosition = new Vector3(-230, 150, 0);
                break;
        }

        GameObject ani = Instantiate(Resources.Load("Prefab/Image_Animator_gang")) as GameObject;
        ani.transform.SetParent(this.transform, false);
        //ani.GetComponent<GameAnimator>().Play();
        switch (pos)
        {
            case "bot":
                ani.transform.localPosition = new Vector3(0, -120, 0);
                break;
            case "right":
                ani.transform.localPosition = new Vector3(270, 40, 0);
                break;
            case "top":
                ani.transform.localPosition = new Vector3(0, 200, 0);
                break;
            case "left":
                ani.transform.localPosition = new Vector3(-270, 40, 0);
                break;
        }
        //mjCard.transform.localScale = new Vector3(1.2f, 1.2f, 1);
        Invoke("showNumer", 1);
    }

    //开局动画
    private void playStartAnimator()
    {
        GameObject ani = Instantiate(Resources.Load("Prefab/Image_Animator_duijukaishi")) as GameObject;
        ani.transform.SetParent(this.transform, false);
        //ani.GetComponent<GameAnimator>().Play();
        ani.transform.localPosition = new Vector3(0, 20, 0);
        //mjCard.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    private IEnumerator roundEnd()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("显示结果按键");
        nextBtn.gameObject.SetActive(true);
    }

    private IEnumerator showHuNumber()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("显示结算数字 plist长度="+ plist.Count);
        int hutype = 0;
        for (int i = 0; i < this.plist.Count; i++)
        {
            PlayerData pd = plist[i];
            hutype=pd.getHupai();
            Debug.Log("i="+i+" 结算=" + pd.getWinGold());
            string pos = "";
            if (pd.getWinGold() != 0)
            {
                if (pd.getUserId() == GameInfo.Instance.positon)//位置是自己
                {
                    pos = "bot";
                }
                else
                {
                    pos = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, pd.getUserId());
                }
                Debug.Log("pos 结算="+ pd.getWinGold());
                if (hutype != 10)//如果是 10 就是流局 ，没数值
                {
                    switch (pos)
                    {
                        case "bot":
                            myNum.GetComponent<Number>().showNumber(pd.getWinGold());
                            break;
                        case "right":
                            rightNum.GetComponent<Number>().showNumber(pd.getWinGold());
                            break;
                        case "top":
                            topNum.GetComponent<Number>().showNumber(pd.getWinGold());
                            break;
                        case "left":
                            leftNum.GetComponent<Number>().showNumber(pd.getWinGold());
                            break;
                    }
                }

            }
        }
        //
        Debug.Log("胡牌的类型="+ hutype);
        switch (hutype)
        {
            case MajooUtil.HuPai_PaiXing__DaDuiZi_大对子:
            case MajooUtil.HuPai_PaiXing__QingDaDui_清大对:
                this.imgHuType.transform.gameObject.SetActive(true);
                Sprite sp1 = Resources.Load("Texture/game/xiaojiesuan/HuType_2", typeof(Sprite)) as Sprite;
                this.imgHuType.sprite = sp1;
                break;
            case MajooUtil.HuPai_PaiXing__QingYiSe_清一色:
                this.imgHuType.transform.gameObject.SetActive(true);
                Sprite sp2 = Resources.Load("Texture/game/xiaojiesuan/HuType_" + hutype, typeof(Sprite)) as Sprite;
                this.imgHuType.sprite = sp2;
                break;
            case MajooUtil.HuPai_PaiXing__XiaoQiDui_小七对:
            case MajooUtil.HuPai_PaiXing__QingQiDui_清七对:
                this.imgHuType.transform.gameObject.SetActive(true);
                Sprite sp3 = Resources.Load("Texture/game/xiaojiesuan/HuType_4", typeof(Sprite)) as Sprite;
                this.imgHuType.sprite = sp3;
                break;
            case MajooUtil.HuPai_PaiXing__LongQiDui_龙七对:
            case MajooUtil.HuPai_PaiXing__QingLongQiDui_清龙七对:
                this.imgHuType.transform.gameObject.SetActive(true);
                Sprite sp4 = Resources.Load("Texture/game/xiaojiesuan/HuType_6", typeof(Sprite)) as Sprite;
                this.imgHuType.sprite = sp4;
                break;
            case MajooUtil.HuPai_PaiXing__ShuangLongQiDui_双龙七对:
            case MajooUtil.HuPai_PaiXing__ShuangQingLongQiDui_双清龙七对:
                this.imgHuType.transform.gameObject.SetActive(true);
                Sprite sp5 = Resources.Load("Texture/game/xiaojiesuan/HuType_8" , typeof(Sprite)) as Sprite;
                this.imgHuType.sprite = sp5;
                break;
            case MajooUtil.HuPai_PaiXing__LiuJu_流局:
                this.imgHuType.transform.gameObject.SetActive(true);
                Sprite sp6 = Resources.Load("Texture/game/xiaojiesuan/HuType_" + hutype, typeof(Sprite)) as Sprite;
                this.imgHuType.sprite = sp6;
                break;
            case MajooUtil.HuPai_PaiXing__QiangGang_抢杠:
                this.imgHuType.transform.gameObject.SetActive(true);
                Sprite sp7 = Resources.Load("Texture/game/xiaojiesuan/HuType_" + hutype, typeof(Sprite)) as Sprite;
                this.imgHuType.sprite = sp7;
                break;
        }

        StartCoroutine(hideImgHuType());
    }

    private IEnumerator hideImgHuType()
    {
        yield return new WaitForSeconds(4);
        this.imgHuType.transform.gameObject.SetActive(false);
    }

    private void showNumer()
    {
        Debug.Log("杠牌类型="+ gangType+"杠牌位置="+ gangPostion);
        switch (this.gangType)
        {
            case 1://直杠
                        switch (gangPostion)
                        {
                            case "bot":
                                myNum.GetComponent<Number>().showNumber(200);
                                break;
                            case "right":
                                rightNum.GetComponent<Number>().showNumber(200);
                                break;
                            case "top":
                                topNum.GetComponent<Number>().showNumber(200);
                                break;
                            case "left":
                                leftNum.GetComponent<Number>().showNumber(200);
                                break;
                        }
                        Debug.Log("杠牌类型="+ gangType+"放杠位置="+ fangGangPos);
                        if (fangGangPos == GameInfo.Instance.positon)
                        {
                            myNum.GetComponent<Number>().showNumber(-200);
                         }
                        else
                        {
                            string fg = GameInfo.Instance.TryGetLocPos(GameInfo.Instance.positon, fangGangPos);
                            switch (fg)
                            {
                                case "right":
                                    rightNum.GetComponent<Number>().showNumber(-200);
                                    break;
                                case "top":
                                    topNum.GetComponent<Number>().showNumber(-200);
                                    break;
                                case "left":
                                    leftNum.GetComponent<Number>().showNumber(-200);
                                    break;
                            }
                        }

                break;
            case 2://明杠
                        switch (gangPostion)
                        {
                            case "bot":
                                myNum.GetComponent<Number>().showNumber(300);
                                rightNum.GetComponent<Number>().showNumber(-100);
                                topNum.GetComponent<Number>().showNumber(-100);
                                leftNum.GetComponent<Number>().showNumber(-100);
                            break;
                            case "right":
                                myNum.GetComponent<Number>().showNumber(-100);
                                rightNum.GetComponent<Number>().showNumber(300);
                                topNum.GetComponent<Number>().showNumber(-100);
                                leftNum.GetComponent<Number>().showNumber(-100);
                            break;
                            case "top":
                                myNum.GetComponent<Number>().showNumber(-100);
                                rightNum.GetComponent<Number>().showNumber(-100);
                                topNum.GetComponent<Number>().showNumber(300);
                                leftNum.GetComponent<Number>().showNumber(-100);
                            break;
                            case "left":
                                myNum.GetComponent<Number>().showNumber(-100);
                                rightNum.GetComponent<Number>().showNumber(-100);
                                topNum.GetComponent<Number>().showNumber(-100);
                                leftNum.GetComponent<Number>().showNumber(300);
                            break;
                        }
                break;
            case 3://暗杠
                        switch (gangPostion)
                        {
                            case "bot":
                                myNum.GetComponent<Number>().showNumber(600);
                                rightNum.GetComponent<Number>().showNumber(-200);
                                topNum.GetComponent<Number>().showNumber(-200);
                                leftNum.GetComponent<Number>().showNumber(-200);
                            break;
                            case "right":
                                myNum.GetComponent<Number>().showNumber(-200);
                                rightNum.GetComponent<Number>().showNumber(600);
                                topNum.GetComponent<Number>().showNumber(-200);
                                leftNum.GetComponent<Number>().showNumber(-200);
                            break;
                            case "top":
                                myNum.GetComponent<Number>().showNumber(-200);
                                rightNum.GetComponent<Number>().showNumber(-200);
                                topNum.GetComponent<Number>().showNumber(600);
                                leftNum.GetComponent<Number>().showNumber(-200);
                            break;
                            case "left":
                                myNum.GetComponent<Number>().showNumber(-200);
                                rightNum.GetComponent<Number>().showNumber(-200);
                                topNum.GetComponent<Number>().showNumber(-200);
                                leftNum.GetComponent<Number>().showNumber(600);
                            break;
                        }
                break;
        }
        gangType = 0;
        gangPostion = "";
    }
}
