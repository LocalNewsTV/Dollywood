using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Rendering;

public enum EnemyStates { alive, dead }; 
public class WanderingAI : MonoBehaviour
{
    [SerializeField] private GameObject laserbeamPrefab;
    private GameObject laserbeam;
    public float fireRate = 2.0f;
    private float nextFire = 0.0f;
    
    private float enemySpeed = 3.0f;
    private float obstacleRange = 5.0f;
    private float sphereRadius = 0.75f;
    private EnemyStates state;
    void Start()
    {
        state = EnemyStates.alive;
    }
    
    public void ChangeState(EnemyStates state)
    {
        this.state = state;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //set colour
        //Determine the range vector (Starting a enemy)
        Vector3 rangeTest = transform.position + transform.forward * obstacleRange;
        //Draw line to show the range vector
        Debug.DrawLine(transform.position, rangeTest);
        //draw a wire sphere at the point on the end of the range vector;
        Gizmos.DrawWireSphere(rangeTest, sphereRadius);
    }

    void Update()
    {
        if (state == EnemyStates.alive){
            //move enemy
            transform.Translate(Vector3.forward * enemySpeed * Time.deltaTime);
            //Generate Ray
            Ray ray = new Ray(transform.position, transform.forward);
            //Spherecast and determine if enemy needs to turn
            RaycastHit hit;
            if (Physics.SphereCast(ray, sphereRadius, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if(laserbeam == null && Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        laserbeam = Instantiate (laserbeamPrefab) as GameObject;
                        laserbeam.transform.position = transform.TransformPoint(0, 1.5f, 1.5f);
                        laserbeam.transform.rotation = transform.rotation;
                    }
                    else
                    {
                        Debug.Log("don't fire");
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    Debug.Log("avoiding barrier");
                    float turnAngle = Random.Range(-110, 110);
                    transform.Rotate(Vector3.up * turnAngle);
                }
            }
        }
    }


}
