using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spacecraft : MonoBehaviour
{
    [SerializeField] protected float rcsThrust = 100f;
    [SerializeField] protected float mainThrust = 100f;
    [SerializeField] protected float levelLoadDelay = 2f;

    [SerializeField] protected AudioClip mainEngine;
    [SerializeField] protected AudioClip success;
    [SerializeField] protected AudioClip death;

    [SerializeField] protected ParticleSystem mainEngineParticles;
    [SerializeField] protected ParticleSystem successParticles;
    [SerializeField] protected ParticleSystem deathParticles;

    protected Rigidbody rigidBody;
    protected AudioSource audioSource;

    protected bool isTransitioning = false;
    protected bool collisionsDisabled = false;
    protected bool isSandboxMode = false;

    protected virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
        if (!isTransitioning)
        {
            RespondToThrustInput();
            RespondToRotateInput();
            if (Input.GetKeyDown(KeyCode.P)) // Check for sandbox mode toggle
            {
                ToggleSandboxMode();
            }
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionsDisabled || isSandboxMode) { return; }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    protected virtual void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    protected virtual void StartDeathSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
    }

    protected virtual void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // loop back to start
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    protected virtual void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }

    protected virtual void RespondToThrustInput()
    {
        // Base class does nothing, intended to be overridden by subclasses
    }

    protected virtual void RespondToRotateInput()
    {
        // Base class does nothing, intended to be overridden by subclasses
    }

    protected virtual void RotateManually(float rotationThisFrame)
    {
        rigidBody.freezeRotation = true; // take manual control of rotation
        transform.Rotate(Vector3.forward * rotationThisFrame);
        rigidBody.freezeRotation = false; // resume physics control of rotation
    }

    protected virtual void ToggleSandboxMode()
    {
        isSandboxMode = !isSandboxMode;
        Debug.Log("Sandbox Mode: " + isSandboxMode);
    }
}

public class Rocket : Spacecraft
{

    protected override void RespondToThrustInput()
    {
        if (isSandboxMode || Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            StopApplyingThrust();
        }
    }

    protected override void RespondToRotateInput()
    {
        if (isSandboxMode || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                RotateManually(rcsThrust * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                RotateManually(-rcsThrust * Time.deltaTime);
            }
        }
    }

    protected virtual void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    protected virtual void StopApplyingThrust()
    {
        audioSource.Stop();
        mainEngineParticles.Stop();
    }
}


