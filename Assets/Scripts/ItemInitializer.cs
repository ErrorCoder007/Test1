using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class ItemInitializer : MonoBehaviour
{
    [SerializeField] private ItemParameters _itemParameters;
    private UnityEngine.UI.Image image;

    private void Awake()
    {
        image = GetComponent<UnityEngine.UI.Image>();

        SetParameter();
    }

    private void SetParameter()
    {
        image.sprite = GetRandomSprite();
        image.color = GetRandomColor();
    }

    private Sprite GetRandomSprite()
    {
        List<Sprite> sprites = _itemParameters.Sprites;
        return sprites[Random.Range(0, sprites.Count - 1)];
    }

    private Color GetRandomColor()
    {
        List<Color> colors = _itemParameters.Colors;
        return colors[Random.Range(0, colors.Count - 1)];
    }
}
