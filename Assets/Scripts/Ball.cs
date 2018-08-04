using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public static int i = 0;
    public int f;
    void Start () {
        List<float> genes = Main.instance.oldSamples[i];
        // Genes[0] is an angle
        float v = 10;
        GetComponent<Rigidbody>().velocity = new Vector2(v * Mathf.Cos(genes[0]) , v * Mathf.Sin(genes[0]));
        f = i;
        i += 1;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Main.instance.results.Add(metric());
        Main.instance.newSamples.Add(Main.instance.oldSamples[f]);
        Destroy(gameObject);
    }
    private float metric()
    {
        return (transform.position - new Vector3(Main.instance.startPos.x, Main.instance.startPos.y)).magnitude;
    }

}
