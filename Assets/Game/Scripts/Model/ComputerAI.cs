using System.Collections.Generic;
using UnityEngine;

public class ComputerAI : MonoBehaviour
{
    /// <summary>
    /// 当前的出牌类型
    /// </summary>
    public CardType currType = CardType.None;

    /// <summary>
    /// 当前出的牌
    /// </summary>
    public List<Card> SelectCards = new List<Card>();
    /// <summary>
    /// 自动出牌
    /// </summary>
    /// <param name="cards">所有的卡牌</param>
    /// <param name="cardType">上一轮出牌类型</param>
    /// <param name="weight">卡牌权值</param>
    /// <param name="length">卡牌长度</param>
    /// <param name="isBiggest">是否最大权值</param>
    /// <returns></returns>
    public void SmartSelectCard(List<Card> cards, CardType cardType, int weight, int length, bool isBiggest)
    {
        cardType = isBiggest ? CardType.None : cardType;
        currType = cardType;
        SelectCards.Clear();
        switch (cardType)
        {
            case CardType.None:
                //select=选择最小牌
                SelectCards = FindSmallestCard(cards);
                //修改当前t出牌类型
                break;
            case CardType.Single:
                SelectCards = FindSingle(cards, weight);
                break;
            case CardType.Double:
                SelectCards = FindDouble(cards, weight);
                break;
            case CardType.Straigtht:
                SelectCards = FindStraigtht(cards, weight, length);
                if (SelectCards.Count == 0)
                {
                    SelectCards = FindBoom(cards, -1);
                    currType = CardType.Boom;
                    if (SelectCards.Count == 0)
                    {
                        SelectCards = FindJokerBoom(cards);
                        currType = CardType.JokerBoom;
                    }
                }

                break;
            case CardType.DoubleStraight:
                //Sn=A1*n+n*(n-1)/d
                SelectCards = FindDoubleStraight(cards, weight, length);
                if (SelectCards.Count == 0)
                {
                    SelectCards = FindBoom(cards, -1);
                    currType = CardType.Boom;
                    if (SelectCards.Count == 0)
                    {
                        SelectCards = FindJokerBoom(cards);
                        currType = CardType.JokerBoom;
                    }
                }
                break;
            case CardType.TribleStraight:
                SelectCards = FindTribleStraight(cards, weight);
                if (SelectCards.Count == 0)
                {
                    SelectCards = FindBoom(cards, -1);
                    currType = CardType.Boom;
                    if (SelectCards.Count == 0)
                    {
                        SelectCards = FindJokerBoom(cards);
                        currType = CardType.JokerBoom;
                    }
                }
                break;
            case CardType.Three:
                SelectCards = FindThree(cards, weight);
                if (SelectCards.Count == 0)
                {
                    SelectCards = FindBoom(cards, -1);
                    currType = CardType.Boom;
                    if (SelectCards.Count == 0)
                    {
                        SelectCards = FindJokerBoom(cards);
                        currType = CardType.JokerBoom;
                    }
                }
                break;
            case CardType.ThreeAndOne:
                SelectCards = FindThreeAndOne(cards, weight);
                if (SelectCards.Count == 0)
                {
                    SelectCards = FindBoom(cards, -1);
                    currType = CardType.Boom;
                    if (SelectCards.Count == 0)
                    {
                        SelectCards = FindJokerBoom(cards);
                        currType = CardType.JokerBoom;
                    }
                }
                break;
            case CardType.ThreeAndTwo:
                SelectCards = FindThreeAndTwo(cards, weight);
                if (SelectCards.Count == 0)
                {
                    SelectCards = FindBoom(cards, -1);
                    currType = CardType.Boom;
                    if (SelectCards.Count == 0)
                    {
                        SelectCards = FindJokerBoom(cards);
                        currType = CardType.JokerBoom;
                    }
                }
                break;
            case CardType.Plane:
                SelectCards = FindPlane(cards, weight);
                if (SelectCards.Count == 0)
                {
                    SelectCards = FindBoom(cards, -1);
                    currType = CardType.Boom;
                    if (SelectCards.Count == 0)
                    {
                        SelectCards = FindJokerBoom(cards);
                        currType = CardType.JokerBoom;
                    }
                }
                break;

            case CardType.FourAndTwoSingle:
                SelectCards = FindBoom(cards, weight);
                if (SelectCards.Count == 0)
                {
                    SelectCards = FindJokerBoom(cards);
                    currType = CardType.JokerBoom;
                }
                break;
            case CardType.FourAndTwoDouble:
                SelectCards = FindBoom(cards, weight);
                if (SelectCards.Count == 0)
                {
                    SelectCards = FindJokerBoom(cards);
                    currType = CardType.JokerBoom;
                }
                break;
            case CardType.Boom:
                SelectCards = FindBoom(cards, weight);
                if (SelectCards.Count == 0)
                {
                    SelectCards = FindJokerBoom(cards);
                    currType = CardType.JokerBoom;
                }
                break;
            case CardType.JokerBoom:
                break;
            default:
                break;
        }

    }
    /// <summary>
    /// 找飞机带翅膀
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    private List<Card> FindPlane(List<Card> cards, int miniweight)
    {
        List<Card> select = new List<Card>();
        int i = 0;
        while(i>=4)
        {
            List<Card> Three1 = FindThree(cards, miniweight);
            if (Three1 == null)
            {
                break;
            }
            foreach (var item in Three1)
            {
                cards.Remove(item);                
            }
            if (cards.Count < 5)
            {
                break;
            }
            List<Card> Three2 = FindThree(cards, miniweight);            
            if (Three2 == null)
            {
                break;
            }
            int w1 = Tools.GetWeight(Three1, CardType.Three);
            int w2 = Tools.GetWeight(Three2, CardType.Three);
            if (w2 - w1 == 3)
            {
                select.AddRange(Three1);
                select.AddRange(Three2);
                foreach (var item in Three2)
                {
                    cards.Remove(item);
                }
                List<Card> single = FindSingle(cards, -1);
                foreach (var item in single)
                {
                    cards.Remove(item);
                }
                List<Card> single1 = FindSingle(cards, -1);
                select.AddRange(single);
                select.AddRange(single1);
                break;
            }
            else
            {
                i++;
                continue;
            }
            
        }
        return select;
    }

