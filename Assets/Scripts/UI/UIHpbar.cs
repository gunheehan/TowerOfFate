using UnityEngine;
using UnityEngine.UI;

public class UIHpbar : MonoBehaviour, IUIInterface
{
    [SerializeField] private Slider hpslider;
    [SerializeField] private RectTransform rectCanvas;
    [SerializeField] private RectTransform rectHp;

    private float maxhp;
    private Transform monsterPos = null;

    private Vector2 initialPosition;
    public Vector3 offset;

    private void Start()
    {
        offset = new Vector3(0, 1, 0);
    }

    private void LateUpdate()
    {
        if (monsterPos != null)
        {
            UpdateHpBarPos();
        }
    }

    private void UpdateHpBarPos()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(monsterPos.position + offset);
            
        Vector2 localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectCanvas, screenPos, null, out localPos);
    
        rectHp.localPosition = localPos;
    }

    public void SetMonsterTransform(Transform trans)
    {
        monsterPos = trans;
    }

    public void SetHpbar(float hp)
    {
        maxhp = hp;
        hpslider.value = maxhp / maxhp;
    }

    public void UpdateHP(float currentHP)
    {
        hpslider.value = currentHP / maxhp; // 현재 HP 비율을 반영하여 HP 바를 업데이트
    }
}
