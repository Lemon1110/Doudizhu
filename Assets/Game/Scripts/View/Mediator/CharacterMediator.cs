using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 连接角色命令和视图
/// </summary>
public class CharacterMediator : EventMediator
{
    [Inject]
    public CharacterView CharacterView { get; set; }
    public override void OnRegister()
    {
        CharacterView.Init();
        dispatcher.AddListener(CommandEvent.DealCard, onDealCard);
        dispatcher.AddListener(ViewEvent.COMPLETE_DEAL, onCompleteDeal);
        dispatcher.AddListener(ViewEvent.DEAL_THREECARD, onDealThreeCard);
        dispatcher.AddListener(ViewEvent.REQUEST_PLAY, onPlayerPlayCard);
        dispatcher.AddListener(ViewEvent.SUCCESS_PLAY, onPlaySuccessPlay);
        dispatcher.AddListener(ViewEvent.RESTART_GAME, onRestartGame);
        dispatcher.AddListener(ViewEvent.UPDATE_INTEGRATION, onUpdateIntegration);

        RoundModel.ComputerHandler += RoundModel_ComputerHandler;

        //更新积分
        dispatcher.Dispatch(CommandEvent.RequestUpdate);
    }

  

    public override void OnRemove()
    {
        dispatcher.RemoveListener(CommandEvent.DealCard, onDealCard);
        dispatcher.RemoveListener(ViewEvent.COMPLETE_DEAL, onCompleteDeal);
        dispatcher.RemoveListener(ViewEvent.DEAL_THREECARD, onDealThreeCard);
        dispatcher.RemoveListener(ViewEvent.REQUEST_PLAY, onPlayerPlayCard);
        dispatcher.RemoveListener(ViewEvent.SUCCESS_PLAY, onPlaySuccessPlay);
        dispatcher.RemoveListener(ViewEvent.RESTART_GAME, onRestartGame);
        dispatcher.RemoveListener(ViewEvent.UPDATE_INTEGRATION, onUpdateIntegration);
        RoundModel.ComputerHandler -= RoundModel_ComputerHandler;
    }
    #region 回调函数
    /// <summary>
    /// 更新积分显示
    /// </summary>
    /// <param name="payload"></param>
    private void onUpdateIntegration(IEvent evt)
    {
        GameData data = evt.data as GameData;
        CharacterView.player.characterUI.SetIntegration(data.PlayerIntegration);
        CharacterView.ComputerLeft.characterUI.SetIntegration(data.ComputerLeftIntegration);
        CharacterView.ComputerRight.characterUI.SetIntegration(data.ComputerRightIntegration);
    }
    /// <summary>
    /// 重新开始游戏
    /// </summary>
    private void onRestartGame()
    {
        CharacterView.player.CardList.Clear();
        CharacterView.ComputerLeft.CardList.Clear();
        CharacterView.ComputerRight.CardList.Clear();
    }
    
    /// <summary>
    /// 电脑自动出牌
    /// </summary>
    /// <param name="obj"></param>
    private void RoundModel_ComputerHandler(ComputerSmartArgs e)
    {

        StartCoroutine(DelayOneSecoud(e));
    }

