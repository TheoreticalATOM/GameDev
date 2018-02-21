using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {
    public float viewRadius;
    [Range (0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
        if (visibleTargets.Count != 0)
        {
            print("Player in sight!");
            //affect player's suspicion meter
        }
        //Check if hit by throwable object
        //if so play die animation
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        //Make sure the player is of layer targetMask
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position,viewRadius,targetMask);

        for (int i = 0; i<targetsInViewRadius.Length;i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward,dirToTarget)<(viewAngle)/2)
            {
                float distToTarget = Vector3.Distance(transform.position,target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGloabal)
    {
        if (!angleIsGloabal) {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
