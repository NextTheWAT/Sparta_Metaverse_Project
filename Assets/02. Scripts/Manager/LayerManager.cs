using UnityEngine;

public class LayerManager : MonoBehaviour
{

    [Header("레이어 설정 목록")]
    public LayerData[] layerSettings;

    [System.Serializable]
    public class LayerData
    {
        [Header("기준 오브젝트")]
        public GameObject targetObject; // 기준 오브젝트
        public string sortingLayerName = "Layer 1";
        public int orderInLayer = 0;

        [Header("상대 레이어 설정")]
        public bool useRelativeOrder = false;
        public GameObject[] affectedObjects; // 이 배열에 있는 애들이 상대 적용됨
        public int relativeOffset = 0;       // 기준 오브젝트보다 얼마나 차이나게 할지

    }


    void Start()
    {
        ApplyLayers();
    }

    public void ApplyLayers()
    {
        foreach (var layer in layerSettings)
        {
            if (layer.targetObject == null)
                continue;

            // 기준 오브젝트 설정
            SpriteRenderer[] baseRenderers = layer.targetObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in baseRenderers)
            {
                renderer.sortingLayerName = layer.sortingLayerName;
                renderer.sortingOrder = layer.orderInLayer;
            }

            // 상대 적용 설정된 오브젝트 처리
            if (layer.useRelativeOrder && layer.affectedObjects != null)
            {
                foreach (var obj in layer.affectedObjects)
                {
                    if (obj == null) continue;

                    SpriteRenderer[] renderers = obj.GetComponentsInChildren<SpriteRenderer>();
                    foreach (var renderer in renderers)
                    {
                        renderer.sortingLayerName = layer.sortingLayerName;
                        renderer.sortingOrder = layer.orderInLayer + layer.relativeOffset;
                    }
                }
            }
        }
    }


}
