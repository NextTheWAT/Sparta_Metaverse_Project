using UnityEngine;

public class LayerManager : MonoBehaviour
{

    [Header("���̾� ���� ���")]
    public LayerData[] layerSettings;

    [System.Serializable]
    public class LayerData
    {
        [Header("���� ������Ʈ")]
        public GameObject targetObject; // ���� ������Ʈ
        public string sortingLayerName = "Layer 1";
        public int orderInLayer = 0;

        [Header("��� ���̾� ����")]
        public bool useRelativeOrder = false;
        public GameObject[] affectedObjects; // �� �迭�� �ִ� �ֵ��� ��� �����
        public int relativeOffset = 0;       // ���� ������Ʈ���� �󸶳� ���̳��� ����

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

            // ���� ������Ʈ ����
            SpriteRenderer[] baseRenderers = layer.targetObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (var renderer in baseRenderers)
            {
                renderer.sortingLayerName = layer.sortingLayerName;
                renderer.sortingOrder = layer.orderInLayer;
            }

            // ��� ���� ������ ������Ʈ ó��
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
