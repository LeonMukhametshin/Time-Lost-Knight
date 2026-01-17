using System;
using UnityEngine;

public static class EnemyFactory
{
    public static object Create(
        EnemyData enemyData, 
        Transform enemyTransform, 
        Transform contactChecker = null,
        Transform[] waypoints = null)
    {
        switch(enemyData.enemyType)
        {
            case EnemyType.Damageable: 
                return CreateDamageableType(enemyData, enemyTransform, contactChecker, waypoints);
            case EnemyType.Attack: 
                return CreateAttackType(enemyData, enemyTransform, contactChecker, waypoints);
            default: throw new ArgumentOutOfRangeException();
        }
    }

    private static EnemyAttack CreateAttackType(
        EnemyData enemyData, 
        Transform enemyTransform, 
        Transform contactChecker, 
        Transform[] waypoints)
    {
        var movement = CreateEnemyBehaviuor(enemyData, enemyTransform, contactChecker, waypoints);
        var attack = CreateAttack();

        return new EnemyAttack(movement, attack);
    }

    private static EnemyDamageable CreateDamageableType(
        EnemyData enemyData,
        Transform enemyTransform,
        Transform contactChecker,
        Transform[] waypoints)
    { 
        var movement = CreateEnemyBehaviuor(enemyData, enemyTransform, contactChecker, waypoints);

        return new EnemyDamageable(movement);
    }

    private static IEnemyBehaviuor CreateEnemyBehaviuor(
        EnemyData data,
        Transform enemyTransform,
        Transform contactChecker = null,
        Transform[] m_waypoints = null)
    {
        switch (data.movementType)
        {
            case EnemyMovementType.Patrol: 
                return new EnemyPatrolBehaviour(
                        enemyTransform,
                        contactChecker,
                        data.moveSpeed,
                        data.rayLenght);

            case EnemyMovementType.Waypoints: 
                return new EnemyWaypointBehaviour(
                        enemyTransform,
                        m_waypoints,
                        data.moveSpeed);

            default: return null;
        };
    }

    private static IAttack CreateAttack() =>
        new MeleeHitbox();
}
