using UnityEngine;

public class CubeSpawner : MonoBehaviour {
    public bool canSpawn = true;
    public Transform bed;
    private GameObject cube;

    private float spawnRate = 0.01f;
    private float spawnTimer = 1f;

    // Start is called before the first frame update
    void Start() {
        cube = transform.GetChild(0).gameObject;
        cube.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (!canSpawn) {
            spawnTimer = spawnRate;
            return;
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate) {
            GameObject instantiatedCube = Instantiate(cube, transform.position, cube.transform.rotation);
            instantiatedCube.SetActive(true);

            instantiatedCube.transform.parent = bed;

            spawnTimer = 0f;
        }
    }
}
