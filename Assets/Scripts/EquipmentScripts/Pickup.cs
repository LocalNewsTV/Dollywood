using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] PickUpOptions itemType;
    private float x;
    private float z;
    private float y;
    public float rotationSpeed = 45f;
    
    // Start is called before the first frame update
    public enum PickUpOptions
    {
        Dagger,
        Pistol,
        Pistol_Ammo,
        Sword,
        RPG,
        RPG_Ammo,
        MedKit,
    }
    void Start()
    {
        x = transform.rotation.x;
        z = transform.rotation.z;
        y = transform.rotation.y;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemType == PickUpOptions.Dagger) {
                Messenger.Broadcast(GameEvent.DAGGER_UNLOCK);
            }
            else if (itemType == PickUpOptions.Pistol){
                Messenger.Broadcast(GameEvent.PISTOL_UNLOCK);
            }
            else if (itemType == PickUpOptions.Pistol_Ammo){
                Messenger.Broadcast(GameEvent.PISTOL_AMMO_PICKUP);
            }
            else if (itemType == PickUpOptions.Sword){
                Messenger.Broadcast(GameEvent.SWORD_UNLOCK);
            }
            else if (itemType == PickUpOptions.RPG){
                Messenger.Broadcast(GameEvent.RPG_UNLOCK);
            }
            else if (itemType == PickUpOptions.RPG_Ammo){
                Messenger.Broadcast(GameEvent.RPG_AMMO_PICKUP);
            }
            else if (itemType == PickUpOptions.MedKit){
                Messenger.Broadcast(GameEvent.HEALTH_KIT_PICKUP);
            }
                Destroy(this.gameObject);
        }

    }
    void Update()
    {
        float rotateY = rotationSpeed * Time.deltaTime % 360;
        transform.Rotate(new Vector3(0, rotateY, 0 ), Space.World);
    }
}
