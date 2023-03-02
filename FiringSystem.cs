using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FiringSystem : MonoBehaviour
{
    public Mode FireMode;

    [SerializeField]
    private float fireSpeed;
    [SerializeField]
    private GameObject _muzzleFlashPoint;
    [SerializeField]
    private ParticleSystem _muzzleFlash;
    [SerializeField]
    private bool auto;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Toggle _fireTypebtn;

    public enum Mode
    {
        Single,
        Auto
    }
    public void CheckMode()
    {
        if (FireMode == Mode.Single)
        {
            auto = false;
            _fireTypebtn.interactable = false;
            _fireTypebtn.isOn = false;
        }
        else if (FireMode == Mode.Auto)
        {
            _fireTypebtn.interactable = true;
        }
    }

    void Start()
    {
        //_animator = GetComponent<Animator>();
        //_muzzleFlash.gameObject.transform.position = _muzzleFlashPoint.transform.position;

        //CheckMode();
    }

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _muzzleFlash.gameObject.transform.position = _muzzleFlashPoint.transform.position;

        CheckMode();
    }

    void Update()
    {

    }

    void checkToggle()
    {
        if (_fireTypebtn.isOn)
            auto = true;
        else
            auto = false;
    }

    public void FireDown()
    {
        checkToggle();
        if (auto)
        {
            InvokeRepeating("Fire", 0, fireSpeed);
            _animator.SetTrigger("Loopshoot");
        }
        else
        {
            Fire();
            _animator.SetTrigger("shoot");
        }
    }

    void Fire()
    {
        GetComponent<AudioSource>().Play();
        _muzzleFlash.gameObject.SetActive(true);
        _muzzleFlash.Play();
    }

    public void FireUp()
    {
        _animator.SetTrigger("idle");
        _muzzleFlash.Stop();
        _muzzleFlash.gameObject.SetActive(false);
        CancelInvoke();
    }


}
