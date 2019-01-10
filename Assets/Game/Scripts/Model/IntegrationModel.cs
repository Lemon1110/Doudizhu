using UnityEngine;
using System.Collections;

/// <summary>
/// 积分
/// </summary>
public class IntegrationModel
{
    /// <summary>
    /// 底分
    /// </summary>
    public int BasePoint;
    /// <summary>
    /// 倍数
    /// </summary>
    public int Mutiples;

    /// <summary>
    /// 当前分数
    /// </summary>
    public int Result {
        get
        {
            return Mutiples * BasePoint;
        }
    }

    /// <summary>
    /// 玩家积分
    /// </summary>
    private int playerIntegration;
    public int PlayerIntegration
    {
        set
        {
            if (value <= 0)
            {
                playerIntegration = 0;
            }
            else
            {
                playerIntegration = value;
            }
        }
        get
        {
            return playerIntegration;
        }
    }
    private int computerLeftIntegration;
    public int ComputerLeftIntegration
    {
        set
        {
            if (value <= 0)
            {
                computerLeftIntegration = 0;
            }
            else
            {
                computerLeftIntegration = value;
            }
        }
        get
        {
            return computerLeftIntegration;
        }
    }

    private int computerRightIntegration ;
    public int ComputerRightIntegration
    {
        set
        {
            if (value <= 0)
            {
                computerRightIntegration = 0;
            }
            else
            {
                computerRightIntegration = value;
            }
        }
        get
        {
            return computerRightIntegration;
        }
    }
    /// <summary>
    /// 初始化积分
    /// </summary>
    public void InitIntegration()
    {
        PlayerIntegration =2000;
        ComputerLeftIntegration =3000;
        ComputerRightIntegration = 4000;
        Mutiples = 1;
        BasePoint = 100;
    }

}
