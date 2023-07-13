using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHandler : MonoBehaviour
{

    public WeaponHandler WeaponHandlerRef;

    public LayerMask hitLayers;

    public bool DebugTrail = false;
    public struct BufferObj
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
        public Vector3 size;
    }

    private LinkedList<BufferObj> _trailList = new LinkedList<BufferObj>();
    LinkedList<BufferObj> _trailFillerList = new LinkedList<BufferObj>();
    private int _maxFrameBuffer = 4;
    private BoxCollider _weaponCollider;

    Animator _anim;

    int _attackId = 0;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _weaponCollider = (BoxCollider)WeaponHandlerRef.Weapon.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left Click
        {
            SetAttack(1);
        }
        else if (Input.GetMouseButtonDown(1)) // Right Click
        {
            SetAttack(2);
        }
        if (_anim.GetBool("IsDamageOn"))
        {
            CheckTrail();
        }
    }



    private void SetAttack(int attackType)
    {
        if (_anim.GetBool("CanAttack"))
        {
            _attackId++;
            _anim.SetTrigger("Attack");
            _anim.SetInteger("AttackType", attackType);
        }
    }
    private void CheckTrail()
    {
        //Stores the collider position, size and rotation.
        BufferObj bo = new BufferObj();
        bo.size = _weaponCollider.size;
        bo.scale = _weaponCollider.transform.localScale;
        bo.rotation = _weaponCollider.gameObject.transform.rotation;
        bo.position = _weaponCollider.transform.position + /**/ _weaponCollider.transform.TransformVector(_weaponCollider.center);
        _trailList.AddFirst(bo);

        if (_trailList.Count > _maxFrameBuffer)
        {
            _trailList.RemoveLast();
        }

        if (_trailList.Count > 1)
        {
            _trailFillerList = FillTrail(_trailList.First.Value, _trailList.Last.Value);
        }

        Collider[] hits = Physics.OverlapBox(bo.position, bo.size / 2, bo.rotation, hitLayers, QueryTriggerInteraction.Ignore);

        Dictionary<long, Collider> colliderList = new Dictionary<long, Collider>();
        CollectColliders(hits, colliderList);

        foreach (BufferObj cbo in _trailFillerList)
        {
            hits = Physics.OverlapBox(cbo.position, cbo.size / 2, cbo.rotation, hitLayers, QueryTriggerInteraction.Ignore);
            CollectColliders(hits, colliderList);
        }
        foreach (Collider collider in colliderList.Values)
        {
            HitData hd = new HitData();
            hd.id = _attackId;
            Hittable hittable = collider.GetComponent<Hittable>();
            if (hittable)
            {
                hittable.Hit(hd);
            }
        }
    }

    private static void CollectColliders(Collider[] hits, Dictionary<long, Collider> colliderList)
    {
        for (int i = 0; i < hits.Length; i++)
        {
            if (!colliderList.ContainsKey(hits[i].GetInstanceID())) 
            {
            colliderList.Add(hits[i].GetInstanceID(), hits[i]);
            }
        }
    }

    private LinkedList<BufferObj> FillTrail(BufferObj from, BufferObj to)
    {
        LinkedList<BufferObj> fillerList = new LinkedList<BufferObj>();
        float distance = Mathf.Abs((from.position - to.position).magnitude);
        if (distance>_weaponCollider.size.z)
        {
            float steps = Mathf.Ceil(distance / _weaponCollider.size.z);
            float stepsAmount = 1 / (steps + 1);
            float stepValue = 0;
            for (int i = 0; i < (int)steps; i++)
            {
                stepValue += stepsAmount;
                BufferObj tmpBo = new BufferObj();
                tmpBo.size = _weaponCollider.size;
                tmpBo.position = Vector3.Lerp(from.position, to.position, stepValue);
                tmpBo.rotation = Quaternion.Lerp(from.rotation, to.rotation, stepValue);
                fillerList.AddFirst(tmpBo);
            }
        }
        return fillerList;
    }
    private void OnDrawGizmos()
    {
        if (DebugTrail)
        {

            foreach (BufferObj bo in _trailList)
            {
                Gizmos.color = Color.blue;
                Gizmos.matrix = Matrix4x4.TRS(bo.position, bo.rotation, bo.scale);
                Gizmos.DrawWireCube(Vector3.zero, bo.size);
            }

            foreach (BufferObj bo in _trailFillerList)
            {
                Gizmos.color = Color.yellow;
                Gizmos.matrix = Matrix4x4.TRS(bo.position, bo.rotation, bo.scale);
                Gizmos.DrawWireCube(Vector3.zero, bo.size);
            }
        }
    }
}
