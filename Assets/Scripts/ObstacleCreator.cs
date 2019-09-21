using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleCreator : MonoBehaviour
{
    public GameObject fab;
    public float radius = .1f;
    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = Instantiate(fab, transform) as GameObject;
        obj.transform.SetParent(transform, false);
        obj.transform.localPosition = new Vector3(0,0,0);
   }

    // Update is called once per frame
    void Update()
    {
    }
}
