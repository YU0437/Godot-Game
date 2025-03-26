using Godot;

public partial class DamageText : Node2D
{
    [Export] private AnimationPlayer animPlayer;
    [Export] public Label Label;


    public void ShowDamageText(float damage, Vector2 position)
    {
        Label.Text = damage.ToString("0");
        // 将 Label 的全局坐标对齐到敌人位置
        Position = position - new Vector2(15, 0);

        animPlayer.Play("float_up");
    }

    private void OnAnimationPlayerFinished(string animName)
    {
        QueueFree(); // 动画结束后自动销毁
    }
}