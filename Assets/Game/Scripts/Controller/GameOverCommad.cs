using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
/// <summary>
/// 游戏结束
/// </summary>
public class GameOverCommad : EventCommand
{
    [Inject]
    public IntegrationModel integrationModel { get; set; }
    [Inject]
    public RoundModel RoundModel { get; set; }

    [Inject]
    public CardModel CardModel { get; set; }
    public override void Execute()
    {
        int result = integrationModel.Result;

        #region 保存数据
        GameOverArgs e = evt.data as GameOverArgs;
        if (e.PlayerWin)
        {
            integrationModel.PlayerIntegration += result;
        }
        else
        {
            integrationModel.PlayerIntegration -= result;
        }

        if (e.ComputerLeftWin)
        {
            integrationModel.ComputerLeftIntegration += result;
        }
        else
        {
            integrationModel.ComputerLeftIntegration -= result;
        }

        if (e.ComputerRightWin)
        {
            integrationModel.ComputerRightIntegration += result;
        }
        else
        {
            integrationModel.ComputerRightIntegration -= result;
        }

        GameData data = new GameData();
        data.PlayerIntegration = integrationModel.PlayerIntegration;
        data.ComputerLeftIntegration = integrationModel.ComputerLeftIntegration;
        data.ComputerRightIntegration = integrationModel.ComputerRightIntegration; 
        #endregion

        Tools.SaveData(data);
        //更新积分
        dispatcher.Dispatch(ViewEvent.UPDATE_INTEGRATION,data);

        CardModel.InitCardLibrary();
        RoundModel.InitRound();
        PoolManager.Instance.HideAllObject("Card");

        Debug.Log("游戏结束");
        //显示游戏结束面板
        Tools.CreateUIPanel(PanelType.GameOverPanel);

    }
}