    IEnumerator DelayOneSecoud(ComputerSmartArgs e)
    {
        yield return new WaitForSeconds(2f);
        bool can = false;
        switch (e.characterType)
        {
            case CharacterType.ComputerRight:
                can = CharacterView.ComputerRight.ComputerSmartPlayCard(e.cardType, e.weight,
                    e.length, e.biggest == CharacterType.ComputerRight);
                //出牌的检测
                if (can)
                {
                    List<Card> cardList = CharacterView.ComputerRight.SelectCards;
                    CardType cardtype = CharacterView.ComputerRight.currType;
                    CharacterView.Desk.Clear();//先清空桌面
                    foreach (var item in cardList)//添加牌到桌面
                    {
                        CharacterView.AddCard(CharacterType.Desk, item, false);
                    }

                    PlayCardArgs ee = new PlayCardArgs(cardList.Count, Tools.GetWeight(cardList, cardtype), CharacterType.ComputerRight, cardtype);
                    //游戏胜利的判断
                    if (!CharacterView.ComputerRight.HasCard)
                    {
                        Identity r = CharacterView.ComputerRight.Identity;
                        Identity l = CharacterView.ComputerLeft.Identity;
                        Identity p = CharacterView.player.Identity;
                        GameOverArgs eee = new GameOverArgs()
                        {
                            ComputerRightWin = true,
                            ComputerLeftWin = l == r ? true : false,
                            PlayerWin = p == r ? true : false
                        };
                        dispatcher.Dispatch(CommandEvent.GameOver, eee);
                    }
                    else
                        dispatcher.Dispatch(CommandEvent.PlayCard, ee);//传参

                }
                else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;
            case CharacterType.ComputerLeft:
                can = CharacterView.ComputerLeft.ComputerSmartPlayCard(e.cardType, e.weight,
                    e.length, e.biggest == CharacterType.ComputerLeft);
                //出牌的检测
                if (can)
                {
                    List<Card> cardList = CharacterView.ComputerLeft.SelectCards;
                    CardType cardtype = CharacterView.ComputerLeft.currType;
                    CharacterView.Desk.Clear();//先清空桌面
                    foreach (var item in cardList)//添加牌到桌面
                    {
                        CharacterView.AddCard(CharacterType.Desk, item, false);
                    }

                    PlayCardArgs ee = new PlayCardArgs(cardList.Count, Tools.GetWeight(cardList, cardtype), CharacterType.ComputerLeft, cardtype);
                    //游戏胜利的判断
                    if (!CharacterView.ComputerLeft.HasCard)
                    {

                        Identity r = CharacterView.ComputerRight.Identity;
                        Identity l = CharacterView.ComputerLeft.Identity;
                        Identity p = CharacterView.player.Identity;
                        GameOverArgs eee = new GameOverArgs()
                        {
                            ComputerLeftWin = true,
                            ComputerRightWin = r == l ? true : false,
                            PlayerWin = p == l ? true : false
                        };
                        dispatcher.Dispatch(CommandEvent.GameOver, eee);
                    }
                    else
                        dispatcher.Dispatch(CommandEvent.PlayCard, ee);//传参
                }
                else
                {
                    dispatcher.Dispatch(CommandEvent.PassCard);
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 发牌回调
    /// </summary>
    private void onDealCard(IEvent evt)
    {
        DealCardArgs e = evt.data as DealCardArgs;
        CharacterView.AddCard(e.cType, e.card, e.selected);
    }
    /// <summary>
    /// 发牌结束回调
    /// </summary>
    private void onCompleteDeal()
    {
        CharacterView.player.Sort(false);
        CharacterView.Desk.Sort(true);
        CharacterView.ComputerLeft.Sort(true);
        CharacterView.ComputerRight.Sort(true);
    }
    /// <summary>
    /// 发底牌的回调
    /// </summary>

    private void onDealThreeCard(IEvent evt)
    {
        GrabLandlordArgs e = evt.data as GrabLandlordArgs;

        CharacterView.AddThreeCard(e.ctype);
    }
    /// <summary>
    /// 请求出牌的回调
    /// </summary>
    /// <param name="payload"></param>
    private void onPlayerPlayCard(IEvent payload)
    {
        List<Card> cardList = CharacterView.player.FindSelectCard();
        CardType cardtype;
        bool can = Rulers.CanPop(cardList, out cardtype);
        if (can)
        {
            //可以出牌

            PlayCardArgs e = new PlayCardArgs(cardList.Count, Tools.GetWeight(cardList, cardtype), CharacterType.Player, cardtype);

            dispatcher.Dispatch(CommandEvent.PlayCard, e);//传参


        }
        else
        {
            Debug.Log("不符合出牌规则，请重新选择");
        }
    }
    /// <summary>
    /// 玩家成功出牌
    /// </summary>
    private void onPlaySuccessPlay()
    {
        List<Card> cardList = CharacterView.player.FindSelectCard();
        CharacterView.Desk.Clear();//先清空桌面
        foreach (var item in cardList)//添加牌到桌面
        {
            CharacterView.AddCard(CharacterType.Desk, item, false);
        }
        CharacterView.player.DeleteCardUI();
        //游戏胜利的判断
        if (!CharacterView.player.HasCard)
        {
            Identity r = CharacterView.ComputerRight.Identity;
            Identity l = CharacterView.ComputerLeft.Identity;
            Identity p = CharacterView.player.Identity;
            GameOverArgs eee = new GameOverArgs()
            {
                PlayerWin  = true,
                ComputerRightWin = r == p ? true : false,
                ComputerLeftWin = l == p ? true : false
            };
            dispatcher.Dispatch(CommandEvent.GameOver,eee);
        }
        else
            dispatcher.Dispatch(ViewEvent.COMPLETE_PLAY);

    }


    #endregion

}
