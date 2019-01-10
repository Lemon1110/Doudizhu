using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 电脑控制
/// </summary>
public class ComputerControl :CharacterBase{
    /// <summary>
    /// 角色UI控制
    /// </summary>
    public CharacterUI characterUI;
    /// <summary>
    /// Pass
    /// </summary>
    public CanvasGroup cg_Pass;

    private Identity identity;

    public ComputerAI ComputerAI;
    /// <summary>
    /// 角色身份
    /// </summary>
    public Identity Identity
    {
        get { return identity; }
        set
        {
            identity = value;
            characterUI.SetIdentity(value);
        }
    }

    /// <summary>
    /// 添加卡牌
    /// </summary>
    /// <param name="card"></param>
    public override void AddCard(Card card, bool selected)
    {
        base.AddCard(card, selected);
        
        characterUI.SetRemain(CardCount);

    }
    /// <summary>
    /// 出牌
    /// </summary>
    /// <param name="card"></param>
    public override Card DealCard()
    {
        Card card = base.DealCard();
        characterUI.SetRemain(CardCount);
        return card;

    }
    public override void Sort(bool asc)
    {
        base.Sort(asc);
        
    }
    /// <summary>
    /// 电脑出的牌的类型
    /// </summary>
    public CardType currType { get { return ComputerAI.currType; } }
    /// <summary>
    /// 电脑选择的牌
    /// </summary>
    public List<Card> SelectCards { get { return ComputerAI.SelectCards; } }
    /// <summary>
    /// 电脑自动出牌
    /// </summary>
    public bool ComputerSmartPlayCard( CardType cardType, int weight, int length, bool isBiggest)
    {
        ComputerAI.SmartSelectCard(CardList,cardType,weight,length,isBiggest);
        if (SelectCards.Count != 0)
        {
            DestroyCards();
            return true;
        }
        else
        {
            ComputerPass();
            return false;
        }
    }
    /// <summary>
    /// 删除手牌
    /// </summary>
    private void DestroyCards()
    {
        CardUI[] cardUIs = transform.Find("CreatePoint").GetComponentsInChildren<CardUI>();
        for (int i = 0; i < cardUIs.Length; i++)
        {
            for (int j = 0; j < SelectCards.Count; j++)
            {
                if (SelectCards[j] == cardUIs[i].Card)
                {
                    cardUIs[i].Destroy();
                    CardList.Remove(SelectCards[j]);
                }
            }
        }
        SortCardUI(CardList);
        characterUI.SetRemain(CardCount);
    }

    //临时保存的卡牌和卡牌UI
    List<CardUI> tempcardUI = null;
    List<Card> tempCard = null;
   
    
    /// <summary>
    /// 电脑Pass 
    /// </summary>
    public void ComputerPass()
    {
        cg_Pass.alpha =1;
        StartCoroutine(PassAnim());
    }
    /// <summary>
    /// Pass的渐隐动画
    /// </summary>
    /// <returns></returns>
    IEnumerator PassAnim()
    {
        float time = 1f;
        while (time >= 0f)
        {
            yield return new WaitForSeconds(0.1f);
            time -= 0.1f;
            cg_Pass.alpha -= 0.1f;
        }
    }
}
