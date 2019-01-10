using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色UI
/// </summary>
public class CharacterUI : MonoBehaviour {
    public Image img_Head;//头像
    public Image img_Identity;//身份
    public Text txt_Integration;//积分
    public Text txt_Remain;//剩余
    
    /// <summary>
    /// 设置身份
    /// </summary>
    public void SetIdentity(Identity identity)
    {
        Sprite head = null;
        Sprite iden = null;
        switch (identity)
        {
            case Identity.Farmer:
                head = Resources.Load<Sprite>("Pokers/Role_Farmer");
                iden = Resources.Load<Sprite>("Pokers/Identity_Farmer");
                break;
            case Identity.Landlord:
                head = Resources.Load<Sprite>("Pokers/Role_Landlord");
                iden = Resources.Load<Sprite>("Pokers/Identity_Landlord");
                break;
            default:
                break;
        }
        if (head == null || iden == null)
        {
            Debug.LogError("设置身份的图片不存在！");
            return;
        }
            
        img_Head.sprite = head;
        img_Identity.sprite = iden;
    }
    /// <summary>
    /// 设置积分
    /// </summary>
    /// <param name="intergration"></param>
    public void SetIntegration(int integration)
    {
        txt_Integration.text = "积分:" + integration;
    }
    /// <summary>
    /// 设置剩余卡牌数
    /// </summary>
    /// <param name="number"></param>
    public void SetRemain(int number)
    {
        txt_Remain.text = "剩余牌数:" + number;
    }
   
}
