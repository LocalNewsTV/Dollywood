using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Elevator : MonoBehaviour
{
    [SerializeField] string nextScene;
    private IEnumerator next()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(nextScene);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            Messenger.Broadcast(GameEvent.NEXT_LEVEL);
            StartCoroutine(next());
        }
    }
}