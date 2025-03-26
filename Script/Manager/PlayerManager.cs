using Game.Script.Interface;
using Godot;

public partial class PlayerManager : Node, IPlayerHurtListener
{
    [Signal]
    public delegate void AttackSpeedUpdatedEventHandler(float attacksPerSecond);

    [Signal]
    public delegate void HpUiUpdateEventHandler(int currentHp, int hp);

    private float CurrentAttacksPerSecond; //当前攻击次数
    private int CurrentHp; //当前生命
    [ExportCategory("角色配置")] [Export] private PlayerConfig playerConfig;

    public void OnPlayerHurt(Enemy enemy, int enemyAck) //玩家受击
    {
        CurrentHp -= enemyAck;
        EmitSignalHpUiUpdate(CurrentHp, playerConfig.Hp); //更新血量ui
    }

    public override void _Ready()
    {
        CurrentHp = playerConfig.Hp; //初始化血量
        CurrentAttacksPerSecond = playerConfig.AttacksPerSecond; //初始化攻速
        EmitSignalAttackSpeedUpdated(CurrentAttacksPerSecond); //传递信号至shot
    }
}