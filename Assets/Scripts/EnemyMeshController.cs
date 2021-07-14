using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMeshController : MonoBehaviour
{
    public GameObject TargetObject;//�ڕW�ʒu
    NavMeshAgent m_navMeshAgent; /// NavMeshAgent
    
    
    void Start()
    {
        // NavMeshAgent�R���|�[�l���g���擾
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // NavMesh�������ł��Ă���Ȃ�
        if (m_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            // NavMeshAgent�ɖړI�n���Z�b�g
            m_navMeshAgent.SetDestination(TargetObject.transform.position);
        }
    }
}
