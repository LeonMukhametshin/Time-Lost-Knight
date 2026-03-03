using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IEffectable
{
    [SerializeField] private GameObject m_hitParticles;
    [SerializeField] private Animator m_animator;

    public void TakeDamage(float amount)
    {
        Debug.Log(amount);

        CreateParticles();

        m_animator.SetTrigger("damage");
    }

    private void CreateParticles()
    {
        Instantiate(m_hitParticles, transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360)));
    }
}