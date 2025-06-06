using UnityEngine;

public class SetupTiles : MonoBehaviour
{
    [SerializeField] private GameObject _starterTile;
    void Start()
    {
        for (int i = 0; i < 11; i++)
        {
            Instantiate(_starterTile, new Vector3(i, 1, -0.2f),new Quaternion(0,0,0,0));
            Instantiate(_starterTile, new Vector3(i, 0, -0.2f), new Quaternion(0, 0, 0, 0));
            Instantiate(_starterTile, new Vector3(i, -1, -0.2f), new Quaternion(0, 0, 0, 0));
            Instantiate(_starterTile, new Vector3(i, -2, -0.2f), new Quaternion(0, 0, 0, 0));
            Instantiate(_starterTile, new Vector3(i, -3, -0.2f), new Quaternion(0, 0, 0, 0));
        }
    }
}
