using MelonLoader;
using UnityEngine;
using Il2CppScheduleOne;
using System.Collections;
using System;

namespace FactoryTest
{
    [RegisterTypeInIl2Cpp]
    public class TransferPoint : MonoBehaviour
    {
        public TransferPoint(IntPtr ptr) : base(ptr) { }
        public conveyor transferBox;
        private int layerMask = 1 << 17;

        void Start()
        {
            transferBox = transform.parent.FindChild("Transfer Box").GetComponent<conveyor>();
            MelonCoroutines.Start(TransferItems());
        }

        IEnumerator TransferItems()
        {
            while (true)
            {
                while (!transferBox.containingItem) { yield return new WaitForSeconds(0); }
                while (transferBox.containingItem)
                {
                    yield return new WaitForSeconds(0);
                    if (Physics.CheckSphere(transform.position, 0.1f, layerMask))
                    {
                        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f, layerMask);
                        if (colliders.Length > 0)
                        {
                            if (true)
                            {
                                if (!colliders[0].GetComponent<conveyor>().containingItem && transferBox.hasCompleted)
                                {
                                    colliders[0].GetComponent<conveyor>().containedItem = transferBox.containedItem;
                                    colliders[0].GetComponent<conveyor>().containingItem = true;
                                    transferBox.containedItem = null;
                                    transferBox.containingItem = false;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
