using TMPro;
using UnityEngine;

public class EnemyThree : Entity
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [SerializeField] private IdleStateData m_idleStateData;
    [SerializeField] private LookForPlayerStateData m_lookForPlayerStateData;
    [SerializeField] private PlayerDetectedData m_playerDetectedData;
    [SerializeField] private RangeAttackData m_rangeAttackData;

    [SerializeField] private Transform m_rangeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        fsm.Initialize(
            new EnemyThreeIdleState(fsm, core, EnemyAnimationConst.IDLE, this, m_idleStateData),
            new EnemyThreeLookForPlayerState(fsm, core, EnemyAnimationConst.LOOK_FOR_PLAYER, this, m_lookForPlayerStateData),
            new EnemyThreePlayerDetectedState(fsm, core, EnemyAnimationConst.PLAYER_DETECTED, this, m_playerDetectedData),
            new EnemyThreeRangeAttackState(fsm, core, EnemyAnimationConst.RANGED_ATTACK, this, m_rangeAttackPosition, m_rangeAttackData)
            );

        animationToFSM.Initialize(fsm);

        fsm.ChangeState<EnemyThreeIdleState>();
    }

    public override void Update()
    {
        base.Update();

        textMeshProUGUI.text = fsm.currentState.ToString();
    }
}