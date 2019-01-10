using strange.extensions.command.impl;
using strange.extensions.context.api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// 请求发牌
/// </summary>
public class RequestDealCommand : EventCommand
{
    [Inject]
    public CardModel cardModel { get; set; }

    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject GameRoot { get; set; }
    public override void Execute()
    {
        cardModel.Shuffle();//洗牌

        //发牌
        GameRoot.GetComponent<GameRoot>().StartCoroutine(DealCard());
    }
    IEnumerator DealCard()
    {
        CharacterType curr = CharacterType.Player;
        for (int i = 0; i < 51; i++)
        {
            if (curr == CharacterType.Desk|| curr == CharacterType.Library)
                curr = CharacterType.Player;
            DealTo(curr);
            //换人
            curr++;
            //等待0.01s
            yield return new WaitForSeconds(0.01f);
        }
        //发底牌
        for (int i = 0; i < 3; i++)
        {
            DealTo(CharacterType.Desk);
        }
        //发牌结束
        dispatcher.Dispatch(ViewEvent.COMPLETE_DEAL);
    }

    /// <summary>
    /// 发牌
    /// </summary>
    /// <param name="cType">给谁</param>
    private void DealTo(CharacterType cType)
    {
        Card card = cardModel.DealCard(cType);
        DealCardArgs e = new DealCardArgs(cType, card, false);

        dispatcher.Dispatch(CommandEvent.DealCard, e);

    }

}

