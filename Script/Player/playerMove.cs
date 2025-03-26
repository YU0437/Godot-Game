using Godot;

public partial class playerMove : CharacterBody2D
{
    [ExportCategory("移动设置")] [Export] public float MoveSpeed;
    [ExportCategory("动画脚本")] [Export] public playerAnimation PlayerAnimation;
    [Export] private AnimatedSprite2D sprite; // 拖拽赋值角色Sprite节点


    // 每帧调用的函数（帧率依赖）
    public override void _PhysicsProcess(double delta)
    {
        var userInput = Input.GetVector("left", "right", "up", "down");
        Velocity = userInput * MoveSpeed;
        MoveAndSlide();
        PlayerAnimation.UpdateAnimationMove(Velocity);

        // 水平翻转逻辑
        if (Velocity.X != 0) sprite.Scale = new Vector2(Mathf.Sign(Velocity.X), sprite.Scale.Y);
    }
}