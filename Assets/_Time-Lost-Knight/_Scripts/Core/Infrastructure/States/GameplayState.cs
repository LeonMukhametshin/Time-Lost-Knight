using System;
using UnityEngine.SceneManagement;

public class GameplayState : IState
{
    public event Action levelLoaded;

    private StateMachine m_stateMachine;

    public GameplayState(
        StateMachine stateMachine)
    {
        m_stateMachine = stateMachine;
    }

    public void Enter()
    {
        LoadLevel();
        levelLoaded += SpawnEnemies;
    }

    public void Exit()
    {
        levelLoaded -= SpawnEnemies;
    }

    private void LoadLevel()
    {
        //TODO random level selection
        SceneManager.LoadSceneAsync(
            SceneNames.LEVEL_EXAMPLE,
            LoadSceneMode.Additive)
            .completed += _ =>
            {
                levelLoaded?.Invoke();
            };
    }

    private void SpawnEnemies()
    {
        //m_enemySpawner.Spawn();
    }
}