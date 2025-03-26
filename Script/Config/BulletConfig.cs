using Godot;

[GlobalClass]
public partial class BulletConfig : Resource
{
    [ExportCategory("远程伤害")] [Export] public float BaseDamage = 5f;
    [ExportCategory("子弹范围")] [Export] public float BulletRange = 1f;
    [ExportCategory("子弹大小")] [Export] public float BulletSize = 1f;
    [ExportCategory("子弹弹速")] [Export] public float BulletSpeed = 200f;
}