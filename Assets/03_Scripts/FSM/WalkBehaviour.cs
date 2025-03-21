using DataDeclaration;
using UnityEngine;
using UnityEngine.AI;

public class WalkBehaviour : StateMachineBehaviour
{
    private NavMeshAgent agent;

    private Vector3[] path; // AI 경로
    private int curPathIndex; // 현재 경로 인덱스
    
    private bool isTrigger;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.TryGetComponent<NavMeshAgent>(out agent);
        if (agent == null)
        {
            Debug.LogError("agent is null");
        }

        path = new[]
        {
            new Vector3(-8.86f, 0f, -16.54f), new Vector3(2.84f, 0f, -9.74f), new Vector3(4.71f, 0f, 0f),
            new Vector3(4f, 0f, 30f)
        };

        curPathIndex = 0;
        isTrigger = false;

        Move();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            Move();
        }

        if (curPathIndex == 3 && !isTrigger)
        {
            isTrigger = true;
            UIManager.Instance.Fade(0, 1, 3, () => GameManager.ChangeScene(SceneType.Main));
        }
    }

    /// <summary>
    /// 지정된 경로 탐색 기능
    /// </summary>
    private void Move()
    {
        if (path.Length == 0) return;

        agent.SetDestination(path[curPathIndex]);
        curPathIndex = (curPathIndex + 1) % path.Length;
    }
}