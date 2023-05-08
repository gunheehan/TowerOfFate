using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster : MonoBehaviour
{
    private Animation anim;
    private List<Vector3> roadtarget;
    
    public float moveSpeed = 1f; // 이동 속도

    private int currentIndex = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        roadtarget = StageManager.Instance.Roadtarget;

        gameObject.transform.position = roadtarget[0];
        anim.CrossFade("Anim_Run");
        TargetManager.Instance.EnQueueTarget(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentIndex < roadtarget.Count)
        {
            Vector3 targetPosition = roadtarget[currentIndex];

            // 목표 위치로 이동 방향 설정
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(moveDirection);

            // 목표 위치로 이동
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // 목표 위치에 도달했을 때 다음 위치로 이동
            if (transform.position == targetPosition)
            {
                currentIndex++;
            }
        }
        else
        {
            currentIndex = 0;
        }
    }
}
