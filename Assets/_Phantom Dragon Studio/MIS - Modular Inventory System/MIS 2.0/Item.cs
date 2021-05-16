using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MIS {
    public enum ModifiableProperties
    {
        Strength,
        Agility,
        Intelligence,
        Endurance,
        HealthBase,
        HealthRegen,
        MEFBase,
        MEFRegen,
        Stamina,
        StamRegen,
        AttackSpeed,
        MovementSpeed,
        CriticalChance,
        DodgeChance,
        ElementalResistance,
        FireResistance,
        EarthResistance,
        WindResistance,
        DivineResistance,
        DarkResistance,
        ArcaneResistance,
        LightningResistance,
        WaterResistance,
    }

    public class Item : MonoBehaviour
    {

        private List<Dictionary<ModifiableProperties, int>> _modifiableProperties;

        public List<Dictionary<ModifiableProperties, int>> ModifiableProperties { get => _modifiableProperties; private set => _modifiableProperties = value; }

        public Dictionary<ModifiableProperties, int> modTypeAndValues = new Dictionary<ModifiableProperties, int>();

    }
}
