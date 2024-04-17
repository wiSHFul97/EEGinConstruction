using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class pickupable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

	private void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "Player" && other.transform.childCount == 2) {
			GameObject parentObject = this.gameObject.transform.parent.gameObject;
			parentObject.transform.SetParent(other.transform);
			parentObject.transform.localPosition = new Vector3(0, .5f, 2);
		}
		// else if (other.gameObject.tag == "Crane") {
		// 	GameObject parentObject = this.gameObject.transform.parent.gameObject;
		// 	parentObject.transform.SetParent(other.transform);
		// 	parentObject.transform.localPosition = new Vector3(0, -1, 0);
		// 	shouldDoCraneMovement = 1;
		// }
	}
}
