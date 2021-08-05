using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyMeshController : MonoBehaviour
{
    private bool Isground = false;
    public GameObject TargetObject; NavMeshAgent m_navMeshAgent; Rigidbody rb;
    void Start()
    {
        // NavMeshAgentコンポーネントを取得
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        // OffMeshLinkに乗った際のアクション
        StartCoroutine(MoveNormalSpeed(m_navMeshAgent));
    }
    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.0f))
        {
            Isground = true;
        }
        else
        {
            Isground = false;
        }

        // NavMeshが準備できているなら
        if (m_navMeshAgent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            // NavMeshAgentに目的地をセット
            m_navMeshAgent.SetDestination(TargetObject.transform.position);
        }
    }

    IEnumerator MoveNormalSpeed(NavMeshAgent agent)
    {

        agent.autoTraverseOffMeshLink = false; // OffMeshLinkによる移動を禁止

        while (true)
        {
            // OffmeshLinkに乗るまで普通に移動
            yield return new WaitWhile(() => agent.isOnOffMeshLink == false);

            agent.isStopped = true;
            agent.updatePosition = false;
            agent.updateRotation = false;


            rb.isKinematic = false;
            rb.AddForce(CalculateVelocity(transform.localPosition, agent.currentOffMeshLinkData.endPos, 60) * rb.mass, ForceMode.Impulse);
            yield return new WaitWhile(() =>
            {
                return Isground;
            });
            yield return new WaitWhile(() =>
            {
                return !Isground;
            });
            agent.Warp(transform.localPosition);
            // NavmeshAgentを到達した事にして、Navmeshを再開
            agent.CompleteOffMeshLink();
            agent.isStopped = false;
            agent.updatePosition = true;
            agent.updateRotation = true;
            rb.isKinematic = true;
        }
    }

    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {

        // 射出角をラジアンに変換
        float rad = angle * Mathf.PI / 180;

        // 水平方向の距離x
        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z)) + 2.0f;

        // 垂直方向の距離y
        float y = pointA.y - (pointB.y + 0.5f);

        // 斜方投射の公式を初速度について解く
        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if (float.IsNaN(speed))
        {
            // 条件を満たす初速を算出できなければVector3.zeroを返す
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
        }

    }
}