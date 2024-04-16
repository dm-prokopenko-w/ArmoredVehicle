using Core;
using UnityEngine;

namespace PlayerSystem
{
    public class PlayerView : CharacterView
    {
        [SerializeField] private GameObject Line;
        [SerializeField] private GameObject UI;

        public void ActiveGame(bool value)
        {
            Line.SetActive(value);
            UI.SetActive(value);
        }
    }
}
