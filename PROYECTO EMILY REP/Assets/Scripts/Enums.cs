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

public enum Levels
{
    Level_01,
    Level_02,
    Level_03,
    Level_04,
    Level_05,
    Level_06
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
    HipsAccesories,
    RightLeg,
    RightKnee,
    RightFoot,
    LeftLeg,
    LeftKnee,
    LeftFoot,
}

public enum EquipmentType
{
     RightWeapon01,
     RightWeapon02,
     RightWeapon03,
     LeftWeapon01,
     LeftWeapon02,
     LeftWeapon03,
     head,
     Body,
     Legs,
     Hands
}

public enum HeadEquipmentType
{
    FullHelmet,
    Hat,
    Hood,
    FaceCover
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

//Esto se usa para determinar el tipo de item recogible
public enum ItemPickUpType
{
    WorldSpawn,
    CharacterDrop,
    ChestDrop
}

public enum LevelType
{
    SCRUM,
    XP,
    FDD,
    DSDM,
    CRYSTAL,
    LEANDEV
}