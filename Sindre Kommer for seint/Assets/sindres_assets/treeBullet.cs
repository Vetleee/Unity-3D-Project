using UnityEngine;
using System.Collections;

public class treeBullet : MonoBehaviour {
	public float speed;
	private bool hit; 
	Vector3 _direction; 
	bool isReady;
	public int maxRange = 40; 

	void Awake()
	{
		speed = speed*Time.deltaTime;
		isReady = false; 
	}

	// Use this for initialization
	void Start () 
	{

	}

	public void SetDirection(Vector3 direction)
	{
		_direction = direction.normalized;

		isReady = true;

	}

	void FixedUpdate()
	{

		float translation = Time.deltaTime * 10;
		transform.Translate(5, 0, translation);

		if (isReady) 
		{
			Vector3 position = transform.position;

			position += _direction * speed; //* Time.deltaTime;

			transform.position = position;

			//			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

			//			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

			//			if((transform.position.x <min.x) || (transform.position.x <min.x) ||
			//				(transform.position.x <min.x) || (transform.position.x <min.x))
			//			{
			//				Destroy(gameObject);
			//			}
			//destroy after a certain range
			if (maxRange > 0) {
				maxRange--;
			}
			if (maxRange <= 0) {
				Destroy (gameObject);
				Destroy (this);
			}

		}
	}
	void OnTriggerEnter(Collider other)
	{
		Destroy (this.gameObject);
	}
}
