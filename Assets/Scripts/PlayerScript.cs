using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerScript : MonoBehaviour {

	public Vector2 speed = new Vector2(50,50);
	
	private Vector3 movement = new Vector3();
	
	public PlayerIndex controlNo = PlayerIndex.One;

	Animator animator;

	private bool stunned = false;
	public float timer = 0.75f;

	private bool invulnerable = false;

	float timeStunned = 0f;

	BoxCollider2D hitbox;

	// Update is called once per frame
	void Awake ()
	{
		animator = GetComponent<Animator> ();
		hitbox = GetComponent<BoxCollider2D> ();
	}

	void Update () 
	{
		GamePadState controller = GamePad.GetState (controlNo);

		float inputX = controller.ThumbSticks.Left.X;
		float inputY = controller.ThumbSticks.Left.Y;


		float leftDistance, rightDistance, upDistance, downDistance;
		leftDistance =  CalculateDistance (new Vector2(-1f, 0f));
		rightDistance = CalculateDistance (new Vector2(1f,  0f));
		upDistance =    CalculateDistance (new Vector2(0f,  1f));
		downDistance =  CalculateDistance (new Vector2(0f, -1f));

		if (inputX > 0 && rightDistance < 0.1) return;
		if (inputX < 0 && leftDistance < 0.1) return;
		if (inputY > 0 && upDistance < 0.1)	return;
		if (inputY < 0 && downDistance < 0.1) return;

		if (stunned)
		{
			if (Time.time - timeStunned > timer)
			{
				stunned = false;
				gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
				animator.SetBool ("Stunned", false);
			}
			else return;
		}
		if (invulnerable && Time.time - timeStunned > timer * 2)
			invulnerable = false;

		movement = new Vector3 (speed.x * inputX, speed.y * inputY, 0);

		movement *= Time.deltaTime;

		if (movement != Vector3.zero)
				animator.SetBool ("Moving", true);
		else
				animator.SetBool ("Moving", false);

		transform.Translate (movement);
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		PuckScript puck = collision.collider.gameObject.GetComponent<PuckScript> ();
		PlayerScript player = collision.collider.gameObject.GetComponent<PlayerScript> ();
		RumbleScript rumble = GetComponent<RumbleScript> ();


		if (puck != null)
		{
			SoundEffectsHelper.Instance.MakePuckHitSound();
			puck.rigidbody2D.AddForce(new Vector2(movement.x, movement.y));
			rumble.RumbleForSeconds (0.1f, 0f, 5f);
		}
		if (player != null)
		{
			// do knockout stuff
			if (player.movement.magnitude - movement.magnitude > 0 && !invulnerable)
			{
				rumble.RumbleForSeconds (timer, 5f, 0f);
				animator.SetBool("Stunned", true);
				stunned = true; invulnerable = true;
				timeStunned = Time.time;
				gameObject.GetComponent<Rigidbody2D>().isKinematic = true; 
				SoundEffectsHelper.Instance.MakePlayerHitSound();
			}
			else
			{
				rumble.RumbleForSeconds(0.1f, 5f, 0f);
			}
		}
		animator.SetBool ("Contact", true);
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		animator.SetBool ("Contact", false);
	}

	float CalculateDistance(Vector2 direction)
	{
		Vector2 pos = new Vector2 (gameObject.transform.position.x + hitbox.center.x,
		                           gameObject.transform.position.y + hitbox.center.y);
		RaycastHit2D hit = Physics2D.Raycast (pos, direction);
		if (hit.collider.gameObject.GetComponent<PuckScript>() == null && hit.collider.gameObject.GetComponent<PlayerScript>() == null)
		{
			Vector2 point = hit.point;

			if (direction.x == 0)
				return point.x - pos.x - hitbox.size.x;
			else
			    return point.y - pos.y - hitbox.size.y;
		}
		else return 1000;
	}

}
