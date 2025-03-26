using Godot;

public partial class Shot : Node2D
{
    private PlayerManager attackSpeedUpdated;
    [ExportCategory("子弹配置")] [Export] private BulletConfig BulletConfig;
    [ExportCategory("子弹类型")] [Export] public PackedScene BulletScene;
    private float currentAttackSpeed = 1f;
    [ExportCategory("枪口位置")] [Export] public Marker2D Muzzle;
    private Timer shotTimer;

    public override void _Ready()
    {
        attackSpeedUpdated = GetNode<PlayerManager>("/root/Node2D/GameManager/PlayerManager");
        shotTimer = GetNode<Timer>("ShotTimer");
        attackSpeedUpdated.AttackSpeedUpdated += OnAttackSpeedUpdated;
        shotTimer.Timeout += Shoot; //计时射击
        //Todo:后续可以和delta混合使用
        UpdateShotInterval(); //初始化计时间隔
    }

    public override void _ExitTree() //防止内存泄漏
    {
        attackSpeedUpdated.AttackSpeedUpdated -= OnAttackSpeedUpdated;
    }

    private void UpdateShotInterval() //更新攻击间隔
    {
        shotTimer.Start(); // 必须手动启动！
        shotTimer.WaitTime = 1.0f / currentAttackSpeed;
    }


    private void OnAttackSpeedUpdated(float attackSpeed) //攻速被修改时,绑定的方法
    {
        currentAttackSpeed = attackSpeed;
        UpdateShotInterval();
    }

    private void Shoot()
    {
        var bullet = BulletScene.Instantiate<Bullet>(); //生成一个子弹场景,类型为Bullet脚本
        bullet.InitBulletConfig(BulletConfig); //传出子弹配置
        bullet.Position = Muzzle.GlobalPosition;
        GetTree().CurrentScene.AddChild(bullet); //"射击"
    }
}