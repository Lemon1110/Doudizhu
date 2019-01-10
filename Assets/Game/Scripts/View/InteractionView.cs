using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.UI;
/// <summary>
/// 按钮交互View
/// </summary>
public class InteractionView : View {
    public Button btn_Deal;
    public Button btn_Pass;
    public Button btn_Play;
    public Button btn_Disgrab;
    public Button btn_Grab;
    
    /// <summary>
    /// 全部隐藏
    /// </summary>
    public void DeactiveAll()
    {
        btn_Deal.gameObject.SetActive(false);
        btn_Pass.gameObject.SetActive(false);
        btn_Play.gameObject.SetActive(false);
        btn_Disgrab.gameObject.SetActive(false);
        btn_Grab.gameObject.SetActive(false);
    }
    /// <summary>
    /// 显示发牌
    /// </summary>
    public void ActiveDeal()
    {
        btn_Deal.gameObject.SetActive(true);
        btn_Pass.gameObject.SetActive(false);
        btn_Play.gameObject.SetActive(false);
        btn_Disgrab.gameObject.SetActive(false);
        btn_Grab.gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示出牌
    /// </summary>
    public void ActivePlayAndPass(bool CanPass=true)
    {
        btn_Deal.gameObject.SetActive(false);
        btn_Pass.gameObject.SetActive(true);
        btn_Pass.interactable = CanPass;
        btn_Play.gameObject.SetActive(true);
        btn_Disgrab.gameObject.SetActive(false);
        btn_Grab.gameObject.SetActive(false);
    }
    /// <summary>
    /// 显示抢地主
    /// </summary>
    public void ActiveGrabAndDisgrab()
    {
        btn_Deal.gameObject.SetActive(false);
        btn_Pass.gameObject.SetActive(false);
        btn_Play.gameObject.SetActive(false);
        btn_Disgrab.gameObject.SetActive(true);
        btn_Grab.gameObject.SetActive(true);
    }
   
}
