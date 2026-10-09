using UnityEngine;
public class RimBuilder : MonoBehaviour
{
    public int segments = 20;
    public float radius = 0.35f; // raza inelului
    public float thickness = 0.08f; // grosimea tubului
    public Material material;
    [ContextMenu("Build Rim")]
    void Build()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
            DestroyImmediate(transform.GetChild(i).gameObject);
        for (int i = 0; i < segments; i++)
        {
            float a = i * Mathf.PI * 2f / segments;
            GameObject s = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            s.transform.SetParent(transform, false);
            s.transform.localPosition = new Vector3(Mathf.Cos(a) * radius,
            0f, Mathf.Sin(a) * radius);
            s.transform.localScale = Vector3.one * thickness;
            if (material != null) s.GetComponent<Renderer>().sharedMaterial =
            material;
        }
    }
}
