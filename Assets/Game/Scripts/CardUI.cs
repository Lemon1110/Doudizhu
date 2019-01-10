﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
/// <summary>
/// 卡牌UI类
/// </summary>
public class CardUI : ReuseableObject, IPointerClickHandler
{
    /// <summary>
    /// 显示图片
    /// </summary>
    private Image image;
    private Card card;
    private bool isSelected;
    /// <summary>
    /// 卡牌的信息
    /// </summary>
    public Card Card
    {
        get { return card; }
        set
        {
            card = value;
            SetImage();
        }
    }
    /// <summary>
    /// 显示图片
    /// </summary>
    private void SetImage()
    {
        if (card.BelongTo == CharacterType.Player || card.BelongTo == CharacterType.Desk)
        {
            Sprite s = Resources.Load<Sprite>("Pokers/" + card.CardName);
            image.sprite = s;

        }
        else
        {
            Sprite s = Resources.Load<Sprite>("Pokers/CardBack");
            image.sprite = s;
        }
    }



    /// <summary>
    /// 是否被选中
    /// </summary>
    /// <returns></returns>
    public bool Selected
    {
        get { return isSelected; }
        set
        {
            if (value == isSelected) return;
            if (value == true)
            {
                transform.localPosition += Vector3.up * 10;
            }
            else
            {
                transform.localPosition -= Vector3.up * 10;
            }
            isSelected = value;
        }

    }

    /// <summary>
    /// 点击事件
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (card.BelongTo == CharacterType.Player)
            {
                Selected = !Selected;
            }

        }
    }
    /// <summary>
    /// 销毁卡牌
    /// </summary>
    public void Destroy()
    {
        PoolManager.Instance.HideObject(gameObject);
    }

    /// <summary>
    /// 每次创建时会调用
    /// </summary>
    public override void BeforeGetObject()
    {

        image = GetComponent<Image>();
    }
    /// <summary>
    /// 每次销毁时会调用
    /// </summary>
    public override void BeforeHideObject()
    {
        isSelected = false;
        image.sprite = null;
        card = null;

    }
    /// <summary>
    /// 设置卡牌位置
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="Index"></param>
    public void SetPosition(Transform parent, int index)
    {
        transform.SetParent(parent, false);
        transform.SetSiblingIndex(index);
        if (card.BelongTo == CharacterType.Desk || card.BelongTo == CharacterType.Player)
        {
            transform.localPosition = Vector3.right * 25 * index;
            if (isSelected)
            {
                transform.localPosition += Vector3.up * 10;
            }
        }
        else if (card.BelongTo == CharacterType.ComputerLeft || card.BelongTo == CharacterType.ComputerRight)
        {
            transform.localPosition = -Vector3.up * 15 * index;
        }
    }
}
