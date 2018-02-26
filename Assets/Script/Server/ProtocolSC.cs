

public class ProtocolSC  {

    //登录请求
    public const int Sub_Cmd_LoginRqs = 1;
    //创建房间请求
    public const int Sub_Cmd_CreatRoomRqs = 2;
    //加入房间请求
    public const int Sub_Cmd_JoinRoomRqs = 3;
    //有其他玩家进入房间
    public const int Sub_Cmd_PLayerEnterRoom = 4;
    //房间人满开局
    public const int Sub_Cmd_PUSH_GAMESTART= 5;
    //服务器发牌
    public const int Sub_Cmd_PUSH_GIVECARD= 6;
    //出牌
    public const int Sub_Cmd_GAME_CHUPAI = 7;
    //准备完成
    public const int Sub_Cmd_GAME_READY = 8;
    //摸牌
    public const int Sub_Cmd_GAME_MOPAI = 9;

    //过牌
    public const int Sub_Cmd_GAME_GUOPAI = 10;
    //自摸
    public const int Sub_Cmd_GAME_ZIMO= 11;
    //碰牌
    public const int Sub_Cmd_GAME_PENGPAI = 12;
    //直杠牌  
    public const int Sub_Cmd_GAME_GANGPAI = 13;
    //吃胡
    public const int Sub_Cmd_GAME_HUPAI = 14;

    //明杠牌  
    public const int Sub_Cmd_GAME_MINGGANG = 15;
    //暗杠牌  
    public const int Sub_Cmd_GAME_ANGANG = 16;

    //抢杠
    public const int Sub_Cmd_GAME_QIANGGANG = 17;
    //小结算
    public const int Sub_Cmd_GAME_END = 18;


    //改名字
    public const int Sub_Cmd_HALL_CHANGENAME = 19;
    //改头像
    public const int Sub_Cmd_GAME_CHANGEICON = 20;
    //总结算
    public const int Sub_Cmd_GAME_OVER = 21;
    //流局
    public const int Sub_Cmd_GAME_LIUJU= 22;

}
