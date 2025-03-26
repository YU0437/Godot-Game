using Godot;

public partial class playerMove : CharacterBody2D
{
    [ExportCategory("移动设置")] [Export] public float MoveSpeed;

    public override void _Ready()
    {
    }

    // 每帧调用的函数（帧率依赖）
    public override void _Process(double delta)
    {
        var userInput = Input.GetVector("left", "right", "up", "down");
        Velocity = userInput * MoveSpeed;
        MoveAndSlide();
    }
}