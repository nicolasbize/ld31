using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float walkSpeed = 5f;
	public float runSpeed = 10f;
	public float jumpForce = 10f;
	public float gravity = 60f;
	public LayerMask colliderMask;
	
	private bool isRunning = false;
	private bool isJumping = false;
	private bool onGround = false;

	private float mx = 0f;
	private float my = 0f;
		
	private BoxCollider collider;
	private Ray[] rays = new Ray[3];
	
	
	void Start() {
		collider = GetComponent<BoxCollider>();
	}
	
	void Update () {
		HandleInput();
		Move(new Vector3(mx, my, 0) * Time.deltaTime);
	}
	
	private void HandleInput() {
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) {
			isRunning = true;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) {
			isRunning = false;
		}
	
		mx = Input.GetAxisRaw("Horizontal") * (isRunning ? runSpeed : walkSpeed);
		
		if (onGround) {
			my = 0f;
			
			if(Input.GetButtonDown("Jump")) {
				my = jumpForce;
				onGround = false;
			}
        }
        
		// apply gravity
		my -= gravity * Time.deltaTime;	
    }
	
	private void Move(Vector3 movement) {
		Vector3 p = transform.position;
		RaycastHit hitInfo = new RaycastHit();
		Vector3 rayDir = Vector3.zero;
		
		if (movement.y != 0) {
			// check for vertical collisions
	        float vDir = Mathf.Sign(movement.y);
	        // 3 rays: left, middle, right
	        float ySide = p.y + vDir * collider.size.y;
	        rayDir = new Vector3(0, vDir, 0);
			rays[0] = new Ray(new Vector3(p.x - collider.size.x / 2, ySide, 0), rayDir);
			rays[1] = new Ray(new Vector3(p.x, ySide, 0), rayDir);
			rays[2] = new Ray(new Vector3(p.x + collider.size.x / 2, ySide, 0), rayDir);
			
			foreach(Ray r in rays){
				Debug.DrawRay(r.origin, r.direction);
				if (Physics.Raycast(r, out hitInfo, Mathf.Abs(movement.y), colliderMask)) { // see if we hit something
					onGround = vDir < 0;
					my = 0f;
					movement = new Vector3(movement.x, 0f, 0f);
					break;
				} else {
					onGround = false;
				}
			}
		}
		
		if (movement.x != 0) {
			// check for horizontal collisions
			float hDir = Mathf.Sign(movement.x);
			// 3 rays: top, middle, bottom
			float xSide = p.x + hDir * collider.size.x / 2;
			rayDir = new Vector3(hDir, 0, 0);
	
			rays[0] = new Ray(new Vector3(xSide, p.y - collider.size.y, 0), rayDir);
			rays[1] = new Ray(new Vector3(xSide, p.y, 0), rayDir);
			rays[2] = new Ray(new Vector3(xSide, p.y + collider.size.y, 0), rayDir);
			
			foreach(Ray r in rays){
				Debug.DrawRay(r.origin, r.direction);
				if (Physics.Raycast(r, out hitInfo, Mathf.Abs(movement.x), colliderMask)) { // see if we hit something
					mx = 0f;
	                movement = new Vector3(0f, movement.y, 0f);
	                break;
	            }
	        }
	    }
        
        
		transform.Translate (movement);
	}
	
	
}
