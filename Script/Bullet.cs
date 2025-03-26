using Godot;

public partial class Bullet : Area2D
{
    // 使用预定义的向量常量
    private static readonly Vector2 MoveDirection = Vector2.Right;
    [ExportCategory("移动速度")] [Export] public float EnemySpeed = 200f;

    public override void _Ready()
    {
        GlobalSignals.EnemyHit += OnEnemyHit; //订阅信号,触发信号时执行OnEnemyHit方法
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += MoveDirection * EnemySpeed * (float)delta;
    }

    public override void _ExitTree()
    {
        GlobalSignals.EnemyHit -= OnEnemyHit; // 取消订阅，防止内存泄漏
    }

    private void OnTimerTimeout()
    {
        QueueFree(); // 销毁子弹
    }

    private void OnEnemyHit()
    {
        QueueFree(); // 销毁子弹
    }
}