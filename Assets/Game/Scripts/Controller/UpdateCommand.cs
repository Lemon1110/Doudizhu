using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// 更新积分
/// </summary>
public class UpdateCommand:EventCommand
{
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }
    public override void Execute()
    {
        GameData data = new GameData();
        data.PlayerIntegration = IntegrationModel.PlayerIntegration;
        data.ComputerLeftIntegration = IntegrationModel.ComputerLeftIntegration;
        data.ComputerRightIntegration = IntegrationModel.ComputerRightIntegration;
        dispatcher.Dispatch(ViewEvent.UPDATE_INTEGRATION, data);
    }

}

