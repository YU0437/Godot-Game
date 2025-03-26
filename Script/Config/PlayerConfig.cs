using Godot;

public partial class PlayerConfig : Resource
{
    [ExportCategory("攻击")] [Export] public float Ack = 1;
    [ExportCategory("每秒攻击次数")] [Export] public float AttacksPerSecond = 5;
    [ExportCategory("防御")] [Export] public int Def;
    [ExportCategory("生命")] [Export] public int Hp = 10;
}