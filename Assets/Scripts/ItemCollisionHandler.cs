using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;


public class ItemCollisionHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ScoreManager _scoreManager;

    private Color activeColorItem;

    private void Start()
    {
        activeColorItem = GetComponent<UnityEngine.UI.Image>().color;
    }

    private void Update()
    {
        CheckForFallingObjects();
    }

    private void CheckForFallingObjects()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 15.0f);

        if (hit != null && hit.TryGetComponent<Basket>(out Basket component))
        {
            Color color = component.GetComponent<UnityEngine.UI.Image>().color;
            CompareColorsAndAdjustScore(color, activeColorItem);

            Destroy(gameObject);
        }
    }

    private void CompareColorsAndAdjustScore(Color color1, Color color2)
    {
        if (color1 == color2)
        {
            ScoreManager.Instance?.AddScore(1);
        }
        else
        {
            ScoreManager.Instance?.SubtractFromScore(1);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15.0f);
    }
}
