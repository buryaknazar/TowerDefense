using UnityEngine;

namespace General
{
    public class HealthBar
    {
        public void LookAtCamera(Transform healthBarParent)
        {
            healthBarParent.LookAt(Camera.main.transform.position);
        }

        public void ChangeHealthBar(float currentHealth, float maxHealth, Transform healthBarLine)
        {
            var healthPercentage = Mathf.Clamp01(currentHealth / maxHealth);
            var scale = healthBarLine.localScale;
            scale.x = healthPercentage;
            healthBarLine.localScale = scale;
        }
    }
}