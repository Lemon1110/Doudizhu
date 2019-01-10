﻿using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

/// <summary>
/// 发牌View
/// </summary>
public class CharacterView : View
{
    public PlayerControl player;
    public ComputerControl ComputerLeft;
    public ComputerControl ComputerRight;
    public DeskControl Desk;
    /// <summary>
    /// 初始化身份
    /// </summary>
    public void Init()
    {
        player.Identity = Identity.Farmer;
        ComputerLeft.Identity = Identity.Farmer;
        ComputerRight.Identity = Identity.Farmer;
    }
    /// <summary>
    /// 添加卡牌
    /// </summary>
    /// <param name="cType">给谁添加</param>
    /// <param name="card">添加的卡牌</param>
    /// <param name="selected">是否选中</param>
    public void AddCard(CharacterType cType,Card card,bool selected)
    {
        
        switch (cType)
        {
            
            case CharacterType.Player:
                player.AddCard(card, selected);

                break;
            case CharacterType.ComputerRight:
                ComputerRight.AddCard(card, selected);

                break;
            case CharacterType.ComputerLeft:
                ComputerLeft.AddCard(card, selected);
                break;
            case CharacterType.Desk:
                Desk.AddCard(card, selected);
                break;
            default:
                break;
        }

    }
    /// <summary>
    /// 发底牌
    /// </summary>
    /// <param name="cType">给谁发</param>
    public void AddThreeCard(CharacterType cType)
    {
        Card card = null;
        switch (cType)
        {
            case CharacterType.Player:
                for (int i = 0; i < 3; i++)
                {
                    card = Desk.DealCard();
                    player.AddCard(card,true);
                }
                player.Identity = Identity.Landlord;
                player.Sort(false);
                break;
            case CharacterType.ComputerRight:
                for (int i = 0; i < 3; i++)
                {
                    card = Desk.DealCard();
                    ComputerRight.AddCard(card, false);
                }
                ComputerRight.Identity = Identity.Landlord;
                ComputerRight.Sort(true);
                break;
            case CharacterType.ComputerLeft:
                for (int i = 0; i < 3; i++)
                {
                    card = Desk.DealCard();
                    ComputerLeft.AddCard(card, false);
                }
                ComputerLeft.Identity = Identity.Landlord;
                ComputerLeft.Sort(true);
                break;
           
            default:
                break;
        }

        Desk.Clear();
    }
	
}
