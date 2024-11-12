using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TreasureGenerator : MonoBehaviour
{
    public GameObject treasure;
    public GameObject[,] treasures = new GameObject[15,1];
    public float[] ys = new float[15];
    public float xz = 50;
    public float height = 30;
    private bool goodheight = false;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 15; x+=1) {
            goodheight = false;
            Vector3 spawn = new Vector3(0,0,10);
            while (goodheight == false) {
                spawn = new Vector3(UnityEngine.Random.Range(-xz, xz), UnityEngine.Random.Range(8, 20), UnityEngine.Random.Range(-xz, xz));
                goodheight = aboveground(spawn);
            }
            GameObject instance = Instantiate(treasure, spawn, Quaternion.identity);
            treasures[x,0] = instance;
            ys[x] = spawn.y;
        }
    }
    private bool aboveground(Vector3 pos) {
        RaycastHit hit;
        Ray ray = new Ray(pos, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            if (Physics.CheckSphere(pos, 1))
            {
                return false;
            }
            return true;
        }
        return false; 
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < 10; x++) {
            TimeSpan time = DateTime.Now.TimeOfDay;
            if (treasures[x,0] != null) {
                Vector3 position = treasures[x,0].transform.position;
                treasures[x,0].transform.position = new Vector3(position.x, ys[x] + 5 * Mathf.Sin(((float) time.TotalSeconds)), position.z);
            }
        }
    }
}
