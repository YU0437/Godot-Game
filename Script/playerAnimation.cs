using Godot;

public partial class playerAnimation : Node
{
    [ExportCategory("动画选择")] [Export] public AnimatedSprite2D CharacterSprite;
    private bool isHurt;

    public override void _Ready()
    {
        GlobalSignals.PlayerHit += OnPlayerHit; //订阅信号,触发信号时执行OnPlayerHit方法
    }

    public override void _ExitTree()
    {
        // 取消订阅，防止内存泄漏
        GlobalSignals.PlayerHit -= OnPlayerHit;
    }

    public void UpdateAnimationMove(Vector2 velocity)
    {
        if (isHurt) return;
        CharacterSprite.Play(velocity == Vector2.Zero ? "idle" : "run");
    }

    private void OnPlayerHit()
    {
        isHurt = true;
        CharacterSprite.Play("hurt");
        // 0.5秒后解除受击状态
        GetTree().CreateTimer(1f).Timeout += () => isHurt = false;
    }
}