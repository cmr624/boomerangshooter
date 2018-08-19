using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour {

    //This is the singleton
    public static ShootManager Instance { get; private set; }

    [HideInInspector] //this hides public variables from the inspector
    public List<GameObject> allShots;

    /*
    [SerializeField]
    GameObject iAmPrivate; i will appear in the inspector but im public
    */

    private void Start()
    {
        Instance = this;

        allShots = new List<GameObject>();
    }

    private void Update()
    {
        // Debug.Log("Number of shots in existence: " + allShots.Count);
    }

    public void Recall()
    {

        foreach(GameObject bullet in allShots)
        {
            StartCoroutine(RecallBullet(bullet));

            //This all recalls one bullet
            //bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            bullet.GetComponent<State>().state = true;
            bullet.GetComponent<SpriteRenderer>().color = new Color(255f, 0f, 0f);
        }
    }

    public IEnumerator RecallBullet(GameObject bullet)
    {
        while (bullet != null && Vector2.Distance(bullet.transform.position, PlayerManager.Instance.player.transform.position) > Mathf.Epsilon)
        {

            if(bullet == null)
            {
                yield break;
            }

            bullet.transform.position = Vector2.MoveTowards(bullet.transform.position, PlayerManager.Instance.player.transform.position, .9f);

            //this line tells the coroutine to wait for one frame

            yield return null;

            //yield return new WaitForSeconds(1f);
        }
    }

}
