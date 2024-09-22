using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
    [Header("Parameter")]
    [SerializeField] private float _colorChangeRate = 5.0f;

    [Space(10)]
    [Header("Components")]
    [SerializeField] private ItemParameters _itemParameters;

    [Space(10)]
    [SerializeField] private Image _image;

    public Color ActiveColor { get; private set; }

    private void Start()
    {
        _image = GetComponent<Image>();
        StartCoroutine(ChangeColor());
    }
    private IEnumerator ChangeColor()
    {
        while (true)
        {
            _image.color = _itemParameters.Colors[Random.Range(0, _itemParameters.Colors.Count - 1)];
            yield return new WaitForSeconds(_colorChangeRate);
        }
    }
}
