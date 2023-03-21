using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public RotationAxes axes = RotationAxes.MouseXAndY;
    private float horizSenseMax = 10.0f;
    private float vertSenseMax = 10.0f;
    private float sensitivityHoriz = 10.0f;
    private float sensitivityVert = 10.0f;

    private float minVert = -90.0f;
    private float maxVert = 90.0f;

    private float rotationX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        sensitivityHoriz = (PlayerPrefs.GetInt("sensitivity", 1) / 100.0f) * horizSenseMax;
        sensitivityVert = (PlayerPrefs.GetInt("sensitivity", 1) / 100.0f) * vertSenseMax;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public enum RotationAxes { 
        MouseXAndY,
        MouseX,
        MouseY,
    }
    private void Awake()
    {
        Messenger.AddListener(GameEvent.GAME_INACTIVE, OnGameInactive);
        Messenger.AddListener(GameEvent.GAME_ACTIVE, OnGameActive);
        Messenger<int>.AddListener(GameEvent.SENSITIVITY_CHANGED, OnSensitivityChange);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.GAME_INACTIVE, OnGameInactive);
        Messenger.RemoveListener(GameEvent.GAME_ACTIVE, OnGameActive);
        Messenger<int>.RemoveListener(GameEvent.SENSITIVITY_CHANGED, OnSensitivityChange);
    }
    private void OnSensitivityChange(int sensitivity){
        sensitivityHoriz = (sensitivity / 100.0f) * horizSenseMax;
        sensitivityVert = (sensitivity / 100.0f) * vertSenseMax;
    }
    private void OnGameActive()
    {
        this.enabled = true;
    }
    private void OnGameInactive()
    {
        this.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX){
            float deltaHoriz = Input.GetAxis("Mouse X") * sensitivityHoriz;
            transform.Rotate(Vector3.up * deltaHoriz);

        } else if (axes == RotationAxes.MouseY){
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

            transform.localEulerAngles = new Vector3(rotationX, 0, 0);

        } else {
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);

            float deltaHoriz = Input.GetAxis("Mouse X") * sensitivityHoriz;
            float rotationY = transform.localEulerAngles.y + deltaHoriz;

            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }
}
