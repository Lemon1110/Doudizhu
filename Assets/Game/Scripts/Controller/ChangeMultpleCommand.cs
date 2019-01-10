using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;

/// <summary>
/// 改变倍数的命令
/// </summary>
public class ChangeMultpleCommand :EventCommand
{
    [Inject] //千万千万不能掉！！！掉了就报空！！
    public IntegrationModel integrationModel { get; set; }
    public override void Execute()
    {
        
        int mutiple = (int)evt.data;//接受

        //base.Execute();
        integrationModel.Mutiples = mutiple;

        Tools.CreateUIPanel(PanelType.BackGroundPanel);
        Tools.CreateUIPanel(PanelType.CharacterPanel);
        Tools.CreateUIPanel(PanelType.InteractionPanel);
    }


}
