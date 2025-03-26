namespace Game.Script.Interface;

public interface IPlayerHurtListener
{
    // 参数：谁攻击了玩家,伤害多少
    void OnPlayerHurt(Enemy enemy, int enemyAck);
}