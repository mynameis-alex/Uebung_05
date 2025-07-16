using System;
using Unity.XR.CoreUtils;
using UnityEngine;

public class SparrowController : MonoBehaviour {
    private float flySpeed = 3f;
    private GameObject target;

    public GameObject targetsParent;
    private GameObject[] targets;
    private int targetIndex = 0;



  void Start()
  {
        //get targets
        targets = new GameObject[targetsParent.transform.childCount];

        for (int i = 0; i < targetsParent.transform.childCount; i++) {
            targets[i] = targetsParent.transform.GetChild(i).gameObject;
        }

        target = targets[0];
  }


  void Update() {

        //move to tree
        if (target != null) {

            lookAt(target);

            //check if target is reached, then fly to next target
            if (
                Mathf.Abs(transform.position.x - target.transform.position.x) < 0.1 &&
                Mathf.Abs(transform.position.y - target.transform.position.y) < 0.1 &&
                Mathf.Abs(transform.position.z - target.transform.position.z) < 0.1
            ) {
                nextTarget();
            } else {
                moveToTarget();
            }

        }
        
    }

    void nextTarget()
    {

        targetIndex++;

        if (targetIndex >= targets.Length)
        {
            targetIndex = 0;
        }

        target = targets[targetIndex];
     
    }

    //called when sphere isn't at same spot as wanted tree and therefore starts moving there
    void moveToTarget() {

        //pos of target without changing Y
        Vector3 targetPos = new Vector3(
            target.transform.position.x,
            target.transform.position.y,
            target.transform.position.z
        );
        //where to go and move
        Vector3 direction = (targetPos - transform.position).normalized;
        transform.position += direction * flySpeed * Time.deltaTime;

    }

    void lookAt(GameObject target) {
        //smooth transition cause lookAt rotates object directly
        //source: https://discussions.unity.com/t/smooth-look-at/394528
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1.2f * Time.deltaTime);
    }

}
