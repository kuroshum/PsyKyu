using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMeshController : MonoBehaviour{
public GameObject TargetObject;NavMeshAgent m_navMeshAgent;Rigidbody rb;
    void Start()
    {
        // NavMeshAgent�R���|�[�l���g���擾
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        // OffMeshLink�ɏ�����ۂ̃A�N�V����
        StartCoroutine(MoveNormalSpeed(m_navMeshAgent));
    }
void Update(){
        // NavMesh�������ł��Ă���Ȃ�
       if (m_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid){
            // NavMeshAgent�ɖړI�n���Z�b�g
            m_navMeshAgent.SetDestination(TargetObject.transform.position);
        }
    }

    IEnumerator MoveNormalSpeed(NavMeshAgent agent) {

        agent.autoTraverseOffMeshLink = false; // OffMeshLink�ɂ��ړ����֎~

        while (true)
        {
            // OffmeshLink�ɏ��܂ŕ��ʂɈړ�
            yield return new WaitWhile(() => agent.isOnOffMeshLink == false);

            agent.isStopped = true;
            rb.isKinematic = false;
            Debug.Log(CalculateVelocity(transform.localPosition, agent.currentOffMeshLinkData.endPos,60));
            rb.AddForce(CalculateVelocity(transform.localPosition, agent.currentOffMeshLinkData.endPos, 60) * rb.mass, ForceMode.Impulse);
            yield return new WaitWhile(() =>
            {
                return Vector3.Distance(transform.localPosition, agent.currentOffMeshLinkData.endPos) > 1.0f;
            });
            agent.updatePosition = true;
            rb.isKinematic = true;
            // NavmeshAgent�𓞒B�������ɂ��āANavmesh���ĊJ
            agent.CompleteOffMeshLink();
            agent.isStopped = false;
        }
    }

    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB,float angle)
    {

        // �ˏo�p�����W�A���ɕϊ�
        float rad = angle * Mathf.PI / 180;

        // ���������̋���x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z))+2.0f;

        // ���������̋���y
        float y = pointA.y - (pointB.y+0.5f);

        // �Ε����˂̌����������x�ɂ��ĉ���
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // �����𖞂����������Z�o�ł��Ȃ����Vector3.zero��Ԃ�
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
        }
        
    }
}