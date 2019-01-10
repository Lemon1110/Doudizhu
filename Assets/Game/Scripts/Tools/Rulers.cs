using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏规则类
/// </summary>
public static class Rulers
{
    /// <summary>
    /// 是否是单牌
    /// </summary>
    /// <param name="cards">选择的手牌</param>
    /// <returns></returns>
    public static bool IsSingle(List<Card> cards)
    {
        return cards.Count == 1;
    }
    /// <summary>
    /// 是否是对
    /// </summary>
    /// <param name="cards">选择的手牌</param>
    /// <returns></returns>
    public static bool IsDouble(List<Card> cards)
    {
        if (cards.Count == 2)
        {
            if (cards[0].CardWeight == cards[1].CardWeight)
            {
                return true;
            }
        }

        return false;
    }
    /// <summary>
    /// 是否是顺子
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsStraight(List<Card> cards)
    {
        if (cards.Count < 5 || cards.Count > 12)
        {
            return false;
        }
        for (int i = 0; i < cards.Count - 1; i++)
        {
            Weight tempWeight = cards[i].CardWeight;
            if (cards[i + 1].CardWeight - tempWeight != 1) // 3 4 5 6 7
            {
                return false;
            }
            //不能超过A
            if (tempWeight > Weight.One || cards[i + 1].CardWeight > Weight.One)// J Q K A 2 
            {
                return false;
            }
        }
        return true;

    }
    /// <summary>
    /// 是否是连对
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsDoubleStraight(List<Card> cards)//33 44 55
    {
        if (cards.Count < 6 || cards.Count % 2 != 0)
        {
            return false;
        }
        for (int i = 0; i <= cards.Count - 2; i += 2)
        {
            if (cards[i].CardWeight != cards[i + 1].CardWeight)
            {
                return false;
            }

            if (i + 2 < cards.Count)
            {
                if (cards[i + 2].CardWeight - cards[i].CardWeight != 1)
                {
                    return false;
                }
                //不能超过A
                if (cards[i].CardWeight > Weight.One || cards[i + 2].CardWeight > Weight.One)
                {
                    return false;
                }
            }

        }
        return true;
    }
    /// <summary>
    /// 是否是三连顺
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsTribleStraight(List<Card> cards)
    {
        if (cards.Count < 6 || cards.Count % 3 != 0)
        {
            return false;
        }
        for (int i = 0; i < cards.Count - 3; i += 3) //333444 i=0，
        {//333444555 i =0,3,
            
            if (cards[i].CardWeight == cards[i + 1].CardWeight &&
                cards[i + 1].CardWeight == cards[i + 2].CardWeight &&
                cards[i + 3].CardWeight == cards[i + 4].CardWeight &&
                cards[i + 4].CardWeight == cards[i + 5].CardWeight)
            {
                if (cards[i].CardWeight < Weight.One)
                {
                    if (cards[i + 3].CardWeight - cards[i].CardWeight == 1
                   && cards[i + 3].CardWeight < Weight.One)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    /// <summary>
    /// 是否是三不带
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThree(List<Card> cards)
    {
        if (cards.Count != 3) return false;
        if (cards[0].CardWeight != cards[1].CardWeight)
            return false;
        if (cards[2].CardWeight != cards[1].CardWeight)
            return false;
        if (cards[0].CardWeight != cards[2].CardWeight)
            return false;
        return true;
    }
    /// <summary>
    /// 是否是三带一
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThreeAndOne(List<Card> cards)
    {

        if (cards.Count != 4)
        {
            return false;
        }
        if (cards[0].CardWeight == cards[1].CardWeight && cards[1].CardWeight == cards[2].CardWeight)
        {
            return true;
        }
        else if (cards[1].CardWeight == cards[2].CardWeight && cards[2].CardWeight == cards[3].CardWeight)
            return true;
        return false;
    }
    /// <summary>
    /// 是否是三带二
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsThreeAndTwo(List<Card> cards)
    {
        if (cards.Count != 5)
        {
            return false;
        }
        if (cards[0].CardWeight == cards[1].CardWeight && cards[1].CardWeight == cards[2].CardWeight)
        {
            if (cards[3].CardWeight == cards[4].CardWeight)
                return true;
        }
        else if (cards[2].CardWeight == cards[3].CardWeight && cards[3].CardWeight == cards[4].CardWeight)
        {
            if (cards[0].CardWeight == cards[1].CardWeight)
                return true;
        }
        return false;

    }

    /// <summary>
    /// 是否是飞机
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsPlane(List<Card> cards)
    {
        if (cards.Count != 8)
        {
            return false;
        }
        for (int i = 0; i < 3; i++)
        {
            if (cards[i].CardWeight == cards[i + 1].CardWeight && cards[i + 1].CardWeight == cards[i + 2].CardWeight)
            {
                if (cards[i + 3].CardWeight - cards[i].CardWeight == 1 & cards[i + 3].CardWeight == cards[i + 4].CardWeight
                 && cards[i + 4].CardWeight == cards[i + 5].CardWeight)
                    return true;
            }
        }

        return false;
    }
    /// <summary>
    /// 是否是四帯二
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsFourAndTwoSingle(List<Card> cards)
    {
        if (cards.Count != 6)
        {
            return false;
        }
        if (cards[0].CardWeight == cards[1].CardWeight && cards[1].CardWeight == cards[2].CardWeight
            && cards[2].CardWeight == cards[3].CardWeight)
        {
            return true;
        }
        else if (cards[2].CardWeight == cards[3].CardWeight && cards[3].CardWeight == cards[4].CardWeight
            && cards[4].CardWeight == cards[5].CardWeight)
            return true;
        return false;
    }

    /// <summary>
    /// 是否是是四带两对
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsFourAndTwoDouble(List<Card> cards)
    {

        if (cards.Count != 8)
        {
            return false;
        }
        if (cards[0].CardWeight == cards[1].CardWeight && cards[1].CardWeight == cards[2].CardWeight
           && cards[2].CardWeight == cards[3].CardWeight)
        {
            if (cards[4].CardWeight == cards[5].CardWeight && cards[6].CardWeight == cards[7].CardWeight)
                return true;
        }
        else if (cards[2].CardWeight == cards[3].CardWeight && cards[3].CardWeight == cards[4].CardWeight
           && cards[4].CardWeight == cards[5].CardWeight)
        {
            if (cards[0].CardWeight == cards[1].CardWeight && cards[6].CardWeight == cards[7].CardWeight)
                return true;
        }
        else if (cards[4].CardWeight == cards[5].CardWeight && cards[5].CardWeight == cards[6].CardWeight
            && cards[6].CardWeight == cards[7].CardWeight)
        {
            if (cards[0].CardWeight == cards[1].CardWeight && cards[2].CardWeight == cards[3].CardWeight)
                return true;
        }


        return false;
    }

    /// <summary>
    /// 是否是炸弹
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsBoom(List<Card> cards)
    {

        if (cards.Count != 4)
        {
            return false;
        }
        if (cards[0].CardWeight != cards[1].CardWeight)
            return false;
        if (cards[1].CardWeight != cards[2].CardWeight)
            return false;
        if (cards[2].CardWeight != cards[3].CardWeight)
            return false;
        return true;
    }
    /// <summary>
    /// 是否是王炸
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool IsJokerBoom(List<Card> cards)
    {
        if (cards.Count != 2)
        {
            return false;
        }
        if (cards[0].CardWeight == Weight.SJoker && cards[1].CardWeight == Weight.LJoker)
        {
            return true;
        }
        else if (cards[1].CardWeight == Weight.SJoker && cards[0].CardWeight == Weight.LJoker)
        {
            return true;
        }

        return false;
    }
    /// <summary>
    /// 判断是否能出牌
    /// </summary>
    /// <param name="cards">要出的牌</param>
    /// <param name="type">出牌的类型</param>
    /// <returns></returns>
    public static bool CanPop(List<Card> cards, out CardType type)
    {
        type = CardType.None;
        bool can = false;

        switch (cards.Count)
        {
            case 1:
                if (IsSingle(cards))
                {
                    type = CardType.Single;
                    can = true;
                }

                break;
            case 2:

                if (IsDouble(cards))
                {
                    type = CardType.Double;
                    can = true;
                }
                else if (IsJokerBoom(cards))
                {
                    type = CardType.JokerBoom;
                    can = true;
                }
                break;
            case 3:
                if (IsThree(cards))
                {
                    type = CardType.Three;
                    can = true;
                }
                break;
            case 4:
                if (IsBoom(cards))
                {
                    type = CardType.Boom;
                    can = true;
                }
                else if (IsThreeAndOne(cards))
                {
                    type = CardType.ThreeAndOne;
                    can = true;
                }
                break;
            case 5:
                if (IsStraight(cards))
                {
                    type = CardType.Straigtht;
                    can = true;
                }
                else if (IsThreeAndTwo(cards))
                {
                    type = CardType.ThreeAndTwo;
                    can = true;
                }


                break;
            case 6:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    can = true;
                }
                else if (IsFourAndTwoSingle(cards))
                {
                    type = CardType.FourAndTwoSingle;
                    can = true;
                }
                else if (IsStraight(cards))
                {
                    type = CardType.Straigtht;
                    can = true;
                }
                else if (IsTribleStraight(cards))
                {
                    type = CardType.TribleStraight;
                    can = true;
                }

                break;
            case 7:
                if (IsStraight(cards))
                {
                    type = CardType.Straigtht;
                    can = true;
                }
                break;
            case 8:
                if (IsStraight(cards))
                {
                    type = CardType.Straigtht;
                    can = true;
                }
                else if (IsPlane(cards))
                {
                    type = CardType.Plane;
                    can = true;
                }
                else if (IsFourAndTwoDouble(cards))
                {
                    type = CardType.FourAndTwoDouble;
                    can = true;
                }
                else if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    can = true;
                }
                break;
            case 9:
                if (IsStraight(cards))
                {
                    type = CardType.Straigtht;
                    can = true;
                }
                else if (IsTribleStraight(cards))
                {
                    type = CardType.TribleStraight;
                    can = true;
                }
                break;
            case 10:
                if (IsStraight(cards))
                {
                    type = CardType.Straigtht;
                    can = true;
                }
                else if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    can = true;
                }
                break;
            case 11:
                if (IsStraight(cards))
                {
                    type = CardType.Straigtht;
                    can = true;
                }

                break;
            case 12:
                if (IsStraight(cards))
                {
                    type = CardType.Straigtht;
                    can = true;
                }
                else if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    can = true;
                }
                else if (IsTribleStraight(cards))
                {
                    type = CardType.TribleStraight;
                    can = true;
                }
                break;
            case 13:

                break;
            case 14:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    can = true;
                }
                break;
            case 15:
                if (IsTribleStraight(cards))
                {
                    type = CardType.TribleStraight;
                    can = true;
                }
                break;
            case 16:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    can = true;
                }
                break;
            case 17:
                break;
            case 18:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    can = true;
                }
                else if (IsTribleStraight(cards))
                {
                    type = CardType.TribleStraight;
                    can = true;
                }
                break;
            case 19:
                break;
            case 20:
                if (IsDoubleStraight(cards))
                {
                    type = CardType.DoubleStraight;
                    can = true;
                }
                break;


            default:
                break;
        }
        Debug.Log(type);
        return can;
    }
}
