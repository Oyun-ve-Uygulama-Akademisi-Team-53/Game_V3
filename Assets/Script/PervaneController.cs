using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PervaneController : MonoBehaviour
{
    public float pervanehizix;
    public float pervanehiziy;
    public float pervanehiziz;
    public PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pc.zamanDurduMu)
        {
            transform.Rotate(1 * Time.deltaTime * pervanehizix, 1 * Time.deltaTime * pervanehiziy, 1 * Time.deltaTime * pervanehiziz);

        }
        
    }
}
