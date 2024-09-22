using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Test/ObjectParameters")]
public class ItemParameters : ScriptableObject
{
    public List<Sprite> Sprites;
    public List<Color> Colors;
}
