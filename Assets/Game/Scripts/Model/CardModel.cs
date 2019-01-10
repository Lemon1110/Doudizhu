using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// 牌库
/// </summary>
public class CardModel  {
    private Queue<Card> cardQueue = new Queue<Card>();//牌库
    private CharacterType cType= CharacterType.Library;

    /// <summary>
    /// 剩余牌的数量
    /// </summary>
    public int CardCount
    {
        get
        {
            return cardQueue.Count;
        }
    }
   
	/// <summary>
    /// 初始化牌库(创建54张牌)
    /// </summary>
    public void InitCardLibrary()
    {
        for (int color = 1; color < 5; color++)
        {
            for (int weight = 0; weight < 13; weight++)
            {
                Weight w = (Weight)weight;
                Colors c = (Colors)color;
                string name = c.ToString() + w.ToString();
                Card card = new Card(name, c, w, cType);
                cardQueue.Enqueue(card);
            }
        }
        Card sJoker = new Card("SJoker", Colors.None, Weight.SJoker,cType);
        Card lJoker = new Card("LJoker", Colors.None, Weight.LJoker, cType);
        cardQueue.Enqueue(sJoker);
        cardQueue.Enqueue(lJoker);

    }
    /// <summary>
    /// 发牌
    /// </summary>
    /// <returns></returns>
    public Card DealCard(CharacterType sendto)
    {
        Card card = cardQueue.Dequeue();
        card.BelongTo = sendto;
        return card;
    }
    /// <summary>
    /// 回收牌
    /// </summary>
    /// <param name="card"></param>
    public void RecycleCard(Card card)
    {
        cardQueue.Enqueue(card);
        card.BelongTo = cType;
    }

    /// <summary>
    /// 洗牌
    /// </summary>
    public void Shuffle()
    {
        List<Card> newList = new List<Card>();
        foreach (Card card in cardQueue)
        {
            int index = Random.Range(0, newList.Count + 1);
            newList.Insert(index, card);
        }
        cardQueue.Clear();
        foreach (Card card in newList)
        {
            
            cardQueue.Enqueue(card);
        }
        newList.Clear();
    }
}
