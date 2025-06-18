using UnityEngine;
using MelonLoader;
using Il2CppScheduleOne;
using System;
using Il2CppScheduleOne.ObjectScripts;
using System.Collections;

namespace FactoryTest
{
    [RegisterTypeInIl2Cpp]
    public class AutoGrowTent : MonoBehaviour
    {
        public AutoGrowTent(IntPtr ptr) : base(ptr) { }
        public Pot growTent;
        private int layerMask = 1 << 17;

        void Start()
        {
            growTent = GetComponent<Pot>();
            growTent.GrowSpeedMultiplier = 50;
            MelonCoroutines.Start(OutputItems());
        }

        IEnumerator OutputItems()
        {
            while (true)
            {
                while (growTent.Plant == null) { yield return new WaitForSeconds(0); }
                while (growTent.Plant.NormalizedGrowthProgress != 1) { yield return new WaitForSeconds(0); }
                while (growTent.Plant != null)
                {
                    if (Physics.CheckSphere(transform.FindChild("Transfer Point").position, 0.1f, layerMask))
                    {
                        Collider[] colliders = Physics.OverlapSphere(transform.FindChild("Transfer Point").position, 0.1f, layerMask);
                        if (colliders.Length > 0)
                        {
                            if (colliders[0].GetComponent<conveyor>().containingItem == false)
                            {
                                int budCount = 0;

                                for (int i = 0; i < 15; i++)
                                {
                                    if (growTent.Plant.transform.GetChild(0).GetChild(10).GetChild(3).GetChild(i).gameObject.active == true)
                                    {
                                        budCount++;
                                    }
                                }

                                if (!colliders[0].GetComponent<conveyor>().containingItem)
                                {
                                    colliders[0].GetComponent<conveyor>().containedItem = growTent.Plant.GetHarvestedProduct(budCount);
                                    colliders[0].GetComponent<conveyor>().containingItem = true;
                                    growTent.Plant.Destroy();
                                }

                            }
                        }
                    }

                    yield return new WaitForSeconds(0);
                }
            }
        }
    }
}
