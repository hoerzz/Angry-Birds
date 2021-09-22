using UnityEngine;

public class BombBird : Bird
{
    public float fielddoImpact;
    public float force;
    public GameObject ExplosionEffect;
    public LayerMask LayerToHit;

    public void Bomb()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position,fielddoImpact,LayerToHit);
        foreach (Collider2D obj in objects)
        {
            Vector2 direction = obj.transform.position - transform.position;
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
            GameObject ExplosioEffectIns = Instantiate(ExplosionEffect,transform.position,Quaternion.identity);
            AudioPlayer.Instance.PlaySFX ("bomb");
            Destroy(ExplosioEffectIns,15);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,fielddoImpact);    
    }

    public override void OnTap()
    {
        Bomb();
    }
    
}
