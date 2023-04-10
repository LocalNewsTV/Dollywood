using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFurniture : MonoBehaviour
{
    [SerializeField] public const float speed = 50f;
    private const float bulletRange = 250f;
    private Vector3 init;

    /// <summary>
    /// Sets initial transform position so it can calculate how far to go before self destructing
    /// </summary>
    private void Start(){ init = transform.position;}
    /// <summary>
    /// Measures space travelled to determine when to self destruct
    /// </summary>
    void Update(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        transform.Rotate(new Vector3(0,0, speed * Time.deltaTime));
        if (Vector3.Distance(transform.position, init) > bulletRange){
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// If Player is hit by furniture, damage the player
    /// </summary>
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            PlayerCharacter pc = other.GetComponent<PlayerCharacter>();
            if (pc){ pc.OnPlayerHit(15);}
        }
    }
}
