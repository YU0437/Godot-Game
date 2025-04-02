using Godot;

public partial class EnemyManager : Node2D
{
    [ExportCategory("怪物场景")] [Export] public PackedScene EnemyScene;
    [Export] public GameManager GoldSystem;
    
    private void EnemyRefresh() //Timer的信号
    {
        var enemy = EnemyScene.Instantiate<Enemy>(); //生成一个怪物场景,由Enemy脚本提供其属性
        enemy.Init(GoldSystem); // 绑定回调接收者
        var screenSize = GetViewport().GetVisibleRect().Size;
        enemy.Position = new Vector2(280, GD.RandRange(10, 100));
        GetTree().CurrentScene.AddChild(enemy);
    }
}