using UnityEngine;

public class FireSwarm : MonoBehaviour
{
    [SerializeField] private float speed = 60f;
    private float bulletRange = 125f;
    private Vector3 init;
    [SerializeField] private int damage;
    private void Start()
    {
        init = transform.parent.position;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, init) > bulletRange) { this.gameObject.SetActive(false); }
    }

    private void BulletHit(Collider other)
    {
        Messenger.Broadcast(GameEvent.WEAPON_FIRED);
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            ZombieAI zombie = other.gameObject.GetComponent<ZombieAI>();
            PlayerCharacter player = other.gameObject.GetComponent<PlayerCharacter>();
            if (player) { player.OnPlayerHit(damage); }
            else if (zombie) { zombie.TakeDamage(damage); }
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other) => BulletHit(other);
    private void OnTriggerExit(Collider other) => BulletHit(other);
    private void OnTriggerEnter(Collider other) => BulletHit(other);
}
