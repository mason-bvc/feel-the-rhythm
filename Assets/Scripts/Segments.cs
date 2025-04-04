using System;
using UnityEngine;


[Serializable]
public class Segments : MonoBehaviour
{
    // Mason: texture stuff
    private Material _wallMaterial;

    [SerializeField] private GameObject endObject;

    public GameObject GetEndObject()
    {  return endObject; }

    // Mason: materials

    public void Awake()
    {
        _wallMaterial = Resources.Load<Material>("Materials/Mason/ProceduralWall0");
    }

    public void Start()
    {
        Color[] colors = new Color[128 * 128];
        Texture2D t = new(128, 128, TextureFormat.RGBA32, 0, false);

        //
        // nested loop because flattening it incurrs a division and mod when
        // retrieving 2d coords per iteration. access pattern should be OK
        // because images are obviously row major and the inner loop is x.
        //
        for (int y = 0; y < 128; y++)
        {
            for (int x = 0; x < 128; x++)
            {
                float val = PerlinNoise.Perlin2D(0, x, y, 0.1F, 4);
                colors[y * 128 + x] = new(val, val, val);
            }
        }

        t.SetPixels(colors);
        t.Apply(true, false);

        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            var go = gameObject.transform.GetChild(i).gameObject;
            var meshRenderer = go.GetComponent<MeshRenderer>();

            if (meshRenderer != null)
            {
                _wallMaterial.mainTexture = t;
                meshRenderer.sharedMaterial = _wallMaterial;
            }
        }
    }
}
