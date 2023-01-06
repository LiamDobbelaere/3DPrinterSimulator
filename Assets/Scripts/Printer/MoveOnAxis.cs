using UnityEngine;

public enum Axis {
    X,
    Y,
    Z
}

public class MoveOnAxis : MonoBehaviour {
    public Axis virtualAxis;
    public Vector3 actualAxis;

    private float speed;
    private float targetPosition;

    private Vector3 origin;

    private bool moving = false;

    // Start is called before the first frame update
    void Start() {
        origin = transform.localPosition;
    }

    // Update is called once per frame
    void Update() {
        if (moving) {
            Vector3 targetLocation = origin + actualAxis * targetPosition;
            if (Vector3.Distance(transform.localPosition, targetLocation) <= float.Epsilon) {
                moving = false;
            }

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetLocation, speed * Time.deltaTime);
        }
    }

    public void MoveTo(float newPosition, float newSpeed) {
        // convert from mm to m
        newSpeed *= 0.001f;
        newPosition *= 0.001f;

        this.speed = newSpeed;
        this.targetPosition = newPosition;

        moving = true;
    }

    public bool IsMoving() {
        return moving;
    }
}
