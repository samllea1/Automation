using System;
using System.Collections;
using Il2CppScheduleOne.ItemFramework;
using Il2CppScheduleOne.Storage;
using MelonLoader;
using UnityEngine;

namespace FactoryTest
{

    [RegisterTypeInIl2Cpp]
    public class conveyor : MonoBehaviour
    {
        public conveyor(IntPtr ptr) : base(ptr) { }

        public Boolean containingItem = false;
        public ItemInstance containedItem;
        public StorageEntity storage;
        public Boolean hasCompleted = true;
        
        void Start()
        {
            storage = transform.parent.GetComponent<StorageEntity>();
            MelonCoroutines.Start(DisplayItems());
        }

        IEnumerator DisplayItems()
        {
            while (true)
            {
                storage.ClearContents();
                while (!containingItem) { yield return new WaitForSeconds(0); }
                storage.InsertItem(containedItem.GetCopy(1), false);
                while (transform.parent.FindChild("Stored Items").childCount == 0)
                {
                    yield return new WaitForSeconds(0);
                }
                transform.parent.FindChild("Stored Items").GetChild(0).transform.localPosition = new Vector3(0f, 0.5125f, -1f);
                hasCompleted = false;
                for (int i = 0; i < 25; i++)
                {
                    transform.parent.FindChild("Stored Items").GetChild(0).transform.localPosition += new Vector3(0f, 0f, 0.04f);
                    transform.parent.FindChild("Stored Items").GetChild(0).rotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.parent.FindChild("Stored Items").GetChild(0).GetChild(0).rotation = Quaternion.Euler(0f, 0f, 0f);
                    yield return new WaitForSeconds(0.001f);
                }
                hasCompleted = true;
                while (containingItem) { yield return new WaitForSeconds(0); }
            }
        }
    }
}
