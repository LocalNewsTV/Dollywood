using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Camera cam;
    [SerializeField] private float attackRange;
    private float minVert = -40f;
    private float maxVert = 38f;
    private float contactPoint;
    private float rotationX = 0f;
    private bool hasHit = false;

    private void Start()
    {
        contactPoint = (minVert + maxVert) / 2.0f;
    }
    public void AdjustScale(float scale)
    {
        attackRange *= scale;
        
    }
    //Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            rotationX += speed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
            if (rotationX < contactPoint + 1f && rotationX > contactPoint - 1f && !hasHit) {
                Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2);
                Ray ray = cam.ScreenPointToRay(point);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    hasHit = true;
                    float distance = Vector3.Distance(hit.transform.position, this.transform.position);
                    if(distance <= attackRange) {
                        ZombieAI enemy = hit.transform.gameObject.GetComponent<ZombieAI>();
                        if (enemy)
                        {
                            enemy.TakeDamage(15);
                        }
                    }
                }
            }
        } else {
            hasHit = false;
            rotationX -= speed;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
        }
        transform.localEulerAngles = new Vector3(rotationX, 0, 0);
    }
}
