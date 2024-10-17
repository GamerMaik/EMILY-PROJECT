using UnityEngine;

public class Enums : MonoBehaviour
{

}
public enum CharacterSlots
{
    CharacterSlot_01,
    CharacterSlot_02,
    CharacterSlot_03,
    CharacterSlot_04,
    CharacterSlot_05,
    CharacterSlot_06,
    CharacterSlot_07,
    CharacterSlot_08,
    CharacterSlot_09,
    CharacterSlot_10,
    NO_SLOT
}

public enum CharacterGroup
{
    Team01, //Son los aliados
    Team02, //Son los enemigos
}

public enum WeaponModelSlot
{
    RightHand,
    LeftHand,

    //Se pueden agregar muchas mas ranuras si se desea
}


//Esto se usa para calcular el da�o segun el tip de ataque
public enum AttackType
{
    LightAttack01,
    LightAttack02,
    HeavyAttack01,
    HeavyAttack02,
    ChargedAttack01,
    ChargedAttack02,
    RunningAttack01,
    RollingAttack01,
    BackstepAttack01,

}

//Esto se usa para calcular la intensidad de da�o para el bloqueo
public enum DamageIntensity
{
    Ping,
    Light,
    Medium,
    Heavy,
    Colosal
}