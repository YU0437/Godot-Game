using Godot;

public partial class Bullet : Area2D
{
    private BulletConfig config;

    public void InitBulletConfig(BulletConfig bulletConfig) //初始化子弹属性
    {
        config = bulletConfig; //接收子弹参数
    }

    private void OnAreaEntered(Area2D body)
    {
        if (body is Enemy enemy) enemy.TakeDamage(config.BaseDamage); //Todo:后续改用信号,解耦
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += Vector2.Right * config.BulletSpeed * (float)delta;
    }


    private void OnTimerTimeout()
    {
        QueueFree(); // 销毁子弹
    }
}