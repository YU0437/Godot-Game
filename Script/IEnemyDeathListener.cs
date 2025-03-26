using Godot;

public interface IEnemyDeathListener
{
    // 参数：哪个敌人死了，给多少分，死在哪
    void OnEnemyDeath(Enemy enemy, int gold, Vector2 deathPos);
}