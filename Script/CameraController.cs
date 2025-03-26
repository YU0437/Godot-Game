using Godot;

public partial class CameraController : Camera2D
{
    [Export] public float FollowSpeed = 0.5f;
    private playerMove Target; // 拖拽赋值玩家节点

    public override void _Ready()
    {
        Target = GetParent<playerMove>();
    }

    public override void _PhysicsProcess(double delta)
    {
        // // 核心代码：平滑移动
        // Position = Position.Lerp(Target.Position, FollowSpeed * (float)delta);
    }
}