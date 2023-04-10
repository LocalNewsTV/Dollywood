using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private GameObject weapon;
    private GameObject tracer;
    private Transform ammoSpawnPoint;
    
    //Set spawn point in the start method
    void Start() => ammoSpawnPoint = weapon.transform.GetChild(0);
    /// <summary>
    /// Instantiates a bullet prefab and uses the LookAt method to aim it at the destination
    /// </summary>
    /// <param name="hitPosition"> Destination in which to aim bullet</param>
    private IEnumerator CreateAndAimProjectile(Vector3 hitPosition){
        tracer = Instantiate(ammoPrefab, ammoSpawnPoint.position, ammoSpawnPoint.rotation);
        tracer.transform.LookAt(hitPosition);
        yield return new WaitForSeconds(1);
        Messenger.Broadcast(GameEvent.WEAPON_FIRED);
    }
    /// <summary>
    /// Used as backup for when the Raycast fails, aims a bullet forward, has slightly less accuracy but will only proc when the player shoots at a skybox
    /// </summary>
    private IEnumerator CreateProjectile(){
        tracer = Instantiate(ammoPrefab, ammoSpawnPoint.position, ammoSpawnPoint.rotation);
        yield return new WaitForSeconds(1);
        Messenger.Broadcast(GameEvent.WEAPON_FIRED);
    }
    /// <summary>
    /// Firing method for weapons, Creates the bullet from a prefab, and aims the bullet at the destination provided by a raycast, if raycast fails, it shoots and prays
    /// </summary>
    public void FireAmmo(){
        Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0.5f);
        Ray ray = cam.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            StartCoroutine(CreateAndAimProjectile(hit.point + (ammoSpawnPoint.transform.forward * 0.25f)));
        } else {
            Vector3 camPos = cam.transform.localPosition;
            camPos.z += cam.transform.localPosition.z;
            StartCoroutine(CreateProjectile());
        }
    }
}
