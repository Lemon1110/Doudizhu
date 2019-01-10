using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterBase : MonoBehaviour {
    private List<Card> cardList = new List<Card>();       
  
    /// <summary>
    /// 当前角色类型
    /// </summary>
    public CharacterType characterType;

    private Transform createPoint;
    public Transform CreatePoint
    {
        get { if (createPoint == null)
            {
                createPoint = transform.Find("CreatePoint").transform;

            }
            return createPoint;
        }
    }

    /// <summary>
    /// 根据索引获取卡牌信息
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Card this[int index]
    {
        get { return cardList[index]; }
    }
    /// <summary>
    /// 根据卡牌获取索引
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public int this[Card card]
    {
        get { return cardList.IndexOf(card); }
    }
    /// <summary>
    /// 卡牌 集合
    /// </summary>
    public List<Card> CardList
    {
        get { return cardList; }
    }
    /// <summary>
    /// 当前卡牌数量
    /// </summary>
    /// <returns></returns>
    public int CardCount
    {
        get{ return cardList.Count; }
    }
    /// <summary>
    /// 是否还有手牌剩余
    /// </summary>
    public bool HasCard
    {
        get { return cardList.Count != 0; }
    }

   
    /// <summary>
    /// 添加牌
    /// </summary>
    public virtual void AddCard(Card card ,bool selected)
    {
        cardList.Add(card);
        card.BelongTo = characterType;
        //更新显示
        CreateCardUI(card, CardCount - 1, selected);
    }
    /// <summary>
    /// 出牌
    /// </summary>
    public virtual Card DealCard()
    {
        Card card = cardList[CardCount - 1];
        cardList.Remove(card);
        return card;
    }
    /// <summary>
    /// 创建卡牌预设
    /// </summary>
    /// <param name="card"></param>
    /// <param name="index"></param>
    public void CreateCardUI(Card card, int index, bool selected)
    {
        GameObject go = PoolManager.Instance.GetObject("Card");
       // go.name = characterType.ToString() + index.ToString();
      
        CardUI cardUI = go.GetComponent<CardUI>();
        
        cardUI.Card = card;
        cardUI.Selected = selected;
        cardUI.SetPosition(CreatePoint, index);
        go.name = card.CardWeight.ToString();
    }
    /// <summary>
    /// 排序
    /// </summary>
    /// asc 升序 desc 降序
    public virtual void Sort(bool asc)
    {
        Tools.Sort(cardList, asc);
        this.SortCardUI(cardList);

    }
    /// <summary>
    /// 排序卡牌UI
    /// </summary>
    /// <param name="cards">有序的序列</param>
    public  void SortCardUI(List<Card> cards)
    {
        CardUI[] cardUIs = CreatePoint.GetComponentsInChildren<CardUI>();
        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < cardUIs.Length; j++)
            {
                if (cards[i] == cardUIs[j].Card)
                {
                    cardUIs[j].SetPosition(CreatePoint, i);
                }
            }
        }

    }

}
