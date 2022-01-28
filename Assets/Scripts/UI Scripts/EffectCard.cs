using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SS.UI
{
    public class EffectCard : MonoBehaviour
    {
        public Text name;
        public Text description;

        public void Despawn()
        {
            gameObject.SetActive(false);
        }

        public void Spawn(Spells.Effect effect)
        {
            if (effect == null) return;

            gameObject.SetActive(true);

            name.text = effect.effectName;
            description.text = effect.description;
        }
        public void Spawn(Spells.Modifier modifier)
        {
            if (modifier == null) return;

            gameObject.SetActive(true);

            name.text = modifier.modifierName;
            description.text = modifier.description;
        }
    }
}
