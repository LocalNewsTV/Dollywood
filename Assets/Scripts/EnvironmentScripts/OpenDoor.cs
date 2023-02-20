using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    private float startX;
    private float endX;
    private float speed = 3f;
    private bool returnToStart = false;
    [SerializeField] private bool lockedDoor = false;
    [SerializeField] private GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        startX = door.transform.localPosition.x;
        endX = startX + 3.6f;

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !lockedDoor)
        {
            returnToStart = false;
            if (door.transform.localPosition.x < endX)
            {
                door.transform.Translate(speed * Time.deltaTime, 0f, 0f, Space.Self);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Called");
            returnToStart = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (returnToStart){
            if (door.transform.localPosition.x > startX){
                door.transform.Translate(-speed * Time.deltaTime, 0f, 0f, Space.Self);
            }
            else{
                returnToStart = false;
            }
        }
    }
}
