using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftLockOnMechanic : MonoBehaviour
{
    [SerializeField]
    private BoxCollider _cone;

    [SerializeField]
    private CharacterController _characterController;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private InputProcessor _inputProcessor;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private Tag _tag;

    private Collider[] _colliderTemp = new Collider[10];
    private GameObject _latestLockOnTarget;

    // Update is called once per frame
    private void Update()
    {
        bool isAttacking = _animator.GetBool(AnimatorConst.IsAttacking) || _inputProcessor.IsAttackInputDown();

        if (_inputProcessor.IsAttackInputDown() || _inputProcessor.IsSkillButtonDown())
        {
            int count = Physics.OverlapBoxNonAlloc(_cone.transform.position, _cone.size / 2f, _colliderTemp, _cone.transform.rotation, _layerMask.value, QueryTriggerInteraction.Collide);

            float closestDistance = float.MaxValue;
            IHittable choosenObject = null;

            for (int i = 0; i < count; i++)
            {
                var collider = _colliderTemp[i];
                var hittable = collider.GetComponent<IHittable>();

                if (hittable.Tags != _tag)
                {
                    continue;
                }

                //if latest lock on target exist here, stop searching for new, let player lock to that target
                if (hittable.GameObject == _latestLockOnTarget)
                {
                    break;
                }

                var distance = Vector3.Distance(hittable.GameObject.transform.position, _characterController.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    choosenObject = hittable;
                }
            }

            if (choosenObject != null)
            {
                _latestLockOnTarget = choosenObject.GameObject;
            }
        }

        if (isAttacking)
        {
            if (_latestLockOnTarget != null)
            {
                _characterController.transform.LookAt(_latestLockOnTarget.transform);
            }
        }
        else
        {
            _latestLockOnTarget = null;
        }
    }
}
