using strange.extensions.context.api;
using strange.extensions.context.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameContext: MVCSContext
{
    public GameContext(MonoBehaviour view, bool autoMapping) : base(view, autoMapping)
    {


    }
    /// <summary>
    /// 绑定映射
    /// </summary>
    protected override void mapBindings()
    {
        //base.mapBindings();
        Debug.Log("启动框架");

        //三种绑定
        injectionBinder.Bind<IntegrationModel>().To<IntegrationModel>().ToSingleton();//绑定并单例
        injectionBinder.Bind<CardModel>().To<CardModel>().ToSingleton();
        injectionBinder.Bind<RoundModel>().To<RoundModel>().ToSingleton();

        mediationBinder.BindView<StartView>().ToMediator<StartMediator>();//绑定开始视图
        mediationBinder.BindView<InteractionView>().ToMediator<InteractionMediator>();//绑定视图
        mediationBinder.BindView<CharacterView>().ToMediator<CharacterMediator>();//绑定角色视图

        commandBinder.Bind(CommandEvent.ChangeMutiple).To<ChangeMultpleCommand>();//绑定改变倍数command
        commandBinder.Bind(CommandEvent.RequestDeal).To<RequestDealCommand>();//绑定请求发牌command
        commandBinder.Bind(CommandEvent.GrabLandlord).To<GrabLandlordCommand>();//绑定抢地主command
        commandBinder.Bind(ContextEvent.START).To<StartCommand>().Once();//绑定开始命令
        commandBinder.Bind(CommandEvent.PlayCard).To<PlayCardCommand>();//绑定出牌命令
        commandBinder.Bind(CommandEvent.RequestUpdate).To<UpdateCommand>();//绑定更新界面分数命令
        commandBinder.Bind(CommandEvent.PassCard).To<PassCardCommand>();//绑定不出牌命令
        commandBinder.Bind(CommandEvent.GameOver).To<GameOverCommad>();//绑定游戏结束命令    
    }
    }

