using TMPro;
using UnityEngine;

public class EnemyFour : Entity
{
    [SerializeField] private Transform m_rangeAttackPosition;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Transform[] m_waypoints;

    public override void Awake()
    {
        base.Awake();

        var enemyFourData = data as EnemyFourData;

        fsm.Initialize(
            new EnemyFourIdleState(fsm, core, EnemyAnimationConst.IDLE, this, enemyFourData.idle),
            new EnemyFourMoveState(fsm, core, EnemyAnimationConst.MOVE, this, enemyFourData.move, m_waypoints),
            new EnemyFourLookForPlayerState(fsm, core, EnemyAnimationConst.LOOK_FOR_PLAYER, this, enemyFourData.lookForPlayer),
            new EnemyFourPlayerDetectedState(fsm, core, EnemyAnimationConst.PLAYER_DETECTED, this, enemyFourData.playerDetected),
            new EnemyFourAttackState(fsm, core, EnemyAnimationConst.RANGED_ATTACK, this, m_rangeAttackPosition, enemyFourData.rangeAttack));

        animationToFSM.Initialize(fsm);

        fsm.ChangeState<EnemyFourIdleState>();
    }

    public override void Update()
    {
        base.Update();

        text.text = fsm.currentState.ToString();
    }
}