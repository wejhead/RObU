using UnityEngine;
using System.Collections;

public class stickController : MonoBehaviour {
	

	// Update is called once per frame
	void Update () {
		float temp = Input.mousePosition.x * (10f/1024f) - 5f;
		Debug.Log (temp);
		transform.localPosition = new Vector3 (temp, transform.localPosition.y, transform.localPosition.z);
	}
}
