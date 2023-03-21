using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGLauncher : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.3f;
    private FireWeapon fw;
    private float timeSinceLastFire = 0;
    // Start is called before the first frame update

    private void Start(){
        fw = GetComponent<FireWeapon>();
    }
    public bool Fire(){
        if (timeSinceLastFire >= fireRate){
            timeSinceLastFire = 0;
            fw.FireAmmo();
            return true;
        }
        return false;
    }
    private void Update(){
        timeSinceLastFire += Time.deltaTime;
    }
}
