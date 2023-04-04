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
    // Start is called before the first frame update
    void Start() => ammoSpawnPoint = weapon.transform.GetChild(0);
    private IEnumerator CreateAndAimProjectile(Vector3 hitPosition){
        tracer = Instantiate(ammoPrefab, ammoSpawnPoint.position, ammoSpawnPoint.rotation);
        tracer.transform.LookAt(hitPosition);
        yield return new WaitForSeconds(1);
        Messenger.Broadcast(GameEvent.WEAPON_FIRED);
    }
    private IEnumerator CreateProjectile(){
        tracer = Instantiate(ammoPrefab, ammoSpawnPoint.position, ammoSpawnPoint.rotation);
        yield return new WaitForSeconds(1);
        Messenger.Broadcast(GameEvent.WEAPON_FIRED);
    }
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
