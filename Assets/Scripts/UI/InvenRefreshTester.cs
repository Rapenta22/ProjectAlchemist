using UnityEngine;

public class InvenRefreshTester : MonoBehaviour
{
    [Tooltip("G Ű�� ������ �κ��丮 UI�� ������ ���ŵ˴ϴ�.")]
    public bool enableRefresh = true;

    private void Update()
    {
        if (!enableRefresh) return;

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (GManager.Instance != null && GManager.Instance.IsInventoryUI != null)
            {
                Debug.Log("[�׽�Ʈ] G Ű �Է����� �κ��丮 UI ���� ����");
                GManager.Instance.IsInventoryUI.UpdateUI();
            }
            else
            {
                Debug.LogWarning("GManager �Ǵ� InventoryUI�� ���� ���õ��� �ʾҽ��ϴ�.");
            }
        }
    }
}
