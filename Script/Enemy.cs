using Godot;

public partial class Enemy : Area2D
{
    // 使用预定义的向量常量
    private static readonly Vector2 MoveDirection = Vector2.Left;
    [ExportCategory("移动速度")] [Export] public float EnemySpeed = 50f;
    [ExportCategory("动画选择")] [Export] public AnimatedSprite2D EnemySprite;
    [ExportCategory("敌人属性")] [Export] private int gold; // 这个敌人值多少金币
    private bool isDead;
    private IEnemyDeathListener listenerDeath; // 谁来接收敌人的死亡通知

    // 初始化时绑定回调
    public void Init(IEnemyDeathListener listener)
    {
        listenerDeath = listener;
    }

    public override void _Ready()
    {
        EnemySprite.AnimationFinished += OnAnimationFinished;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (isDead) return;
        Position += MoveDirection * EnemySpeed * (float)delta;
    }

    private void OnBodyEntered(Node2D body) //敌人触碰到玩家
    {
        if (isDead && body is not CharacterBody2D) return;
        GlobalSignals.EmitPlayerHit(); // 玩家触碰敌人的信号
    }

    private void OnAreaEntered(Area2D body) //怪物受击
    {
        listenerDeath?.OnEnemyDeath(this, gold, GlobalPosition); // 触发回调：告知监听者进行后续处理
        // if (isDead) return;
        // GlobalSignals.EmitEnemyHit(); //怪物受到子弹攻击的信号
        EnemySprite.Play("death");
        isDead = true;
    }

    private void OnAnimationFinished()
    {
        QueueFree();
    }
}