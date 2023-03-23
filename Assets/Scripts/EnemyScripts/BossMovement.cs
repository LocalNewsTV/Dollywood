using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private WaypointPath path;          // the path we are following
    [SerializeField] private GameObject player;
    private Transform sourceWP;         // the waypoint transform we are travelling from
    private Transform targetWP;         // the waypoint transform we are travelling to
    private int targetWPIndex = 0;      // the waypoint index we are travelling to

    private float totalTimeToWP;        // the total time to get from source WP to targetWP
    private float elapsedTimeToWP = 0;  // the elapsed time (sourceWP to targetWP)
    private float speed = 20.0f;         // movement speed

    private bool pauseMotion = false;
    private int timeToWait = 4;
    private bool active = false;

    public void OnStartBossFight()
    {
        active = true;
        TargetNextWaypoint();
    }
    public void ChangeSpeed(float speed)
    {
        this.speed = speed;
    }
    void FixedUpdate()
    {
        if (active) {
            MoveTowardsWaypoint();
            transform.LookAt(player.transform);
        }
    }

    void Start(){
        
    }
    private void Awake(){
        Messenger.AddListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
    }
    private void OnDestroy(){
        Messenger.RemoveListener(GameEvent.START_BOSS_FIGHT, OnStartBossFight);
    }

    // Determine what waypoint we are going to next, and set associated variables
    private void TargetNextWaypoint()
    {
        // reset the elapsed time
        elapsedTimeToWP = 0;
        // determine the source waypoint
        sourceWP = path.GetWaypoint(targetWPIndex);
        // determine the target waypoint
        targetWPIndex = Random.Range(0, path.GetWaypointCount()-1);

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
            StartCoroutine(Wait());
            TargetNextWaypoint();
        }
    }
    private IEnumerator Wait()
    {
        pauseMotion = true;
        yield return new WaitForSeconds(timeToWait);
        pauseMotion = false;
    }
}