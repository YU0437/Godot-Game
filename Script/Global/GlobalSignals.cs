using System;

public static class GlobalSignals
{
    // 定义玩家受击信号
    public static event Action PlayerHit;

    //定义金币UI更新信号
    public static event Action<int> GoldUiUpdate;


    //定义一个方法,用来触发玩家受击事件,避免直接操作 PlayerHit
    public static void EmitPlayerHit()
    {
        PlayerHit?.Invoke(); // 安全调用，避免 null 异常
    }

    //金币UI更新
    public static void EmitGoldUiUpdate(int amount)
    {
        GoldUiUpdate?.Invoke(amount);
    }
}