using UnityEngine;
using Vuforia;

public class UDTTrackableEventHandler : MonoBehaviour, ITrackableEventHandler {

	protected TrackableBehaviour mTrackableBehaviour;
	public Canvas canvas;


	protected virtual void Start () {
		mTrackableBehaviour = GetComponent<TrackableBehaviour> ();
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler (this);
	}

	protected virtual void OnDestroy () {
		if (mTrackableBehaviour)
			mTrackableBehaviour.UnregisterTrackableEventHandler (this);
	}

	public void OnTrackableStateChanged (
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus) {
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			//Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " found");
			OnTrackingFound ();
			canvas.enabled = false;
		} else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
				   newStatus == TrackableBehaviour.Status.NO_POSE) {
			//Debug.Log ("Trackable " + mTrackableBehaviour.TrackableName + " lost");
			OnTrackingLost ();
			canvas.enabled = true;
		} else {
			OnTrackingLost ();
			canvas.enabled = true;
		}
	}

	protected virtual void OnTrackingFound () {
		var rendererComponents = GetComponentsInChildren<Renderer> (true);
		var colliderComponents = GetComponentsInChildren<Collider> (true);
		var canvasComponents = GetComponentsInChildren<Canvas> (true);

		// Enable rendering:
		foreach (var component in rendererComponents)
			component.enabled = true;

		// Enable colliders:
		foreach (var component in colliderComponents)
			component.enabled = true;

		// Enable canvas':
		foreach (var component in canvasComponents)
			component.enabled = true;
	}


	protected virtual void OnTrackingLost () {
		var rendererComponents = GetComponentsInChildren<Renderer> (true);
		var colliderComponents = GetComponentsInChildren<Collider> (true);
		var canvasComponents = GetComponentsInChildren<Canvas> (true);

		// Disable rendering:
		foreach (var component in rendererComponents)
			component.enabled = false;

		// Disable colliders:
		foreach (var component in colliderComponents)
			component.enabled = false;

		// Disable canvas':
		foreach (var component in canvasComponents)
			component.enabled = false;
	}

}
