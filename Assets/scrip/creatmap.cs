using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class creatmap : MonoBehaviour
{
    public GameObject objectPrefab; // Prefab của đối tượng sẽ spawn
    private GameObject spawnedObject; // Đối tượng đã được spawn
    private bool isFollowing = false;
    public Tilemap tilemap;
    private bool isClick = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            isFollowing = true;
        else if (Input.GetMouseButtonDown(1))
            isFollowing = false;
        if (isFollowing) 
            // Kiểm tra nếu chuột trái được nhấn
        {
            // Lấy vị trí của chuột trong thế giới 2D
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; // Đảm bảo z là 0 cho đối tượng 2D
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);
            Vector3 cellCenterWorld = tilemap.GetCellCenterWorld(cellPosition);

            // Spawn đối tượng tại vị trí của chuột
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(objectPrefab, mouseWorldPos, Quaternion.identity);
                isFollowing = true;
            }
            else
            {
                // Kiểm tra nếu chuột nhấn vào đối tượng đã spawn
                if (spawnedObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint(mouseWorldPos))
                {
                    isFollowing = true;
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                spawnedObject = Instantiate(objectPrefab, cellPosition, Quaternion.identity);
            }
        }
        else
        {
            Destroy(spawnedObject);
        }

        

        if (isFollowing && spawnedObject != null)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cellPosition = tilemap.WorldToCell(mouseWorldPos);
            Vector3 cellCenterWorld = tilemap.GetCellCenterWorld(cellPosition);

            // Di chuyển đối tượng đến trung tâm của tile
            spawnedObject.transform.position = cellCenterWorld;
        }
    }
}

