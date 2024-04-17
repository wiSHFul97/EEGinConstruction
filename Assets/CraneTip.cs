using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraneTip : MonoBehaviour
{
	// [SerializeField] private Image alert;
	public GameObject craneTarget;
	private int shouldDoCraneMovement = 0;
	private GameObject attachedObject;

    // Start is called before the first frame update
    void Start()
    {
        // turnOffTheAlarm();
    }

    // Update is called once per frame
    void Update()
    {
        craneTarget.transform.position = new Vector3(craneTarget.transform.position.x+shouldDoCraneMovement*.01f, craneTarget.transform.position.y + shouldDoCraneMovement*0.05f, craneTarget.transform.position.z+shouldDoCraneMovement*.03f);
		if (shouldDoCraneMovement == 1 && craneTarget.transform.position.y > 20) {
			shouldDoCraneMovement = -1;
			Destroy(attachedObject.gameObject);
			attachedObject = null;
		}
		else if (shouldDoCraneMovement == -1 && craneTarget.transform.position.y < -11.5f)
			shouldDoCraneMovement = 0;
    }

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Pickupable") {
			other.transform.SetParent(this.transform);
			other.transform.localPosition = new Vector3(0, -.2f, 0);
			shouldDoCraneMovement = 1;
			attachedObject = other.gameObject;
		}
		// if (other.gameObject.tag == "Player") {
		// 	turnOnTheAlarm();
		// }
	}

	// private void OnTriggerExit(Collider other) {
	// 	if (other.gameObject.tag == "Player") {
	// 		turnOffTheAlarm();
	// 	}
	// }

	// private void turnOnTheAlarm()
    // {
    //     alert.color = Color.red;
    // }

    // private void turnOffTheAlarm()
    // {
    //     alert.color = Color.black;
    // }
}
