using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletHell : MonoBehaviour
{
    [SerializeField] GameObject Bullets;
    [SerializeField] GameObject Player;
    [SerializeField] Material first;
    [SerializeField] Material second;

    List<GameObject> list = new();
    public int numBullets = 100;
    private const int numOfPatterns = 3;

    public int TotalNumPatterns() => numOfPatterns;
    void Start(){
        GameObject temp;
        for(int i = 0; i < numBullets; i++){
            temp = Instantiate(Bullets, this.transform);
            temp.SetActive(false);
            temp.GetComponent<Renderer>().material = i % 2 == 0 ? first : second;
            list.Add(temp);
        }
    }
    public void Fire(int spreadPattern) { 
        switch (spreadPattern){
            case 0: StartCoroutine(FireCircle()); break;
            case 1: StartCoroutine(FireSprinkler()); break;
            case 2: StartCoroutine(FireTargetting()); break;
        }
    }

    public IEnumerator FireTargetting()
    {
        for (int i = 0; i < numBullets / 4; i++){
            list[i].transform.position = this.transform.position;
            list[i].transform.LookAt(Player.transform);
            list[i].SetActive(true);
            yield return new WaitForSeconds(0.35f);
        }
    }
    public IEnumerator FireSprinkler(){
        for(int i = 0; i < numBullets; i++){
            list[i].transform.localEulerAngles = Vector3.up * ((7 * i) % 90);
            list[i].transform.position = this.transform.position;

            list[i].SetActive(true);
            yield return new WaitForSeconds(0.03f);
        }
    }
    public IEnumerator FireCircle(){
        for(int i = 0; i < numBullets; i++){
            list[i].transform.position = this.transform.position;
            list[i].transform.localEulerAngles = Vector3.up * (11 * i % 360);
            list[i].SetActive(true);
            yield return new WaitForSeconds(0.03f);
        }
    }
}
