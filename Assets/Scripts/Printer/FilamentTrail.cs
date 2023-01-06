using UnityEngine;

public class FilamentTrail : MonoBehaviour {
    public MoveOnAxis x;
    public MoveOnAxis y;
    public MoveOnAxis z;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Vector3 targetPosition = new Vector3(x.transform.localPosition.x, z.transform.localPosition.y, 0f);

        transform.position = targetPosition;
    }
}
