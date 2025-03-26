using Godot;

public partial class UIManager : Node
{
    [ExportCategory("金币显示")] [Export] private Label goldLabel;
    [ExportCategory("血量显示")] [Export] private TextureProgressBar HpBar;

    public override void _Ready()
    {
        GlobalSignals.GoldUiUpdate += OnGoldUiUpdate; //绑定事件总线,更新金币ui
        var hpBar = GetParent().GetNode<PlayerManager>("PlayerManager"); //接收信号,更新角色血条ui
        hpBar.HpUiUpdate += OnHpUpdate;
    }

    public override void _ExitTree()
    {
        GlobalSignals.GoldUiUpdate -= OnGoldUiUpdate; // 防内存泄漏
    }


    private void OnGoldUiUpdate(int count)
    {
        goldLabel.Text = count.ToString();
    }

    private void OnHpUpdate(int currentHp, int hp)
    {
        var percent = (float)currentHp / hp * 100;
        HpBar.Value = percent;
    }
}