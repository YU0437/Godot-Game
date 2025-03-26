using Godot;

public partial class Shot : Node2D
{
    [ExportCategory("子弹类型")] [Export] public PackedScene BulletScene;
    [ExportCategory("枪口位置")] [Export] public Marker2D Muzzle;

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton { ButtonIndex: MouseButton.Left, Pressed: true } mouseEvent)
            Shoot(mouseEvent.Position); // 传递鼠标点击位置
    }

    private void Shoot(Vector2 mousePosition)
    {
        var bullet = BulletScene.Instantiate<Bullet>(); //生成一个子弹场景,类型为Bullet脚本
        bullet.Position = Muzzle.GlobalPosition;
        GetTree().CurrentScene.AddChild(bullet);
    }
}