using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JieSuan : MonoBehaviour {

    public Text nameText;
    public Image zhuang;
    public Image winbg;
    public Image hupai;

    private PlayerData pdata;

    public void setData(PlayerData value) {
        pdata = value;
        nameText.text = pdata.getPlayerName();
        int acitonLen = 0;
        if (pdata.getactionlist()!=null&&pdata.getactionlist().Count > 0)
        {
            addAction(pdata.getactionlist());
            acitonLen = pdata.getactionlist().Count;
        }
   
        addHand(pdata.gethandlist(), acitonLen);
        if (value.getZhuangjia() > 0)
        {
            zhuang.gameObject.SetActive(true);
        }
        if (value.getHupai() > 0)
        {
            winbg.gameObject.SetActive(true);
            hupai.gameObject.SetActive(true);
        }
    }

    public void addAction(List<Action> list)
    {
        for(int i = 0; i < list.Count; i++)
        {
            Action action = list[i];
            switch (action.getActionType())
            {
                case 1://吃
                    for (int j = 0; j < action.getActionData().Count; j++)
                    {
                        GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_my_out_card")) as GameObject;
                        mjCard.transform.localPosition = new Vector3((j * 36) + (i * 118 + 25) - 400, 0, 0);
                        mjCard.transform.SetParent(this.transform, false);
                        mjCard.GetComponent<Mj_my_out>().setPic(action.getActionData()[j].ToString());
                    }
                    break;
                case 2://碰
                    for (int j = 0; j < action.getActionData().Count; j++)
                    {
                        GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_my_out_card")) as GameObject;
                        mjCard.transform.localPosition = new Vector3((j * 36) + (i * 118 + 25) - 400, 0, 0);
                        mjCard.transform.SetParent(this.transform, false);
                        mjCard.GetComponent<Mj_my_out>().setPic(action.getActionData()[j].ToString());
                    }
                    break;
                case 3://杠
                    break;
            }
        }
    }

    public void addHand(List<int> list,int pos)
    {
        for (int j = 0; j < list.Count; j++)
        {
            GameObject mjCard = Instantiate(Resources.Load("Prefab/GameObject_my_out_card")) as GameObject;
            if (j == list.Count - 1&& pdata.getHupai()>0)
            {
                mjCard.transform.localPosition = new Vector3((j * 36)-500 + (pos * 170 + 60), 0, 0);
            }
            else
            {
                mjCard.transform.localPosition = new Vector3((j * 36)-500 + (pos * 170 + 20), 0, 0);
            }
      
            mjCard.transform.SetParent(this.transform, false);
            mjCard.GetComponent<Mj_my_out>().setPic(list[j].ToString());
        }
    }

    // Use this for initialization
    void Start () {
       

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
