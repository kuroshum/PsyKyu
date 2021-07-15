using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogNPC : MonoBehaviour
{
    public GameObject TargetObject; /// �ڕW�ʒu
    private NavMeshAgent m_navMeshAgent; /// NavMeshAgent
    // Use this for initialization
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
            if((TargetObject.transform.position - transform.position).sqrMagnitude > 1)
            {
                // NavMeshAgent�ɖړI�n���Z�b�g
                m_navMeshAgent.SetDestination(TargetObject.transform.position);

            }

        }
    }
}
