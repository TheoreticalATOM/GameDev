using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public float SuspicionIncrease;
    public DirectorAwarenessValue SuspicionValue;

    public List<Transform> visibleTargets = new List<Transform>();

    public float RotationSpeed;
    public Vector3 TargetRotation1;

    public Light SpotLight;
    
    private bool BackToNormal = true;
    private float cpt = 0;
    private void Start()
    {
        StartCoroutine(FindTargetsWithDelay(.2f));
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(TargetRotation1 + transform.position, 0.2f);
    }

    private void Update()
    {
        if(visibleTargets.Count > 0)
            SuspicionValue.UpdateValue(SuspicionIncrease * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (visibleTargets.Count == 0)
        {
            if (SpotLight.color != Color.white)
                SpotLight.color = Color.Lerp(SpotLight.color, Color.white, Time.deltaTime * 1f);
            if (!BackToNormal)
            {
                Vector3 dir = TargetRotation1 - transform.position;
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 3f);
                if (transform.rotation == rot)
                {
                    BackToNormal = true;
                    cpt = 1f;
                }


            }
            else
            {
                transform.LookAt(TargetRotation1 * Mathf.Sin(cpt * RotationSpeed));
                cpt += 0.03f;

            }

        }
        else
        {
            BackToNormal = false;
            Vector3 dir = visibleTargets[0].transform.position - transform.position;
            Quaternion rot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2.5f);
            if (SpotLight.color != Color.red)
                SpotLight.color = Color.Lerp(SpotLight.color, Color.red, Time.deltaTime * 1f);

        }
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
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < (viewAngle) / 2)
            {
                float distToTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGloabal)
    {
        if (!angleIsGloabal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
