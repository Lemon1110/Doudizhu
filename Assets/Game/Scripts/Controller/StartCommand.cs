using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using System.IO;

/// <summary>
/// 开始游戏
/// </summary>
public class StartCommand : EventCommand {
    [Inject]
    public IntegrationModel integrationModel { get; set; }
    [Inject]
    public CardModel cardModel { get; set; }
    [Inject]
    public RoundModel roundModel { get; set; }
    public override void Execute()
    {
        Tools.CreateUIPanel(PanelType.StartPanel);
        //初始化Model
        integrationModel.InitIntegration();
        cardModel.InitCardLibrary();
        roundModel.InitRound();      
        //读取数据
        GetData();        
    }
    /// <summary>
    /// 读取数据
    /// </summary>
    public void GetData()
    {
        string fileName = Consts.DataPath;
        FileInfo fileInfo = new FileInfo(fileName);
        if (fileInfo.Exists)
        {
            GameData oldData = Tools.GetDataWithoutBom();
            integrationModel.ComputerLeftIntegration = oldData.ComputerLeftIntegration;
            integrationModel.ComputerRightIntegration = oldData.ComputerRightIntegration;
            integrationModel.PlayerIntegration = oldData.PlayerIntegration;
        }
        
    }
}
