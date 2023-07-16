using UnityEngine;
using UnityEngine.UI;

public class UIHpbar : MonoBehaviour, IUIInterface
{
    [SerializeField] private Slider hpslider; // 몬스터 HP를 조절할 Slider UI 요소

    private float maxhp;
    private Transform monsterPos = null;

    private RectTransform canvasRectTransform; // 캔버스의 RectTransform 컴포넌트
    private Vector2 initialPosition;
    private void OnEnable()
    {
        
    }
    private void LateUpdate()
    {
        if (monsterPos != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(monsterPos.position + offset); //월드좌표(3D)를 스크린좌표(2D)로 변경, offset은 오브젝트 머리 위치

            Debug.Log(Camera.main.name);
            Debug.Log(uiCamera);
            Debug.Log(canvas.worldCamera);
            Vector2 localPos = Vector2.zero;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, null, out localPos); //스크린좌표에서 캔버스에서 사용할 수 있는 좌표로 변경?
    
            rectHp.localPosition = localPos; //그 좌표를 localPos에 저장, 거기에 hpbar를 출력
        }
    }
    private Camera uiCamera; //UI 카메라를 담을 변수
    private Canvas canvas; //캔버스를 담을 변수
    private RectTransform rectParent; //부모의 rectTransform 변수를 저장할 변수
    private RectTransform rectHp; //자신의 rectTransform 저장할 변수

    //HideInInspector는 해당 변수 숨기기, 굳이 보여줄 필요가 없을 때 
    public Vector3 offset = Vector3.zero; //HpBar 위치 조절용, offset은 어디에 HpBar를 위치 출력할지
    public void SetMonsterTransform(Transform trans)
    {
        monsterPos = trans;
        
        canvas = this.gameObject.GetComponentInParent<Canvas>(); //부모가 가지고있는 canvas 가져오기, Enemy HpBar canvas임
        uiCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = hpslider.gameObject.GetComponent<RectTransform>();
        offset = new Vector3(0, 1, 0);
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
