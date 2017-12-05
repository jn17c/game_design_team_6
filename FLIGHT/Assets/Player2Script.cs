using UnityEngine;
using System.Collections;

//Adding this allows us to access members of the UI namespace including Text.
using UnityEngine.UI;

public class Player2Script : MonoBehaviour {

	public static Player2Script p2;
	public float speed;				//Floating point variable to store the player's movement speed.
	public Text countText;			//Store a reference to the UI Text component which will display the number of pickups collected.
	public Text winText;			//Store a reference to the UI Text component which will display the 'You win' message.

	private Rigidbody2D rb2d;		//Store a reference to the Rigidbody2D component required to use 2D Physics.
	private int count;				//Integer to store the number of pickups collected so far.

	public float movementSpeed = 1000.0f;
	public float clockwise = 0.5f;
	public float counterClockwise = -5.0f;
	public float health = 100;

	// Use this for initialization
	void Start()
	{
		//Get and store a reference to the Rigidbody2D component so that we can access it.
		rb2d = GetComponent<Rigidbody2D> ();

		//Initialize count to zero.
		count = 0;

		//Initialze winText to a blank string since we haven't won yet at beginning.
		//	winText.text = "";

		//Call our SetCountText function which will update the text with the current value for count.
		SetCountText ();
	}

	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
	void Update()
	{
		if(Input.GetKey(KeyCode.I)) {
			rb2d.AddForce(transform.up * Time.deltaTime * 250);
		}
		else if(Input.GetKey(KeyCode.K)) {
			rb2d.AddForce(transform.up * Time.deltaTime * -250);
		}
		else if(Input.GetKey(KeyCode.O)) {
			rb2d.AddForce(transform.right * Time.deltaTime * -100);
		}
		else if(Input.GetKey(KeyCode.P)) {
			rb2d.AddForce(transform.right * Time.deltaTime * 100);
		}

		if (Input.GetKey (KeyCode.J)) {
			rb2d.AddTorque(1);
		} else if (Input.GetKey (KeyCode.L)) {
			rb2d.AddTorque(-1);
		}
		if (health == 0)
			Destroy (gameObject);
	}

	//OnTriggerEnter2D is called whenever this object overlaps with a trigger collider.
	void OnTriggerEnter2D(Collider2D other) 
	{
		//Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
		if (other.gameObject.CompareTag ("PickUp")) 
		{
			//... then set the other object we just collided with to inactive.
			other.gameObject.SetActive(false);

			//Add one to the current value of our count variable.
			count = count + 1;

			//Update the currently displayed count by calling the SetCountText function.
			SetCountText ();
		}


	}
	public void DamagePlayer(float damage)
	{
		health = health - damage;
	}

	//This function updates the text displaying the number of objects we've collected and displays our victory message if we've collected all of them.
	void SetCountText()
	{
		//Set the text property of our our countText object to "Count: " followed by the number stored in our count variable.
		//		countText.text = "Count: " + count.ToString ();

		//Check if we've collected all 12 pickups. If we have...
		if (count >= 12)
			//... then set the text property of our winText object to "You win!"
			winText.text = "You win!";
	}
}
