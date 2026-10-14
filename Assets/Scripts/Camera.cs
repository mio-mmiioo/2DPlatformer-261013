using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float offsetX; // カメラのx座標の補正値
    [SerializeField] private float offsetY; // カメラのy座標の補正値 

    private const string PLAYER_TAG = "Player"; // プレイヤーのタグ名

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag(PLAYER_TAG);

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x = player.position.x + offsetX;
        position.y = offsetY;
        transform.position = position;
    }
}
