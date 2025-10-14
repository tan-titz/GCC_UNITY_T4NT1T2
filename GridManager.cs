using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Setting")]
    public Vector2Int gridSize = new Vector2Int(5, 5);
    public Vector2 cellSize = new Vector2(1, 1);
    public Color gridColor = Color.white;

    private Vector3 gridOrigin;
    private Camera mainCamera;
    private float totalWidth, totalHeight;

    void Start()
    {
        mainCamera = Camera.main;
        CalculateGridOrigin();

    }

    void CalculateGridOrigin()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        if (mainCamera != null)
        {
            totalWidth = gridSize.x * cellSize.x;
            totalHeight = gridSize.y * cellSize.y;

            Vector3 cameraPosition = mainCamera.transform.position;
            gridOrigin = new Vector3(cameraPosition.x - totalWidth / 2f, cameraPosition.y - totalHeight / 2f, 0);
        }
    }

    private void OnDrawGizmos()
    {
        CalculateGridOrigin();
        Gizmos.color = gridColor;

        // Hàng ngang
        for (int i = 0; i <= gridSize.y; i++)
        {
            Vector3 start = gridOrigin + new Vector3(0, i * cellSize.y, 0);
            Vector3 end = gridOrigin + new Vector3(totalWidth, i * cellSize.y, 0);
            Gizmos.DrawLine(start, end);
        }

        // Cột dọc
        for (int i = 0; i <= gridSize.x; i++)
        {
            Vector3 start = gridOrigin + new Vector3(i * cellSize.x, 0, 0);
            Vector3 end = gridOrigin + new Vector3(i * cellSize.x, totalHeight, 0);
            Gizmos.DrawLine(start, end);
        }
    }

    void OnMouseDown()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null) return;
        }

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0;

        int x = Mathf.FloorToInt((mouseWorld.x - gridOrigin.x) / cellSize.x);
        int y = Mathf.FloorToInt((mouseWorld.y - gridOrigin.y) / cellSize.y);

        if (x >= 0 && x < gridSize.x && y >= 0 && y < gridSize.y)
        {
            Debug.Log($"Click vao o: ({x}, {y})");
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = mouseWorld;
            sphere.transform.localScale = Vector3.one * 0.3f;
            Destroy(sphere, 2f);
        }
        else
        {
            Debug.Log("Click ngoai grid");
        }
    }
}