using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class ItemSpawner : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField, Range(10.0f, 50.0f)] private float _edgeOffset = 25.0f;
    [SerializeField, Range(5.0f, 200.0f)] private float _speedSpawn = 100.0f;
    [SerializeField, Range(0.5f, 3.0f)] private float _spawnInterval = 1.5f;
    public float SpeedSpawn { set { _speedSpawn = Mathf.Clamp(value, 0.0f, 10.0f); } }

    [Space(10)]
    [Header("Components")]
    [SerializeField] private GameObject _canvas;

    [Space(10)]
    [Header("Prefabs")]
    [SerializeField] private GameObject _item;

    private float canvasHeightRadius;
    private float canvasWidthRadius;

    void Start()
    {
        GetCanvasHeightRadius();
        GetCanvasWidthRadius();

        StartCoroutine(SpawnItem());
    }

    public void RestartSpawn()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void GetCanvasHeightRadius()
    {
        canvasHeightRadius = _canvas.GetComponent<RectTransform>().rect.height / 2.0f;
    }

    private void GetCanvasWidthRadius()
    {
        canvasWidthRadius = (_canvas.GetComponent<RectTransform>().rect.width / 2.0f) - _edgeOffset;
    }

    private IEnumerator SpawnItem()
    {
        while (true)
        {
            GameObject newItem = Instantiate(_item, Vector2.zero, Quaternion.identity, transform);

            SetPosition(newItem);
            StartCoroutine(MoveItem(newItem));

            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SetPosition(GameObject item)
    {
        Vector2 position = FindPosition();
        item.GetComponent<RectTransform>().anchoredPosition = position;
    }

    private Vector2 FindPosition()
    {
        Vector2 randomPos = GetRandomTopPosition();
        Vector2 randomPosLocal = transform.InverseTransformPoint(_canvas.transform.TransformPoint(randomPos));

        return randomPosLocal;
    }

    private Vector2 GetRandomTopPosition()
    {
        float posX = Random.Range(-canvasWidthRadius, canvasWidthRadius);
        return new Vector2(posX, canvasHeightRadius);
    }

    private IEnumerator MoveItem(GameObject item)
    {
        RectTransform itemRectTransform = item.GetComponent<RectTransform>();
        Vector2 itemPosition = itemRectTransform.anchoredPosition;

        Vector2 targetPosition = new Vector2(itemPosition.x, -canvasHeightRadius);

        while (item != null)
        {
            itemRectTransform.anchoredPosition = Vector3.MoveTowards(itemPosition, targetPosition, _speedSpawn * Time.deltaTime);
            itemPosition = itemRectTransform.anchoredPosition;

            if (Mathf.Approximately(targetPosition.y, itemPosition.y))
            {
                Destroy(item);
                yield break;
            }
            yield return null;
        }
    }
}