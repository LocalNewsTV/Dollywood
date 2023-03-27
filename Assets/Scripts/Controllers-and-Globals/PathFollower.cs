using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private WaypointPath path;          // the path we are following
    private Transform sourceWP;         // the waypoint transform we are travelling from
    private Transform targetWP;         // the waypoint transform we are travelling to
    public bool linearPath = true;
    private int targetWPIndex = 0;      // the waypoint index we are travelling to

    private float totalTimeToWP;        // the total time to get from source WP to targetWP
    private float elapsedTimeToWP = 0;  // the elapsed time (sourceWP to targetWP)
    private float speed = 20.0f;         // movement speed

    private bool pauseMotion = false;
    private int timeToWait = 1;
    private bool forward = true;
    void Start()
    {
        TargetNextWaypoint();
    }

    void FixedUpdate()
    {
        MoveTowardsWaypoint();
    }


    // Determine what waypoint we are going to next, and set associated variables
    private void TargetNextWaypoint()
    {
        // reset the elapsed time
        elapsedTimeToWP = 0;
        // determine the source waypoint
        sourceWP = path.GetWaypoint(targetWPIndex);
        // determine the target waypoint
        if (linearPath)
        {
            targetWPIndex += 1;
            if (targetWPIndex >= path.GetWaypointCount())
            {
                targetWPIndex = 0;
            }
        }
        else
        {
            targetWPIndex += forward ? 1 : -1;
            if (targetWPIndex == path.GetWaypointCount() - 1 || targetWPIndex == 0)
            {
                forward = forward ? false : true;
            }
        }

        // if we exhausted our waypoints, reverse the direction

        targetWP = path.GetWaypoint(targetWPIndex);
        // calculate source to target distance
        float distancetoWP = Vector3.Distance(sourceWP.position, targetWP.position);
        // calculate time to waypoint
        totalTimeToWP = distancetoWP / speed;
    }

    // Travel towards the target waypoint (call this from FixedUpdate())
    private void MoveTowardsWaypoint()
    {
        if (pauseMotion) { return; }
        // calculate the elapsed time spent on the way to this waypoint
        elapsedTimeToWP += Time.deltaTime;

        // calculate percent complete
        float elapsedTimePercentage = elapsedTimeToWP / totalTimeToWP;

        //Make speed slower at beginning and end
        elapsedTimePercentage = Mathf.SmoothStep(0, 1, elapsedTimePercentage);

        // move
        transform.position = Vector3.Lerp(sourceWP.position, targetWP.position, elapsedTimePercentage);

        //Rotation
        transform.rotation = Quaternion.Lerp(sourceWP.rotation, targetWP.rotation, elapsedTimePercentage);
        // check if we've reached our waypoint (based on time). If so, target the next waypoint
        if (elapsedTimePercentage >= 1)
        {
            if(targetWPIndex == 0 || targetWPIndex == path.GetWaypointCount() -1)
            {
                StartCoroutine(Wait());
            }
            TargetNextWaypoint();
        }
    }
    private IEnumerator Wait()
    {
        pauseMotion = true;
        yield return new WaitForSeconds(timeToWait);
        pauseMotion = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.gameObject.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}