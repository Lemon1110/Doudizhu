using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// 出牌
/// </summary>
public class PlayCardCommand:EventCommand
{
    [Inject]
    public RoundModel roundModel { get; set; }
    [Inject]
    public IntegrationModel integrationModel { get; set; }
    public override void Execute()
    {
        PlayCardArgs e = evt.data as PlayCardArgs;
        //判断玩家出牌是否合法
        if (e.CharacterType == CharacterType.Player)
        {
            if (e.cardType == roundModel.CardType && e.weight > roundModel.weight)
            {
                dispatcher.Dispatch(ViewEvent.SUCCESS_PLAY);
            }
            else if(e.cardType == CardType.Boom&&roundModel.CardType!=CardType.Boom)
            {
                dispatcher.Dispatch(ViewEvent.SUCCESS_PLAY);
            }
            else if (e.cardType == CardType.JokerBoom)
            {
                dispatcher.Dispatch(ViewEvent.SUCCESS_PLAY);
            }
            else if (roundModel.Biggest == CharacterType.Player)
            {
                dispatcher.Dispatch(ViewEvent.SUCCESS_PLAY);
            }
            else
            {
                Debug.Log("不合法的出牌");
                return;

            }
        }
       

        //炸弹翻倍
        if (e.cardType == CardType.Boom || e.cardType == CardType.JokerBoom)
        {
            Debug.Log("之前倍数"+integrationModel.Mutiples);
            integrationModel.Mutiples *= 2;
            Debug.Log("之后倍数" + integrationModel.Mutiples);
        }
        
        roundModel.Length = e.Length;
        roundModel.weight = e.weight;
        roundModel.CardType = e.cardType;
        roundModel.Biggest = e.CharacterType;
        //转换出牌
        roundModel.turn();
    }

}

