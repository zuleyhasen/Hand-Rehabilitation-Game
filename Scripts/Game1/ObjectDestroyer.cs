using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ObjectMover : MonoBehaviour
{
    public Transform point1_4;
    public Transform point1_8;
    public GameObject objectToMove;

  
    private float moveDistanceThreshold = 0.1f;
    public float minX = 2.4f;
    public float maxX = 5.0f;
    public float minY = -0.5f;
    public float maxY = 0.7f;
    public float minScale = 0.5f;
    public float maxScale = 2.0f;
    private CircleCollider2D objectCollider;



    void UpdateThreshold(string newValue)
    {
        
        float newThreshold;
        if (float.TryParse(newValue, out newThreshold))
        {
            moveDistanceThreshold = newThreshold;
        }
        else
        {
            Debug.LogError("Invalid input for moveDistanceThreshold!");
        }
    }

    public void BackButtonOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {

        
        float distance = Vector2.Distance(point1_4.position, point1_8.position);

       
        float objectScaleY = objectToMove.transform.localScale.y;
        Debug.Log(objectScaleY*6);
        
        //Debug.Log(IsPointOverObject(point1_4) && IsPointOverObject(point1_8));
        //Debug.Log((Mathf.Abs((distance) - (objectScaleY * 6)) < 0.15));

        
        if ((Mathf.Abs((distance) - (objectScaleY * 6)) < 0.15) && IsPointOverObject(point1_4) && IsPointOverObject(point1_8))
        {
            MoveObject();
        }
    }
    bool IsPointOverObject(Transform point)
    {
        Collider2D collider = objectToMove.GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.Log("Collider not found!");
            return false;
        }

        return collider.bounds.Contains(point.position);
    }

    void MoveObject()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomScale = Random.Range(minScale, maxScale);

        Vector2 newPosition = new Vector2(randomX, randomY);
        objectToMove.transform.localPosition = newPosition;

        
        objectToMove.transform.localScale = new Vector3(randomScale, randomScale, 1f);
        
    }
}
