using UnityEngine;
using System.Collections;

public class SimpleObject : MonoBehaviour {
	
	public string 		name;
	public int 			type;
	public GameObject	ObjectCameraPath;
	private GameObject	myPath;
	private CameraPathBezierAnimator cameraPathBezierAnimator;
	
	// Use this for initialization
	void Start () {
		myPath = Instantiate(ObjectCameraPath, ObjectCameraPath.transform.position, ObjectCameraPath.transform.rotation) as GameObject;
		cameraPathBezierAnimator = myPath.GetComponent("CameraPathBezierAnimator") as CameraPathBezierAnimator;
		cameraPathBezierAnimator.animationTarget = this.transform;
		
		cameraPathBezierAnimator.AnimationStarted += OnAnimationStarted;
        cameraPathBezierAnimator.AnimationPaused += OnAnimationPaused;
        cameraPathBezierAnimator.AnimationStopped += OnAnimationStopped;
        cameraPathBezierAnimator.AnimationFinished += OnAnimationFinished;
        cameraPathBezierAnimator.AnimationLooped += OnAnimationLooped;
        cameraPathBezierAnimator.AnimationPingPong += OnAnimationPingPonged;

        cameraPathBezierAnimator.AnimationPointReached += OnPointReached;
        cameraPathBezierAnimator.AnimationPointReachedWithNumber += OnPointReachedByNumber;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnAnimationStarted()
    {
        Debug.Log("The animation has begun");
    }

    private void OnAnimationPaused()
    {
        Debug.Log("The animation has been paused");
    }

    private void OnAnimationStopped()
    {
        Debug.Log("The animation has been stopped");
    }

    private void OnAnimationFinished()
    {
        Debug.Log("The animation has finished");
		Destroy(myPath);
		Destroy(gameObject);
    }

    private void OnAnimationLooped()
    {
        Debug.Log("The animation has looped back to the start");
    }

    private void OnAnimationPingPonged()
    {
        Debug.Log("The animation has ping ponged into the other direction");
    }

    private void OnPointReached()
    {
        Debug.Log("A point was reached");
    }

    private void OnPointReachedByNumber(int pointNumber)
    {
        Debug.Log("The point " + pointNumber + " was reached");
    }
}
