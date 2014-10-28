using UnityEngine;
using System.Collections;
using Pathfinding;

public class AIPather : MonoBehaviour {

	public Transform target;
	public float speed = 10f;
	public float maxWaypointDistance = 2f;

	private Seeker seeker;
	private Path path;
	private int currentWaypoint;

	void Start() {
		seeker = GetComponent<Seeker> ();
		seeker.StartPath (transform.position, target.position, OnPathComplete);
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

		Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized * speed * Time.fixedDeltaTime;
		transform.position = Vector3.Lerp(transform.position, path.vectorPath[currentWaypoint], speed * Time.deltaTime);

		if(Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < maxWaypointDistance)
			currentWaypoint++;
	}
}
