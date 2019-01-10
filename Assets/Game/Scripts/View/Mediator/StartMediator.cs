using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//中介
public class StartMediator : EventMediator
{

    [Inject]
    public StartView StartView { get; set; }//反射？？
    public override void OnRegister()//注册
    {
        //base.OnRegister();
        StartView.Init();

        StartView.dispatcher.AddListener(ViewEvent.CHANGE_MUTIPLE,onViewClick);

    }
    /// <summary>
    /// 移除函数
    /// </summary>
    public override void OnRemove()
    {
        StartView.ViewDestroy();
        //base.OnRemove();
        StartView.dispatcher.RemoveListener(ViewEvent.CHANGE_MUTIPLE, onViewClick);
    }
    /// <summary>
    /// View被点击时候调用
    /// </summary>
    /// <param name="evt"></param>
    public void onViewClick(IEvent evt)
    {
        int mutiple = (int)evt.data;
        //发送出去
        dispatcher.Dispatch(CommandEvent.ChangeMutiple, mutiple);
    }
}

