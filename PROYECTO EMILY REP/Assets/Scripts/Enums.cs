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

public enum WeaponModelSlot
{
    RightHand,
    LeftHand,

    //Se pueden agregar muchas mas ranuras si se desea
}


//Esto se usa para calcular el daño segun el tip de ataque
public enum AttackType
{
    LightAttack01,
    LightAttack02,
    HeavyAttack01,
    ChargedAttack01,
    ChargedAttack02,

}