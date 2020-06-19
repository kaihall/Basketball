using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBall : MonoBehaviour
{
    public Vector3 minimumPower = new Vector3(0, 0, 0);
	public Vector3 maximumPower = new Vector3(0, 0, 0);
	internal Vector3 throwPower = new Vector3(0, 0, 0);
	public Vector3 deltaPower = new Vector3(1f, 1f, 1f);
	private bool mouseDownLastFrame = false;
	private bool movingLeftRight = true;
	private Vector3 resetPosition = new Vector3(0f, 0f, 0f);
	private float deltaX = -0.1f;
	
	// Start is called before the first frame update
    void Start()
    {
        resetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		// Detect mouse button input.
		// If the primary mouse button is down...
        if (Input.GetMouseButton(0)) {
			if (mouseDownLastFrame) {
				movingLeftRight = false;
			} else {
				mouseDownLastFrame = true;
			}
			UpdatePower();
		}
		// If the primary mouse button is up...
		else {
			if (mouseDownLastFrame) {
				Throw();
				StartCoroutine(Reset());
			}
			mouseDownLastFrame = false;
		}
		
		// If we haven't stopped the ball's movement, move the ball.
		if (movingLeftRight) {
			if (transform.position.x <= -3 && deltaX < 0) deltaX = 0.05f;
			else if (transform.position.x >= 3 && deltaX > 0) deltaX = -0.05f;
			
			transform.position = transform.position + deltaX*Vector3.right;
		}
    }

	private void UpdatePower()
	{
		// Update X Power
		
		// Update Y Power
		if ((throwPower.y <= minimumPower.y && deltaPower.y < 0) || 
			(throwPower.y >= maximumPower.y && deltaPower.y > 0)) {
				deltaPower.y *= -1;
		}
		
		// Update Z Power
		if ((throwPower.z <= minimumPower.z && deltaPower.z < 0) || 
			(throwPower.z >= minimumPower.z && deltaPower.z > 0)) {
				deltaPower.z *= -1;
		}
		
		throwPower += deltaPower;
	}
	
	private void Throw() 
	{
		GetComponent<Rigidbody>().isKinematic = false;
		GetComponent<Rigidbody>().AddForce(throwPower);
	}
	
	IEnumerator Reset()
	{
		yield return new WaitForSeconds(3);
		GetComponent<Rigidbody>().isKinematic = true;
		transform.position = resetPosition;
		throwPower = minimumPower;
		movingLeftRight = true;
	}
}
