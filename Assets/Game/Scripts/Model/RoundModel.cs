using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// 出牌回合类
/// </summary>
public class RoundModel
{
    /// <summary>
    /// 玩家出牌事件
    /// </summary>
    public static event Action<bool> PlayerHandler;
    /// <summary>
    /// 电脑出牌事件
    /// </summary>
    public static event Action<ComputerSmartArgs> ComputerHandler;
   

    private CharacterType biggesterCharacter;//最大出牌者
    private CharacterType currentCharacter;//当前角色
    private CardType currentType;//牌的类型
    private int currentWeight;//当前权值
    private int currentLength;//当前长度

    /// <summary>
    /// 当前长度
    /// </summary>
    public int Length {
        get { return currentLength; }
        set { currentLength = value; }
    }
    /// <summary>
    /// 当前权值
    /// </summary>
    public int weight
    {
        get { return currentWeight; }
        set { currentWeight = value; }
    }
    /// <summary>
    /// 当前牌的类型
    /// </summary>
    public CardType CardType
    {
        get { return currentType; }
        set { currentType = value; }
    }
    /// <summary>
    /// 最nb的出牌者
    /// </summary>
    public CharacterType Biggest
    {
        get { return biggesterCharacter; }
        set { biggesterCharacter = value; }
    }
    /// <summary>
    /// 当前出牌者
    /// </summary>
    public CharacterType Current
    {
        get { return currentCharacter; }
        set { currentCharacter = value; }
    }

    /// <summary>
    /// 初始化回合信息
    /// </summary>
    public void InitRound()
    {
        this.Current = CharacterType.Desk;
        this.Biggest = CharacterType.Desk;
        this.CardType = CardType.None;
        this.weight = 0;
        this.Length = 0;

        
    }
    /// <summary>
    /// 开始游戏，出牌
    /// </summary>
    /// <param name="ctype"></param>
    public void Start(CharacterType ctype)
    {
        this.currentCharacter = ctype;
        this.biggesterCharacter = ctype;
        BeginWith(ctype);

    }
    /// <summary>
    /// 轮换出牌
    /// </summary>
    public void turn()
    {
        currentCharacter++;

        if (currentCharacter == CharacterType.Library || currentCharacter == CharacterType.Desk)
        {
            currentCharacter = CharacterType.Player;
        }
        BeginWith(currentCharacter);
    }
    /// <summary>
    /// 开始出牌
    /// </summary>
    /// <param name="currentCharacter"></param>
    private void BeginWith(CharacterType currentCharacter)
    {
        if (currentCharacter == CharacterType.Player)
        {
            //玩家出牌
            if (PlayerHandler != null)
                PlayerHandler(biggesterCharacter!=CharacterType.Player);
        }
        else
        {
            //电脑自动出牌
            if (ComputerHandler != null)
            {
                //CharacterType 出牌者,CardType牌的类型,int权值,int长度
                ComputerSmartArgs e = new ComputerSmartArgs
                {
                    biggest = this.biggesterCharacter,
                    cardType = this.CardType,
                    characterType = this.Current,
                    length = this.Length,
                    weight = this.weight
                };
                ComputerHandler(e);
               
            }
               
        }
    }
}