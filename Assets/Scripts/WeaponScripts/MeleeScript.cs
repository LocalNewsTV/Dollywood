using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Camera cam;
    [SerializeField] private float attackRange;
    [SerializeField] private int damage;
    [SerializeField] private AudioClip[] sounds;
    private AudioSource sound;
    private const int attackHit = 0;
    private const int attackSwing = 1;

    private bool soundRep = false;
    private const float minVert = -40f;
    private const float maxVert = 38f;
    private float contactPoint;
    private float rotationX = 0f;
    private bool hasHit = false;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        contactPoint = (minVert + maxVert) / 2.0f;
        AdjustVolume();

    }
    private void Awake()
    {
        Messenger.AddListener(GameEvent.SOUND_CHANGED, AdjustVolume);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.SOUND_CHANGED, AdjustVolume);
    }

    public void AdjustScale(float scale){
        attackRange *= scale;
    }
    private void AdjustVolume()
    {
        sound.volume = PlayerPrefs.GetInt("sound") / 100.0f;
    }
    //Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            if (!soundRep) {
                soundRep = true;
                sound.PlayOneShot(sounds[attackSwing]);
            }
            rotationX += speed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
            if (rotationX < contactPoint + 1f && rotationX > contactPoint - 1f && !hasHit) {
                Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2);
                Ray ray = cam.ScreenPointToRay(point);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    hasHit = true;
                    float distance = Vector3.Distance(hit.transform.position, this.transform.position);
                    if(distance <= attackRange) {
                        sound.PlayOneShot(sounds[attackHit]);
                        ZombieAI enemy = hit.transform.gameObject.GetComponent<ZombieAI>();
                        if (enemy)
                        {
                            enemy.TakeDamage(damage);
                        }
                    }
                }
            }
        } else {
            soundRep = false;
            hasHit = false;
            rotationX -= speed;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
        }
        transform.localEulerAngles = new Vector3(rotationX, 0, 0);
    }
}
