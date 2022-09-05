using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Characters", menuName = "CharacterPrefabs/CharacterInformation", order = 1)]
public class CharacterScripableObjects : ScriptableObject
{
    public new string name;
    public GameObject prefab;
    public int maxHealth;
    public Sprite image;
}
