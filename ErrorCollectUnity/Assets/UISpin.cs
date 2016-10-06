using UnityEngine;
using System.Collections;

public class UISpin : MonoBehaviour {
    
	void Update () {
        transform.Rotate(Vector3.forward, 1f);
        var size = Mathf.PingPong(Time.time, 1f);
        transform.localScale = new Vector2(size, size);
	}
}
