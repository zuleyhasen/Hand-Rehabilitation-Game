using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Movable : MonoBehaviour
{
    public InputField distanceThresholdInput;
    private float grabDistanceThreshold = 0.2f;
    public Transform thumbTip;
    public Transform[] fingerTips;
    public GameObject[] puzzlePieces;
    private GameObject grabbedPiece;
    private Vector3 offset;

    public Vector2 minPosition;
    public Vector2 maxPosition;

    void Start()
    {
        StartCoroutine(StartScattering());
    }

    IEnumerator StartScattering()
    {
        yield return new WaitForSeconds(2f);

        foreach (GameObject piece in puzzlePieces)
        {
            Vector3 targetPosition = new Vector3(
                Random.Range(minPosition.x, maxPosition.x),
                Random.Range(minPosition.y, maxPosition.y),
                piece.transform.position.z
            );
            StartCoroutine(MovePiece(piece.transform, targetPosition));
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator MovePiece(Transform pieceTransform, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = pieceTransform.position;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            pieceTransform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime);
            yield return null;
        }
    }

    private void Update()
    {
        float thumbDistance = Vector3.Distance(thumbTip.position, fingerTips[0].position);
        float fingerDistance1 = Vector3.Distance(fingerTips[1].position, fingerTips[2].position);
        float fingerDistance2 = Vector3.Distance(fingerTips[2].position, fingerTips[3].position);
        float fingerDistance3 = Vector3.Distance(fingerTips[1].position, fingerTips[3].position);
        float fingerDistance4 = Vector3.Distance(thumbTip.position, fingerTips[3].position);

        if (!string.IsNullOrEmpty(distanceThresholdInput.text))
        {
            grabDistanceThreshold = float.Parse(distanceThresholdInput.text);
        }
        else
        {
            grabDistanceThreshold = 0.1f;
        }

        if (thumbDistance < grabDistanceThreshold
            && fingerDistance1 < grabDistanceThreshold && fingerDistance2 < grabDistanceThreshold
            && fingerDistance3 < grabDistanceThreshold && fingerDistance4 < grabDistanceThreshold
            && grabbedPiece == null)
        {
            foreach (GameObject piece in puzzlePieces)
            {
                if (!piece.GetComponent<PiecesScript>().InRightPosition)
                {
                    if (IsThumbOverPiece(piece))
                    {
                        grabbedPiece = piece;
                        offset = grabbedPiece.transform.position - thumbTip.position;
                        break;
                    }
                }
            }
        }

        if (grabbedPiece != null && (thumbDistance >= grabDistanceThreshold || fingerDistance1 >= grabDistanceThreshold
            || fingerDistance2 >= grabDistanceThreshold || fingerDistance3 >= grabDistanceThreshold || fingerDistance4 >= grabDistanceThreshold))
        {
            grabbedPiece = null;
        }

        if (grabbedPiece != null)
        {
            Vector3 targetPosition = thumbTip.position + offset;
            grabbedPiece.transform.position = targetPosition;
        }
    }

    private bool IsThumbOverPiece(GameObject piece)
    {
        Collider2D collider = piece.GetComponent<Collider2D>();
        return collider.bounds.Contains(thumbTip.position);
    }
}