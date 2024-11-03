using UnityEngine;

namespace KC
{
    [CreateAssetMenu(menuName ="Equipment Model")]
    public class EquipmentModel : ScriptableObject
    {
        public EquipmentModelType equipmentModelType;
        public string maleEquipmentName;
        public string femaleEquipmentName;

        public void LoadModel(PlayerManager player, bool isMale)
        {
            if (isMale)
            {
                LoadMaleModel(player);
            }
            else
            {
                LoadFemaleModel(player);
            }
        }

        private void LoadMaleModel(PlayerManager player)
        {
            switch (equipmentModelType)
            {
                case EquipmentModelType.FullHelmet:
                    foreach (var model in player.playerEquipmentManager.maleHeadFullHelmets)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hat:
                    foreach (var model in player.playerEquipmentManager.hats)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hood:
                    foreach (var model in player.playerEquipmentManager.hoods)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.HelmetAccesory:
                    foreach (var model in player.playerEquipmentManager.helmetAccesories)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.FaceCover:
                    foreach (var model in player.playerEquipmentManager.faceCovers)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Torso:
                    foreach (var model in player.playerEquipmentManager.maleBodies)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Back:
                    foreach (var model in player.playerEquipmentManager.backAccesories)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightShoulder:
                    foreach (var model in player.playerEquipmentManager.rightShoulder)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightUpperArm:
                    foreach (var model in player.playerEquipmentManager.maleRightUpperArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightElbow:
                    foreach (var model in player.playerEquipmentManager.rightElbow)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightLowerArm:
                    foreach (var model in player.playerEquipmentManager.maleRightLowerArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightHand:
                    foreach (var model in player.playerEquipmentManager.maleRightHands)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftShoulder:
                    foreach (var model in player.playerEquipmentManager.leftShoulder)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftUpperArm:
                    foreach (var model in player.playerEquipmentManager.maleLeftUpperArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftElbow:
                    foreach (var model in player.playerEquipmentManager.leftElbow)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftLowerArm:
                    foreach (var model in player.playerEquipmentManager.maleLefttLowerArms)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftHand:
                    foreach (var model in player.playerEquipmentManager.maleLefttHands)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hips:
                    foreach (var model in player.playerEquipmentManager.malehips)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightLeg:
                    foreach (var model in player.playerEquipmentManager.maleRightLegs)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightKnee:
                    foreach (var model in player.playerEquipmentManager.rightKnee)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.HipsAccesories:
                    foreach (var model in player.playerEquipmentManager.hipAccesories)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftLeg:
                    foreach (var model in player.playerEquipmentManager.maleLeftLegs)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftKnee:
                    foreach (var model in player.playerEquipmentManager.leftKnee)
                    {
                        if (model.gameObject.name == maleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftFoot:
                    break;
                default:
                    break;
            }
        }
        private void LoadFemaleModel(PlayerManager player)
        {
            switch (equipmentModelType)
            {
                case EquipmentModelType.FullHelmet:
                    foreach (var model in player.playerEquipmentManager.femaleHeadFullHelmets)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hat:
                    foreach (var model in player.playerEquipmentManager.hats)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hood:
                    foreach (var model in player.playerEquipmentManager.hoods)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.HelmetAccesory:
                    foreach (var model in player.playerEquipmentManager.helmetAccesories)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.FaceCover:
                    foreach (var model in player.playerEquipmentManager.faceCovers)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Torso:
                    foreach (var model in player.playerEquipmentManager.femaleBodies)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Back:
                    foreach (var model in player.playerEquipmentManager.backAccesories)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightShoulder:
                    foreach (var model in player.playerEquipmentManager.rightShoulder)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightUpperArm:
                    foreach (var model in player.playerEquipmentManager.femaleRightUpperArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightElbow:
                    foreach (var model in player.playerEquipmentManager.rightElbow)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightLowerArm:
                    foreach (var model in player.playerEquipmentManager.femaleRightLowerArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightHand:
                    foreach (var model in player.playerEquipmentManager.femaleRightHands)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftShoulder:
                    foreach (var model in player.playerEquipmentManager.leftShoulder)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftUpperArm:
                    foreach (var model in player.playerEquipmentManager.femaleLeftUpperArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftElbow:
                    foreach (var model in player.playerEquipmentManager.leftElbow)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftLowerArm:
                    foreach (var model in player.playerEquipmentManager.femaleLefttLowerArms)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftHand:
                    foreach (var model in player.playerEquipmentManager.femaleLefttHands)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.Hips:
                    foreach (var model in player.playerEquipmentManager.femalehips)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightLeg:
                    foreach (var model in player.playerEquipmentManager.femaleRightLegs)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.RightKnee:
                    foreach (var model in player.playerEquipmentManager.rightKnee)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.HipsAccesories:
                    foreach (var model in player.playerEquipmentManager.hipAccesories)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftLeg:
                    foreach (var model in player.playerEquipmentManager.femaleLeftLegs)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                case EquipmentModelType.LeftKnee:
                    foreach (var model in player.playerEquipmentManager.leftKnee)
                    {
                        if (model.gameObject.name == femaleEquipmentName)
                        {
                            model.gameObject.SetActive(true);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
