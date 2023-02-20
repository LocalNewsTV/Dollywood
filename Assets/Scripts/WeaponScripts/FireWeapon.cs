using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] private int aimSize = 16;
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private GameObject weapon;
    private GameObject tracer;
    private Transform ammoSpawnPoint;
    // Start is called before the first frame update
    void Start(){ ammoSpawnPoint = weapon.transform.GetChild(0); }
    private void OnGUI(){
        GUIStyle style = new GUIStyle();
        style.fontSize = aimSize;

        float posX = cam.pixelWidth / 2 - aimSize / 4;
        float posY = cam.pixelHeight / 2 - aimSize / 2;

        GUI.Label(new Rect(posX, posY, aimSize, aimSize), "x", style);
    }
    // Update is called once per frame
    private IEnumerator SphereIndicator(Vector3 hitPosition)
    {

        tracer = Instantiate(ammoPrefab) as GameObject;
        tracer.transform.position = ammoSpawnPoint.TransformPoint(0, 0, 0);
        tracer.transform.rotation = transform.rotation;
        yield return new WaitForSeconds(1);
    }
    public void FireAmmo()
    {
        Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
        Ray ray = cam.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.transform.gameObject;
            StartCoroutine(SphereIndicator(hit.point));
        }
    }
}
