using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HolyPentagram : Interractble
{
    bool isUsing = false;
    public float timeToCharge = 100;
    public float maxTimeToCharge = 100;
    public bool isDone = false;
    [SerializeField]SpriteRenderer renderer;
    public float timeToStartWait = 5;
    public float timeToWait = 1;
    public float amountToRemove = 0.2f;
    [SerializeField] Transform visualParent;
    [SerializeField] ParticleSystem particles;
    ParticleSystem.EmissionModule emision;
    [SerializeField] ParticleSystem particlesInside;
    ParticleSystem.EmissionModule emisionInside;
    Animator anim;
    UnityEngine.UI.Slider pentagramFillSlider;
    public float maxParticlesEmision;
    protected override void Start()
    {
        manager = Manager.instance;
        player = manager.player;
        anim = GetComponent<Animator>();
        anim.SetFloat("Speed", 0);
        emision = particles.emission;
        emisionInside = particlesInside.emission;
        manager.pentagrams.Add(this);
        pentagramFillSlider = manager.pentagramSliderInterface;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!isDone)
        {
            player.OnUse.AddListener(Use);
            manager.ShowUseInterface("to charge holy symbol");
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")&&!isDone)
        {
            player.OnUse.RemoveListener(Use);
            manager.HideUseInterface();
        }
    }
    public override void Use()
    {
        manager.ShowPentagramInterface();
        emision.rateOverTime = 0;
        player.OnUnUse.AddListener(StopUsing);
        player.Stun();
        isUsing = true;
        Using();
    }
    void Using()
    {
        StartCoroutine(UseCour());
    }
    IEnumerator UseCour()
    {
        particles.Play();
        particlesInside.Play();
        while (isUsing)
        {
            timeToCharge -= Time.deltaTime;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, (maxTimeToCharge-timeToCharge) / maxTimeToCharge);
            emision.rateOverTime = (maxTimeToCharge - timeToCharge) / maxTimeToCharge * 20;
            emisionInside.rateOverTime = (maxTimeToCharge - timeToCharge) / maxTimeToCharge * 3;
            anim.SetFloat("Speed", (maxTimeToCharge - timeToCharge) / maxTimeToCharge);
            pentagramFillSlider.value = (maxTimeToCharge - timeToCharge) / maxTimeToCharge;
            if (timeToCharge <= 0)
            {
                manager.HidePentagramInterface();
                FinishPentagram();
                StopUsing();
            }
            yield return null;
        }
    }
    void StopUsing()
    {
        isUsing = false;
        if (!isDone) StartCoroutine(Wait());
        player.OnUnUse.RemoveListener(StopUsing);
        player.Unstan();
    }
    IEnumerator Wait()
    {
        while (!isUsing && timeToCharge < maxTimeToCharge)
        {
            timeToCharge += amountToRemove;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, (maxTimeToCharge - timeToCharge) / maxTimeToCharge);
            emision.rateOverTime = (maxTimeToCharge - timeToCharge) / maxTimeToCharge * 20;
            emisionInside.rateOverTime = (maxTimeToCharge - timeToCharge) / maxTimeToCharge * 3;
            anim.SetFloat("Speed", (maxTimeToCharge - timeToCharge) / maxTimeToCharge);
            yield return new WaitForSeconds(timeToWait);
        }
        emision.rateOverTime = 0;
        emisionInside.rateOverTime = 0;
        particles.Stop();
        particlesInside.Stop();
        anim.SetFloat("Speed", 0);
        if (timeToCharge > maxTimeToCharge)
        {
            timeToCharge = maxTimeToCharge;
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, 0);
        }
    }
    void FinishPentagram()
    {
        player.OnUse.RemoveListener(Use);
        isDone = true;
        manager.CheckWin();
    }
}
