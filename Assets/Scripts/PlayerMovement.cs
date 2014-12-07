using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 5f;
	public float jumpForce = 10f;
	public float gravity = 60f;
	public LayerMask collisionMask;
	
	private bool onGround = false;
	private bool canJump = true;

	private float mx = 0f;
	private float my = 0f;
		
	private BoxCollider collider;
	private Vector3 s;
	private Vector3 c;
	private float errMargin = 0.01f;
	
	private Ray ray;
	private RaycastHit hit;
	
	
	void Start() {
		collider = GetComponent<BoxCollider>();
	}
	
	void Update () {
		HandleInput();
		Move(new Vector3(mx, my, 0) * Time.deltaTime);
	}
	
	private void HandleInput() {
	
		mx = Input.GetAxisRaw("Horizontal") * speed;
		
		if (onGround) {
			my = 0f;
		}
			
		if(canJump && Input.GetButtonDown("Jump")) {
			my = jumpForce;
			canJump = false;
			onGround = false;
        }
        
		// apply gravity
		if(!onGround) {
			my -= gravity * Time.deltaTime;	
		}
    }
	
	private void Move(Vector3 movement) {
		
		Vector3 p = transform.position;
		RaycastHit hitInfo = new RaycastHit();
		Vector3 rayDir = Vector3.zero;
		Ray[] rays = new Ray[3];

		// check for vertical collisions
        float vDir = Mathf.Abs(movement.y) > errMargin ? Mathf.Sign(movement.y) : -1;
        Debug.Log (movement.y);
        // 3 rays: left, middle, right
		float ySide = p.y + (movement.y > errMargin ? collider.size.y * 2 : collider.size.y / 2);
        rayDir = new Vector3(0, vDir, 0);
		rays[0] = new Ray(new Vector3(p.x - collider.size.x / 2, ySide, 0), 2 * rayDir);
		rays[1] = new Ray(new Vector3(p.x, ySide, 0), 2 * rayDir);
		rays[2] = new Ray(new Vector3(p.x + collider.size.x / 2, ySide, 0), 2 * rayDir);
		
		foreach(Ray r in rays){
			Debug.DrawRay(r.origin, r.direction);
			if (Physics.Raycast(r, out hitInfo, Mathf.Max(Mathf.Abs(movement.y), errMargin), collisionMask)) { // see if we hit something
				onGround = vDir < 0;
				my = 0f;
				movement = new Vector3(movement.x, 0f, 0f);
				canJump = true;
				break;
			} else {
				onGround = false;
			}
		}

		// check for horizontal collisions
		float hDir = Mathf.Sign(movement.x);
		// 3 rays: top, middle, bottom
		float xSide = p.x + hDir * collider.size.x / 2;
		rayDir = new Vector3(hDir, 0, 0);

		rays[0] = new Ray(new Vector3(xSide, p.y + 2 * collider.size.y, 0), rayDir);
		rays[1] = new Ray(new Vector3(xSide, p.y + collider.size.y, 0), rayDir);
		rays[2] = new Ray(new Vector3(xSide, p.y, 0), rayDir);
		
		foreach(Ray r in rays){
			Debug.DrawRay(r.origin, r.direction);
			if (Physics.Raycast(r, out hitInfo, Mathf.Max(Mathf.Abs(movement.x), errMargin), collisionMask)) { // see if we hit something
				mx = 0f;
                movement = new Vector3(0f, movement.y, 0f);
                break;
            }
        }

        
        
		transform.Translate (movement);
	}
	
	
}
