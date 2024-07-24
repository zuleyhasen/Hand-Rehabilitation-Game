using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] Rigidbody2D rb;
    public InputField thresholdInputField; // InputField for yDifferenceThreshold

    public float dragLimit = 3f;
    public float forceToAdd = 35f;
    public Transform[] fingerTips; // Parmak uçlarý
    public Transform[] fingerJoints; // Parmak sonu eklemleri
    public Transform IndexTip;
    private Camera cam;
    private bool isDragging;
    // Baþlangýç konumu için deðiþken
    private Vector3 initialPosition;
    private bool isThrown = false;
    private float returnDelay = 3f; // Topu baþlangýç noktasýna döndürme gecikmesi


    public float yDifferenceThreshold = -0.05f; // Y Difference threshold

    Vector3 IndexPosition
    {
        get
        {
            Vector3 pos = IndexTip.position;
            pos.z = 0f;
            return pos;
        }
    }

    private void Start()
    {
        cam = Camera.main;
        line.positionCount = 2;
        line.SetPosition(0, Vector2.zero);
        line.SetPosition(1, Vector2.zero);
        line.enabled = false;
        initialPosition = rb.transform.position;

        // Listen for changes in the input field value
        thresholdInputField.onEndEdit.AddListener(UpdateThreshold);
    }

    // Update the yDifferenceThreshold value when the input field value changes
    void UpdateThreshold(string newValue)
    {
        float newThreshold;
        if (float.TryParse(newValue, out newThreshold))
        {
            yDifferenceThreshold = newThreshold;
        }
        else
        {
            Debug.LogError("Invalid input for yDifferenceThreshold!");
        }
    }
    public void BackButtonOnClick()
    {
        SceneManager.LoadScene("Menu");
    }

    private void Update()
    {
        bool yumruk = true;
        // Tüm parmak uçlarýnýn ve eklemlerinin y eksen farklarýný kontrol et
        for (int i = 0; i < fingerTips.Length; i++)
        {
            float yDifference = fingerTips[i].position.y - fingerJoints[i].position.y;

            // Eðer fark yDifferenceThreshold'ten büyükse, zýplamayý iptal et
            if (yDifference >= yDifferenceThreshold)
            {
                yumruk = false;
                break;
            }
        }
        if (yumruk && !isDragging)
        {
            DragStart();
        }
        if (isDragging)
        {
            Drag();
        }
        if (!yumruk && isDragging)
        {
            DragEnd();
        }
    }

    void DragStart()
    {
        line.enabled = true;
        isDragging = true;
        line.SetPosition(0, IndexPosition);
    }
    void Drag()
    {
        Vector3 startPos = line.GetPosition(0);
        Vector3 currentPos = IndexPosition;

        Vector3 distance = currentPos - startPos;

        if (distance.magnitude <= dragLimit)
        {
            line.SetPosition(1, currentPos);
        }
        else
        {
            Vector3 limitVector = startPos + (distance.normalized * dragLimit);
            line.SetPosition(1, limitVector);
        }


    }
    private void DragEnd()
    {
        isDragging = false;
        line.enabled = false;
        Vector3 startPos = line.GetPosition(0);
        Vector3 currentPos = line.GetPosition(1);

        Vector3 distance = currentPos - startPos;
        Vector3 finalForce = distance * forceToAdd;
        rb.AddForce(-finalForce, ForceMode2D.Impulse);



        // Topu baþlangýç noktasýna döndürme iþlemini baþlat
        StartCoroutine(ReturnToStartPosition());
    }
    private IEnumerator ReturnToStartPosition()
    {
        yield return new WaitForSeconds(returnDelay);
        // Topun hareketini durdur
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        // Topu baþlangýç noktasýna geri döndür
        rb.position = initialPosition;
    }
}
