using UnityEngine;
using UnityEngine.UI;

public class UIHpbar : MonoBehaviour
{
    [SerializeField] private Slider hpslider; // 몬스터 HP를 조절할 Slider UI 요소

    private float maxhp;

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
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
