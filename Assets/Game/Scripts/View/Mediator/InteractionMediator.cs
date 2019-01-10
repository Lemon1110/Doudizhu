using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using System;

/// <summary>
/// 按钮交互命令
/// </summary>
public class InteractionMediator : EventMediator {
    [Inject]
    public InteractionView intergrationView { get; set; }
    public override void OnRegister()
    {
        intergrationView.ActiveDeal();
        intergrationView.btn_Deal.onClick.AddListener(onDealClick);
        intergrationView.btn_Disgrab.onClick.AddListener(onDisgrabClick);
        intergrationView.btn_Grab.onClick.AddListener(onGrabClick);
        intergrationView.btn_Pass.onClick.AddListener(onPassClick);
        intergrationView.btn_Play.onClick.AddListener(onPlayClick);
        dispatcher.AddListener(ViewEvent.RESTART_GAME, onRestartGame);
        dispatcher.AddListener(ViewEvent.COMPLETE_DEAL, onCompleteDeal);
        dispatcher.AddListener(ViewEvent.COMPLETE_PLAY, onCompletePlay);
        RoundModel.PlayerHandler += ActiveButton;
    }

   

    public override void OnRemove()
    {
        intergrationView.btn_Deal.onClick.RemoveListener(onDealClick);       
        intergrationView.btn_Disgrab.onClick.RemoveListener(onDisgrabClick);
        intergrationView.btn_Grab.onClick.RemoveListener(onGrabClick);
        intergrationView.btn_Pass.onClick.RemoveListener(onPassClick);
        intergrationView.btn_Play.onClick.RemoveListener(onPlayClick);
        dispatcher.RemoveListener(ViewEvent.RESTART_GAME, onRestartGame);
        dispatcher.RemoveListener(ViewEvent.COMPLETE_PLAY, onCompletePlay);
        RoundModel.PlayerHandler -= ActiveButton;
        dispatcher.RemoveListener(ViewEvent.COMPLETE_DEAL, onCompleteDeal);
    }
    #region 回调函数
    /// <summary>
    /// 重新开始游戏
    /// </summary>
    /// <param name="payload"></param>
    private void onRestartGame()
    {
        intergrationView.DeactiveAll();
        intergrationView.ActiveDeal();


    }
    /// <summary>
    /// 发牌的回调函数
    /// </summary>
    private void onDealClick()
    {
        dispatcher.Dispatch(CommandEvent.RequestDeal);
        intergrationView.DeactiveAll();
    }
    /// <summary>
    /// 发牌结束的回调
    /// </summary>
    /// <param name="payload"></param>
    private void onCompleteDeal()
    {
        intergrationView.ActiveGrabAndDisgrab();

    }
    /// <summary>
    /// 不抢地主的回调
    /// </summary>
    private void onDisgrabClick()
    {
        int r = UnityEngine.Random.Range(2, 4);//2 3 
        GrabLandlordArgs e = new GrabLandlordArgs()
        { ctype = (CharacterType)r };
        dispatcher.Dispatch(CommandEvent.GrabLandlord, e);
        intergrationView.DeactiveAll();
    }
    /// <summary>
    /// 抢地主的回调
    /// </summary>
    private void onGrabClick()
    {
        intergrationView.DeactiveAll();
        //倍数翻倍

        //身份改成地主
        GrabLandlordArgs e = new GrabLandlordArgs()
        { ctype = CharacterType.Player };
        dispatcher.Dispatch(CommandEvent.GrabLandlord,e);

       
    }
    /// <summary>
    /// 激活按钮
    /// </summary>
     private void ActiveButton(bool CanPass)
    {
        intergrationView.ActivePlayAndPass(CanPass);
    }
    /// <summary>
    /// 不出牌
    /// </summary>
    public void onPassClick()
    {
        dispatcher.Dispatch(CommandEvent.PassCard);
        intergrationView.DeactiveAll();
    }

    /// <summary>
    /// 出牌
    /// </summary>
    public void onPlayClick()
    {
        dispatcher.Dispatch(ViewEvent.REQUEST_PLAY);
    }
    /// <summary>
    /// 完成出牌的回调
    /// </summary>
    /// <param name="payload"></param>
    private void onCompletePlay()
    {
        intergrationView.DeactiveAll();
    }
   
    #endregion


}