    /// <summary>
    /// 找最小的牌
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    private List<Card> FindSmallestCard(List<Card> cards)
    {
        List<Card> select = new List<Card>();
        //先出顺子
        for (int i = 12; i >= 5; i--)
        {
            select = FindStraigtht(cards, -1, i);
            if (select.Count != 0 && select.Count >= 5)
            {
                currType = CardType.Straigtht;
                break;
            }
        }
        //三带二
        if (select.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                select = FindThreeAndTwo(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CardType.ThreeAndTwo;
                    break;
                }
            }
        }
        //三带一
        if (select.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                select = FindThreeAndOne(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CardType.ThreeAndOne;
                    break;
                }
            }
        }
        //三不带
        if (select.Count == 0)
        {
            for (int i = 0; i < 36; i += 3)
            {
                select = FindThree(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CardType.Three;
                    break;
                }
            }
        }
        //对儿
        if (select.Count == 0)
        {
            for (int i = 0; i < 24; i += 2)
            {
                select = FindDouble(cards, i - 1);
                if (select.Count != 0)
                {
                    currType = CardType.Double;
                    break;
                }
            }
        }
        //单牌
        if (select.Count == 0)
        {
            select = FindSingle(cards, -1);
            currType = CardType.Single;

        }

        return select;
    }

    /// <summary>
    /// 找炸弹
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    private List<Card> FindBoom(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count - 4; i++)
        {
            if (cards[i].CardWeight == cards[i + 1].CardWeight && cards[i].CardWeight == cards[i + 2].CardWeight
                && cards[i].CardWeight == cards[i + 3].CardWeight)
            {
                int totalweight = (int)cards[i].CardWeight + (int)cards[i + 1].CardWeight +
                    (int)cards[i + 2].CardWeight + (int)cards[i + 3].CardWeight;
                if (totalweight > weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    select.Add(cards[i + 2]);
                    select.Add(cards[i + 3]);
                    break;
                }
            }
        }

        return select;
    }
    /// <summary>
    /// 找王炸
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public List<Card> FindJokerBoom(List<Card> cards)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count - 1; i++)
        {
            if (cards[i].CardWeight == Weight.SJoker && cards[i + 1].CardWeight == Weight.LJoker)
            {
                select.Add(cards[i]);
                select.Add(cards[i + 1]);
                break;
            }
        }
        return select;
    }
    /// <summary>
    /// 找三带二
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    private List<Card> FindThreeAndTwo(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        List<Card> three = FindThree(cards, weight);
        if (three.Count != 0)
        {
            foreach (var item in three)
            {
                cards.Remove(item);
            }
            List<Card> two = FindDouble(cards, -1);
            if (two.Count != 0)
            {
                select.AddRange(three);
                select.AddRange(two);
            }

        }
        return select;
    }
    /// <summary>
    /// 找三带一
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    private List<Card> FindThreeAndOne(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        List<Card> three = FindThree(cards, weight);
        if (three.Count != 0)
        {
            foreach (var item in three)
            {
                cards.Remove(item);
            }
            List<Card> one = FindSingle(cards, -1);
            if (one.Count != 0)
            {
                select.AddRange(three);
                select.AddRange(one);
            }
        }
        return select;
    }
    /// <summary>
    /// 找三不带
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    private List<Card> FindThree(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count - 3; i++)
        {
            if (cards[i].CardWeight == cards[i + 1].CardWeight && cards[i].CardWeight == cards[i + 2].CardWeight)
            {
                int totalweight = (int)cards[i].CardWeight + (int)cards[i + 1].CardWeight + (int)cards[i + 2].CardWeight;
                if (totalweight > weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    select.Add(cards[i + 2]);
                    break;
                }
            }
        }
        return select;
    }

    /// <summary>
    /// 找三联顺
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    private List<Card> FindTribleStraight(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();




        return select;
    }
    /// <summary>
    /// 找双连顺、连对
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="miniweight"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    private List<Card> FindDoubleStraight(List<Card> cards, int minWeight, int length)
    {
        List<Card> select = new List<Card>();
        int counter = 0;
        List<int> indexList = new List<int>();
        //334455找667788，手牌为566777888
        for (int i = 0; i < cards.Count - 4; i++)
        {
            int weight = (int)cards[i].CardWeight;
            if (weight > minWeight)
            {
                counter = 0;
                indexList.Clear();
                int temp = 0;
                for (int j = i + 1; j < cards.Count; j++)
                {
                    if (cards[j].CardWeight > Weight.One)//不超过A
                        break;

                    if ((int)cards[j].CardWeight - weight == counter)
                    {
                        temp++;
                        if (temp % 2 == 1)
                        {
                            counter++;
                        }
                        indexList.Add(j);
                    }
                    if (counter == length / 2)
                        break;
                }
            }
            if (counter == length / 2)
            {
                indexList.Insert(0, i);
                break;
            }
        }
        if (counter == length / 2)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                select.Add(cards[indexList[i]]);
            }
        }

        return select;
    }
    /// <summary>
    /// 找顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="miniweight"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    private List<Card> FindStraigtht(List<Card> cards, int miniweight, int length)
    {
        List<Card> select = new List<Card>();
        int counter = 1;
        List<int> indexList = new List<int>();
        for (int i = 0; i <= cards.Count - 4; i++) //3 4 5 6 7 
        {
            int weight = (int)cards[i].CardWeight;
            if (weight > miniweight)
            {
                counter = 1;
                indexList.Clear();
                for (int j = i + 1; j < cards.Count; j++)
                {
                    if (cards[j].CardWeight > Weight.One)
                    {
                        break;
                    }
                    if ((int)cards[j].CardWeight - weight == counter)
                    {
                        counter++;
                        indexList.Add(j);
                    }
                    if (counter == length)
                    {
                        break;
                    }
                }
            }
            if (counter == length)
            {
                indexList.Insert(0, i);
                break;
            }
        }
        if (counter == length)
        {
            for (int i = 0; i < indexList.Count; i++)
            {
                select.Add(cards[indexList[i]]);
            }
        }
        return select;
    }

    /// <summary>
    /// 找对儿
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    private List<Card> FindDouble(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count - 1; i++)
        {
            if (cards[i].CardWeight == cards[i + 1].CardWeight)
            {
                int totalweight = (int)cards[i].CardWeight + (int)cards[i + 1].CardWeight;
                if (totalweight > weight)
                {
                    select.Add(cards[i]);
                    select.Add(cards[i + 1]);
                    break;
                }
            }
        }
        return select;
    }
    /// <summary>
    /// 找单牌
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="weight"></param>
    /// <returns></returns>
    private List<Card> FindSingle(List<Card> cards, int weight)
    {
        List<Card> select = new List<Card>();
        for (int i = 0; i < cards.Count; i++)
        {
            if ((int)cards[i].CardWeight > weight)
            {
                select.Add(cards[i]);
                break;
            }
        }



        return select;
    }



}

