using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGLauncher : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.3f;
    private FireWeapon fw;
    private float timeSinceLastFire = 0;
    private GameObject casingref;
    // Start is called before the first frame update

    private void Start()
    {
        fw = GetComponent<FireWeapon>();
    }
    public void Fire()
    {
        fw.FireAmmo();
    }


    private void Update()
    {
        timeSinceLastFire += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && timeSinceLastFire >= fireRate)
        {
            timeSinceLastFire = 0;
            Fire();
        }
    }
}
