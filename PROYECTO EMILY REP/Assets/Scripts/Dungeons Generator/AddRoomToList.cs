using UnityEngine;

namespace KC
{
    public class AddRoomToList : MonoBehaviour
    {
        private DungeonsGeneratorTemplates templates;

        private void Start()
        {
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<DungeonsGeneratorTemplates>();
            templates.rooms.Add(this.gameObject);

        }
    }
}
