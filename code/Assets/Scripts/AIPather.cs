using UnityEngine;
using System.Collections;
using Pathfinding;

public class AIPather : MonoBehaviour {

	public Transform target;
	public Vector3 targetPosition;

	public float speed = 10f;
	public float maxWaypointDistance = 2f;

	private Seeker seeker;
	private Path path;
	private int currentWaypoint;

	void Start() {
		targetPosition = target.transform.position;
		seeker = GetComponent<Seeker> ();
		seeker.StartPath (transform.position, targetPosition, OnPathComplete);
	}

	public void OnPathComplete(Path p) {
		if (!p.error) {
				path = p;
				currentWaypoint = 0;
		} else {
			Debug.Log(p.error);
		}
	}

	void FixedUpdate() {
		if (path == null)
			return;

		if (currentWaypoint >= path.vectorPath.Count)
			return;

		transform.position = Vector3.Lerp(transform.position, path.vectorPath [currentWaypoint], speed * Time.fixedDeltaTime);

		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < maxWaypointDistance)
			currentWaypoint++;
	}
}
