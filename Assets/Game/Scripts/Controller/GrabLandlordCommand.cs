using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// 抢地主
/// </summary>
public  class GrabLandlordCommand:EventCommand
{
    [Inject]
    public IntegrationModel IntegrationModel { get; set; }
    [Inject]
    public RoundModel RoundModel { get; set; }
    public override void Execute()
    {
        GrabLandlordArgs e = this.evt.data as GrabLandlordArgs;
        IntegrationModel.Mutiples *= 2;

        //发底牌
        dispatcher.Dispatch(ViewEvent.DEAL_THREECARD,e);
        //开始游戏
        RoundModel.Start(e.ctype);
    }


}
