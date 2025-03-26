using Godot;

public partial class EnemyManager : Node2D
{
    [ExportCategory("怪物场景")] [Export] public PackedScene EnemyScene;
    [ExportCategory("敌人死亡接收")] [Export] public GameManager GameSystem; //游戏系统接收
    [ExportCategory("玩家受伤接收")] [Export] public PlayerManager PlayerSystem; //玩家系统接收

    private void EnemyRefresh() //Timer的信号
    {
        var enemy = EnemyScene.Instantiate<Enemy>(); //生成一个怪物场景,由Enemy脚本提供其属性
        enemy.Init(GameSystem); // 绑定回调接收者(谁来接收敌人的死亡通知)
        enemy.Init(PlayerSystem); // 绑定回调接收者(谁来接收玩家的受伤通知)
        // var screenSize = GetViewport().GetVisibleRect().Size;
        enemy.Position = new Vector2(GD.RandRange(-100, 100), GD.RandRange(-100, 100));
        GetTree().CurrentScene.AddChild(enemy);
    }
}