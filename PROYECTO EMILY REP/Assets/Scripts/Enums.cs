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
    RightHandWeaponSlot,
    RightHandShieldSlot,
    LeftHandWeaponSlot,
    LeftHandShieldSlot,
    BackSlot,
    //Se pueden agregar muchas mas ranuras si se desea
}

public enum WeaponModelType
{
    Weapon,
    Shield
}

public enum WeaponClass
{
    StraightSword,
    Spear,
    MediumShield
}

public enum EquipmentModelType
{
    FullHelmet,
    Hat,
    Hood,
    HelmetAccesory,
    FaceCover,
    Torso,
    Back,
    RightShoulder,
    RightUpperArm,
    RightElbow,
    RightLowerArm,
    LeftElbow,
    RightHand,
    LeftShoulder,
    LeftUpperArm,
    LeftLowerArm,
    LeftHand,
    Hips,
    RightLeg,
    RightKnee,
    RightFoot,
    LeftLeg,
    LeftKnee,
    LeftFoot,
}

public enum HeadEquipmentType
{
    FullHelmet,
    Hat,
    Hood,
    FaceCover
}

//Esto se usa para calcular el daño segun el tip de ataque
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

//Esto se usa para calcular la intensidad de daño para el bloqueo
public enum DamageIntensity
{
    Ping,
    Light,
    Medium,
    Heavy,
    Colosal
}