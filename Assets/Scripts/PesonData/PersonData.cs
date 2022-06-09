using UnityEngine;
using UnityEngine.UI;

namespace PesonData
{
    public class PersonData : MonoBehaviour
    {
        [SerializeField] private Text NameText;
        
        public void SetName(string name) 
            => NameText.text = $"Имя: {name}";
    }
}