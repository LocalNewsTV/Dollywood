using UnityEngine;
using System.Collections;

public class scr_camera : MonoBehaviour {

    private float rotate_amount;
    

	// Use this for initialization
	void Start () {
		rotate_amount = Random.Range(0.0001f, 0.20f);
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, (Mathf.Sin(Time.realtimeSinceStartup) * rotate_amount) + transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
