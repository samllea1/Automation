using MelonLoader;
using UnityEngine;
using Il2CppScheduleOne;
using System.Collections;
using System;
using Il2CppScheduleOne.Storage;

namespace FactoryTest
{
    [RegisterTypeInIl2Cpp]
    public class LargeStorageRack : MonoBehaviour
    {
        public LargeStorageRack(IntPtr ptr) : base(ptr) { }

        public Transform transferPoint1 = null;
        public Transform transferPoint2 = null;
        public Transform transferPoint3 = null;
        public Transform transferPoint4 = null;

        public Transform itemSlot1 = null;
        public Transform itemSlot2 = null;
        public Transform itemSlot3 = null;
        public Transform itemSlot4 = null;

        private int layerMask = 1 << 17;

        void Start()
        {
            MelonCoroutines.Start(TransferItems());
        }

        IEnumerator TransferItems()
        {
            yield return new WaitForSeconds(0.25f);

            transferPoint1 = transform.FindChild("Transfer Point back_right");
            transferPoint2 = transform.FindChild("Transfer Point back_left");
            transferPoint3 = transform.FindChild("Transfer Point front_right");
            transferPoint4 = transform.FindChild("Transfer Point front_left");

            itemSlot1 = transform.FindChild("Item Output back_left");
            itemSlot2 = transform.FindChild("Item Output back_right");
            itemSlot3 = transform.FindChild("Item Output front_left");
            itemSlot4 = transform.FindChild("Item Output front_right");

            while (true)
            {
                yield return new WaitForSeconds(0.05f);
                if (Physics.CheckSphere(transferPoint1.position, 0.1f, layerMask))
                {
                    if (GetComponent<StorageEntity>().GetAllItems().Count > 0)
                    {
                        Collider[] colliders = Physics.OverlapSphere(transferPoint1.position, 0.1f, layerMask);
                        if (colliders.Length > 0)
                        {
                            
                            if (!colliders[0].GetComponent<conveyor>().containingItem)
                            {
                                colliders[0].GetComponent<conveyor>().containedItem = GetComponent<StorageEntity>().GetAllItems()[0].GetCopy(1);
                                colliders[0].GetComponent<conveyor>().containingItem = true;
                                GetComponent<StorageEntity>().GetAllItems()[0].Quantity = GetComponent<StorageEntity>().GetAllItems()[0].Quantity-1;
                                if (GetComponent<StorageEntity>().GetAllItems()[0].Quantity == 0)
                                {
                                    GetComponent<StorageEntity>().GetAllItems()[0].RequestClearSlot();
                                }
                            }
                        }
                    }
                    itemSlot1.gameObject.SetActive(true);
                }
                else
                {
                    itemSlot1.gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(0.05f);
                if (Physics.CheckSphere(transferPoint2.position, 0.1f, layerMask))
                {
                    if (GetComponent<StorageEntity>().GetAllItems().Count > 0)
                    {
                        Collider[] colliders = Physics.OverlapSphere(transferPoint2.position, 0.1f, layerMask);
                        if (colliders.Length > 0)
                        {
                            
                            if (!colliders[0].GetComponent<conveyor>().containingItem)
                            {
                                colliders[0].GetComponent<conveyor>().containedItem = GetComponent<StorageEntity>().GetAllItems()[0].GetCopy(1);
                                colliders[0].GetComponent<conveyor>().containingItem = true;
                                GetComponent<StorageEntity>().GetAllItems()[0].Quantity = GetComponent<StorageEntity>().GetAllItems()[0].Quantity - 1;
                                if (GetComponent<StorageEntity>().GetAllItems()[0].Quantity == 0)
                                {
                                    GetComponent<StorageEntity>().GetAllItems()[0].RequestClearSlot();
                                }
                            }
                        }
                    }
                    itemSlot2.gameObject.SetActive(true);
                }
                else
                {
                    itemSlot2.gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(0.05f);
                if (Physics.CheckSphere(transferPoint3.position, 0.1f, layerMask))
                {
                    if (GetComponent<StorageEntity>().GetAllItems().Count > 0)
                    {
                        Collider[] colliders = Physics.OverlapSphere(transferPoint3.position, 0.1f, layerMask);
                        if (colliders.Length > 0)
                        {
                            
                            if (!colliders[0].GetComponent<conveyor>().containingItem)
                            {
                                colliders[0].GetComponent<conveyor>().containedItem = GetComponent<StorageEntity>().GetAllItems()[0].GetCopy(1);
                                colliders[0].GetComponent<conveyor>().containingItem = true;
                                GetComponent<StorageEntity>().GetAllItems()[0].Quantity = GetComponent<StorageEntity>().GetAllItems()[0].Quantity - 1;
                                if (GetComponent<StorageEntity>().GetAllItems()[0].Quantity == 0)
                                {
                                    GetComponent<StorageEntity>().GetAllItems()[0].RequestClearSlot();
                                }
                            }
                        }
                    }
                    itemSlot3.gameObject.SetActive(true);
                }
                else
                {
                    itemSlot3.gameObject.SetActive(false);
                }
                yield return new WaitForSeconds(0.05f);
                if (Physics.CheckSphere(transferPoint4.position, 0.1f, layerMask))
                {
                    if (GetComponent<StorageEntity>().GetAllItems().Count > 0)
                    {
                        Collider[] colliders = Physics.OverlapSphere(transferPoint4.position, 0.1f, layerMask);
                        if (colliders.Length > 0)
                        {
                            
                            if (!colliders[0].GetComponent<conveyor>().containingItem)
                            {
                                colliders[0].GetComponent<conveyor>().containedItem = GetComponent<StorageEntity>().GetAllItems()[0].GetCopy(1);
                                colliders[0].GetComponent<conveyor>().containingItem = true;
                                GetComponent<StorageEntity>().GetAllItems()[0].Quantity = GetComponent<StorageEntity>().GetAllItems()[0].Quantity - 1;
                                if (GetComponent<StorageEntity>().GetAllItems()[0].Quantity == 0)
                                {
                                    GetComponent<StorageEntity>().GetAllItems()[0].RequestClearSlot();
                                }
                            }
                        }
                    }

                    itemSlot4.gameObject.SetActive(true);
                }
                else
                {
                    itemSlot4.gameObject.SetActive(false);
                }
            }
        }
    }
}
