using UnityEngine;

/// <summary>
/// Class for controlling the behaviour of munitions
/// </summary>
public class FireAmmo : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    private float bulletRange = 300f;
    private Vector3 init;
    [SerializeField] private int damage;
    [SerializeField] private bool explodes;
    /// <summary>
    /// Sets initial position of bullet for calculating destruction
    /// </summary>
    private void Start(){
        init = transform.position;
    }
    /// <summary>
    /// Checks position of bullet so it can self destroy when out of range
    /// </summary>
    void Update(){
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, init) > bulletRange){ Destroy(this.gameObject); }
    }

    /// <summary>
    /// If bullet hits something, we damage it. If I expanded upon this game it would probably be wiser to create a "DamageableOpponent" Class
    /// </summary>
    /// <param name="other"></param>
    private void BulletHit(Collider other){
        Messenger.Broadcast(GameEvent.WEAPON_FIRED);
        if (other.CompareTag("Enemy") || other.CompareTag("Player")){
            ZombieAI enemy = other.gameObject.GetComponent<ZombieAI>();
            BossController boss = other.gameObject.GetComponent<BossController>();
            PlayerCharacter player = other.gameObject.GetComponent<PlayerCharacter>();
            if (enemy) { enemy.TakeDamage(damage); }
            else if (boss) { boss.TakeDamage(damage); }
            else if (player) { player.OnPlayerHit(damage); }
        }
        if (explodes) { Messenger.Broadcast(GameEvent.EXPLOSION); }
        Destroy(this.gameObject);
    }
    private void OnTriggerStay(Collider other) => BulletHit(other);
    private void OnTriggerExit(Collider other) => BulletHit(other);
    private void OnTriggerEnter(Collider other) => BulletHit(other);
}
