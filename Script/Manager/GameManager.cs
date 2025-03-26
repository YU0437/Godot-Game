using Godot;

public partial class GameManager : Node, IEnemyDeathListener
{
    private Timer batchTimer; // 定时处理未处理的金币数量
    [ExportCategory("总金币")] [Export] private int GlobalGold;
    private int PendingGold; //待处理的金币数量

    public void OnEnemyDeath(Enemy enemy, int gold, Vector2 deathPos)
    {
        PendingGold += gold;
    }

    public override void _Ready()
    {
        batchTimer = new Timer(); //初始化计时器
        batchTimer.Autostart = true;
        batchTimer.WaitTime = 0.5f; //0.5s刷新一次
        batchTimer.Timeout += FlushGold;
        AddChild(batchTimer);
    }

    private void FlushGold() //更新UI
    {
        if (PendingGold == 0) return;
        GlobalGold += PendingGold;
        PendingGold = 0;
        GlobalSignals.EmitGoldUiUpdate(GlobalGold);
    }
}