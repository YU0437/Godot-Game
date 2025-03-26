using Game.Script.Interface;
using Godot;

public partial class Enemy : Area2D
{
    [ExportCategory("敌人属性")] [Export] public int Ack; // 这个敌人攻击
    [ExportCategory("当前生命")] [Export] public float CurrentHp;
    [ExportCategory("移动速度")] [Export] public float EnemySpeed = 30f;
    [ExportCategory("动画选择")] [Export] public AnimatedSprite2D EnemySprite;
    [Export] public int Gold; // 这个敌人值多少金币
    [ExportCategory("生命")] [Export] public int Hp = 10;
    private bool isDead; //敌人死亡判定
    private IEnemyDeathListener listenerDeath; // 接收敌人的死亡通知
    private IPlayerHurtListener listenerPlayerHurt; // 接收玩家的受伤通知

    // 使用预定义的向量常量
    private Vector2 MoveDirection = Vector2.Left;
    private playerMove player;
    [Export] public PackedScene Text; //伤害文本


    // 初始化时绑定回调
    public void Init(IEnemyDeathListener listener)
    {
        listenerDeath = listener;
    }

    //初始化绑定玩家受伤回调
    public void Init(IPlayerHurtListener listener)
    {
        listenerPlayerHurt = listener;
    }

    public override void _Ready()
    {
        player = GetParent().GetNode<playerMove>("Player");
        EnemySprite.AnimationFinished += OnAnimationFinished; //敌人死亡信号绑定
        CurrentHp = Hp; //重置生命
    }

    private void CalcDirection() //Todo:缓存玩家引用,降低计算频率
    {
        MoveDirection = (player.Position - Position).Normalized();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (isDead) return;
        Position += MoveDirection * EnemySpeed * (float)delta;

        // 简单随机扰动
        if (GD.Randf() < 0.02f) Position += new Vector2(GD.RandRange(-2, 2), GD.RandRange(-2, 2));
        CalcDirection();
    }

    private void OnBodyEntered(Node2D body) //敌人触碰到玩家
    {
        if (isDead && body is not CharacterBody2D) return;
        listenerPlayerHurt?.OnPlayerHurt(this, Ack);
        GlobalSignals.EmitPlayerHit(); // 玩家受击的信号
    }

    private void ShowDamageText(float bulletDamage)
    {
        var damageText = Text.Instantiate<DamageText>(); //生成一个伤害数字的场景,由DamageText脚本提供其属性
        damageText.ShowDamageText(bulletDamage, Position);
        GetTree().CurrentScene.AddChild(damageText);
    }

    public void TakeDamage(float bulletDamage)
    {
        if (isDead == false) ShowDamageText(bulletDamage);
        if (CurrentHp <= 0 || CurrentHp - bulletDamage <= 0)
        {
            Death();
            return;
        }

        CurrentHp -= bulletDamage;
    }

    private void Death()
    {
        listenerDeath?.OnEnemyDeath(this, Gold, GlobalPosition); // 触发回调：告知监听者进行后续处理
        EnemySprite.Play("death");
        isDead = true;
    }

    private void OnAnimationFinished()
    {
        QueueFree();
    }
}