using UnityEngine;

public class FireAmmo : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    private float bulletRange = 300f;
    private Vector3 init;
    [SerializeField] private int damage;
    [SerializeField] private bool explodes;
    private void Start(){
        init = transform.position;
    }
    void Update(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, init) > bulletRange){ Destroy(this.gameObject); }
    }

    private void OnTriggerEnter(Collider other){
        Messenger.Broadcast(GameEvent.WEAPON_FIRED);
        if (other.CompareTag("Enemy") || other.CompareTag("Player")){
            ZombieAI enemy = other.gameObject.GetComponent<ZombieAI>();
            BossController boss = other.gameObject.GetComponent<BossController>();
            PlayerCharacter player = other.gameObject.GetComponent<PlayerCharacter>();
            if (enemy) { enemy.TakeDamage(damage); }
            else if (boss) { boss.TakeDamage(damage); }
            else if (player) { player.OnPlayerHit(damage); }
        }
        if (explodes){ Messenger.Broadcast(GameEvent.EXPLOSION); }
        Destroy(this.gameObject);
}
}
