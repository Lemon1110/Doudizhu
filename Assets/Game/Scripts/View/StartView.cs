using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using strange.extensions.mediation.impl;

/// <summary>
/// 开始游戏View
/// </summary>
public class StartView : EventView {
    private Button btn_One;
    private Button btn_Two;
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        btn_One = transform.Find("Btn_One").GetComponent<Button>();
        btn_Two = transform.Find("Btn_Two").GetComponent<Button>();
        //注册点击事件
        btn_One.onClick.AddListener(onOneClick);
        btn_Two.onClick.AddListener(onTwoClick);
    }
    /// <summary>
    /// 移除点击事件
    /// </summary>
    public void ViewDestroy()
    {
        btn_One.onClick.RemoveListener(onOneClick);
        btn_Two.onClick.RemoveListener(onTwoClick);
    }
    /// <summary>
    /// 单倍按钮点击
    /// </summary>
    private void onOneClick()
    {
        //更改Intergration的倍数为1
        dispatcher.Dispatch(ViewEvent.CHANGE_MUTIPLE, 1);

        //删除面板
        Destroy(gameObject);


    }
    /// <summary>
    /// 双倍按钮点击
    /// </summary>
    public void onTwoClick()
    {
       
        //更改Intergration的倍数为2
        dispatcher.Dispatch(ViewEvent.CHANGE_MUTIPLE, 2);
        //删除面板
        Destroy(gameObject);
    }
}
