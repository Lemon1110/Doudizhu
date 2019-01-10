using UnityEngine;
using System.Collections;


public class Consts
{
    /// <summary>
    /// 游戏数据保存路径
    /// </summary>
    public static readonly string DataPath = Application.persistentDataPath + @"/data.xml";//存文件的路径，可以跨平台
    //Application.dataPath + @"/data.xml"; //
}
/// <summary>
/// View事件
/// </summary>
public enum ViewEvent
{
    CHANGE_MUTIPLE,//改变倍数
    COMPLETE_DEAL,//完成发牌
    DEAL_THREECARD,//发底牌
    REQUEST_PLAY,//请求出牌
    SUCCESS_PLAY,//成功出牌
    COMPLETE_PLAY,//完成出牌
    RESTART_GAME,//重新开始
    UPDATE_INTEGRATION,//更新分数UI
    
}
/// <summary>
/// Command事件
/// </summary>
public enum CommandEvent
{
    ChangeMutiple,//改变倍数
    RequestDeal,//请求发牌
    DealCard,//发牌
    GrabLandlord, //抢地主事件
    PlayCard,//出牌
    PassCard,//不出
    GameOver,//游戏结束
    RequestUpdate,//请求开始时更新分数显示
}
/// <summary>
/// 面板类型
/// </summary>
public enum PanelType
{
    StartPanel,
    BackGroundPanel,
    CharacterPanel,
    InteractionPanel,
    GameOverPanel,
}

/// <summary>
/// 角色类型
/// </summary>
public enum CharacterType//牌库 电脑 玩家 桌子
{
    Library = 0,
    Player = 1,
    ComputerRight = 2,
    ComputerLeft = 3,
    Desk
}
/// <summary>
/// 卡牌花色
/// </summary>
public enum Colors
{
    None,
    Club,//梅花
    Heart,//红桃
    Spade,//黑桃
    Square//方块

}
/// <summary>
/// 卡牌权值
/// </summary>
// 3 4 5 6 7 8 9 10 J Q K A 2 Small Big

public enum Weight
{
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,//8   
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    One,
    Two,
    SJoker,
    LJoker
}
//出牌类型
public enum CardType
{
    None,
    Single,//单 
    Double,//双
    Straigtht,//顺子
    DoubleStraight,//双顺子
    TribleStraight,//三顺子
    Three,//三不带
    ThreeAndOne,//三带一
    ThreeAndTwo,//三带二
    Plane,//飞机
    FourAndTwoSingle,//四帯二
    FourAndTwoDouble,//四带两对
    Boom,//炸弹
    JokerBoom//王炸
}
/// <summary>
/// 身份
/// </summary>
public enum Identity
{
    Farmer,//农民 
    Landlord //地主
}
