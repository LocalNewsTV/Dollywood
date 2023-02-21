using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    [SerializeField] string nextScene;
    private bool goUp = true;
    private float maxY;
    private float minY;
    private float speed = 9.0f;
    Rigidbody rb;
    private bool initialContact = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        minY = rb.transform.position.y;
        maxY = minY + 15;
    }
    // Update is called once per frame
    void Update()
    {
        if (initialContact)
        {
            Vector3 movement = new Vector3(0, goUp ? .5f : -.5f, 0) * speed * Time.deltaTime;
            transform.Translate(movement);
            if (transform.position.y <= minY)
            {
                initialContact = false;
            }
            if (transform.position.y >= maxY || transform.position.y <= minY)
            {
                goUp = goUp ? false : true;
            }
            movement = transform.TransformDirection(movement);
            rb.MovePosition(movement); //= movement;  
        }
    }
    private IEnumerator next()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextScene);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            StartCoroutine(next());
            initialContact = true;
        }
    }
}