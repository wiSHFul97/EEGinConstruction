using System.Collections;
using System.Collections.Generic;
using Esri.ArcGISMapsSDK.Components;
using UnityEngine;

public class SegmentState : MonoBehaviour
{

    public enum STATE { INSTALLED, UNINSTALLED, PREPARED_TO_INSTALL };
    public STATE state = STATE.INSTALLED;
    public Material installedMaterial;
    public Material prepareMaterial;
    public Material uninstalledMaterial;
    
    public GameObject target;
    public GameObject canvas;
    private GameObject canvasInstance;
	private Quaternion initial_rotation;
	private Vector3 initial_location;
	private Transform initial_parent;

	private void enableCamController(bool enabled) {
		ArcGISCameraControllerComponent camController = Camera.main.GetComponent<ArcGISCameraControllerComponent>();
		camController.enabled = enabled;
	}
    private void OnMouseDown() {
        canvasInstance = Instantiate(canvas);
        canvasInstance.SendMessage("getGameObject", gameObject);
		enableCamController(false);
		if (initial_location.Equals(Vector3.zero)) {
			Debug.Log("--------------");
			initial_rotation = gameObject.transform.rotation;
			initial_location = gameObject.transform.position;
			initial_parent = gameObject.transform.parent;
			Debug.Log(initial_parent);
		}
    }

    void changeState(STATE newState) {
        Debug.Log(newState);
        switch (newState)
        {
            case STATE.INSTALLED:
                gameObject.GetComponent<Renderer>().material = installedMaterial;
				gameObject.transform.parent = initial_parent;
				gameObject.transform.position = initial_location;
				gameObject.transform.rotation = initial_rotation;
                break;
            case STATE.UNINSTALLED:
                gameObject.GetComponent<Renderer>().material = uninstalledMaterial;
				gameObject.transform.parent = initial_parent;
				gameObject.transform.position = initial_location;
				gameObject.transform.rotation = initial_rotation;
                break;
            case STATE.PREPARED_TO_INSTALL:
                gameObject.GetComponent<Renderer>().material = prepareMaterial;
				gameObject.transform.position = target.transform.position;
				gameObject.transform.parent = target.transform;
                break;
        }
        this.state = newState;
		enableCamController(true);
        Destroy(canvasInstance);
    }
}
