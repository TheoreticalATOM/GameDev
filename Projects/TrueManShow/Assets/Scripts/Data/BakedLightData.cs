using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Baked Light Data", menuName = "Baked Lighting", order = 0)]
public class BakedLightData : SerializedScriptableObject
{
    public Texture2D[] LightMaps;
    [SerializeField] public LightmapData[] Data;

    [Button()]
    public void ConstructLightMap()
    {
        Data = new LightmapData[LightMaps.Length];
        for (int i = 0; i < Data.Length; i++)
        {
            Data[i] = new LightmapData();
            Data[i].lightmapColor = LightMaps[i];
        }
    }
}
