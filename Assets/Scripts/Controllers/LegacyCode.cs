//* First Person Jump

/*

//TODO
    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.1f;
    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;
    // player
    private float _speed;
    private float _verticalVelocity = 0.0f;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;
    private bool m_hasJumped = false;

    private void CheckIfGrounded()
    {
        // groundedPlayer = m_controller.isGrounded;
        // if (groundedPlayer && playerVelocity.y < 0)
        // {
        //     playerVelocity.y = 0f;
        // }


        //TODO
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + GroundedOffset, transform.position.z);

        Debug.DrawLine(transform.position, spherePosition, Color.yellow);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }
 public void CheckIfJumped()
    {
        //if (m_hasJumped) return;
        //if (!Grounded) return;
        if (InputManager.Instance)
        {
            m_hasJumped = InputManager.Instance.Jump == 1;
            if (!m_hasJumped) _verticalVelocity = Mathf.Sqrt(JumpHeight * -50f * Gravity);

            if (m_hasJumped)
            {
                //_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                Vector3 jumpForce = new Vector3(0, _verticalVelocity, 0);
                m_controller.Move(Time.deltaTime * jumpForce);

                _verticalVelocity -= Gravity * Time.deltaTime;
            }
            // the square root of H * -2 * G = how much velocity needed to reach desired height


            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            // if (_verticalVelocity < _terminalVelocity)
            // {
            //     _verticalVelocity += Gravity * Time.deltaTime;
            // }

            Debug.Log(m_hasJumped);
        }
    }
    private void JumpAndGravity()
    {
        if (Grounded)
        {
            // reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            // stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Ensure the landing sound is played once when grounded again
            // if (!_landSoundPlayed)
            // {
            //     _playerSound.PlayLandSound();
            //     _landSoundPlayed = true;
            // }

            // Jump
            if ( m_hasJumped && _jumpTimeoutDelta <= 0.0f)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                _jumpTimeoutDelta = JumpTimeout;
                //_playerSound.PlayJumpSound();
                // _landSoundPlayed = false; // Remove this line
            }

            // jump timeout
            if (_jumpTimeoutDelta >= 0.0f)
            {
                _jumpTimeoutDelta -= Time.deltaTime;
            }
        }
        else
        {
            // reset the jump timeout timer
            _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }

            // if we are not grounded, do not jump
            //_input.SetJump(false);
            //m_hasJumped = false;

            // _landSoundPlayed = false; // Remove this line
        }

        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }

        // Reset _landSoundPlayed flag when falling below 0 velocity
        if (!Grounded && _verticalVelocity < 0.0f && _fallTimeoutDelta < 0)
        {
            //_landSoundPlayed = false;
        }
    }

*/

//[SerializeField] private float m_rayReach = 30.0f; //TODO set this in PlayerState
//private float m_displacement = 0.0f;
//private Vector3 m_beamMagnitud;
//private Ray m_ray;
//TODO --------- Place in an independent RayBeam GO
// [Header("Testing --------------")]
// [SerializeField] private BeamAudioFX m_beamAudioFX;

// [SerializeField] private float m_displacementSpeed = 20.0f;
// private Vector3[] m_beamPoints;
// private Vector3 m_hitPoint;
// //Add RayBeamPrefab
// [SerializeField] private LineRenderer m_lineRenderer;
// [SerializeField] private BeamImpactParticle m_beamImpactParticle;
//TODO ----------


// m_lineRenderer.positionCount = 2; //TODO move to independent RayBeam GO
// m_beamPoints = new Vector3[m_lineRenderer.positionCount]; //TODO move to independent RayBeam GO
//TODO move to independent RayBeam GO

//m_displacementSpeed = 20.0f;//so_playerState.CoolDown; 

/*TODO move to independent RayBeam GO
m_skrinkColorKeys = new GradientColorKey[] { new GradientColorKey(Color.yellow, 0.0f), new GradientColorKey(Color.yellow, 1.0f) };
m_stretchColorKeys = new GradientColorKey[] { new GradientColorKey(Color.blue, 0.0f), new GradientColorKey(Color.blue, 1.0f) };
m_alphaColorKeys = new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) };
*/



/*
        //TODO place all of this inside independe RayBeam GO
        m_hitPoint = hitPoint;
        EmitImpactParticles(hitPoint, sign);
        if (sign < 0)
        {
            m_lineRenderer.startColor = Color.yellow;
            m_lineRenderer.endColor = Color.red;
            m_beamAudioFX.PlayShrink();
        }
        else
        {
            m_lineRenderer.startColor = Color.blue;
            m_lineRenderer.endColor = Color.green;
            m_beamAudioFX.PlayStretch();
        }
        //TODO ---------------------
        //? For testing, this ray belongs to anything the user click on the screen. Not the center
        m_ray = ray;//Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawLine(m_beamOriginAtGun.position, m_ray.direction + m_beamOriginAtGun.position);
        */



// if (!m_hasShoot) return;
// BeamTailDisplacementAcrossRay();

/*TODO WIP
[SerializeField] GameObject m_beamHeadIndicator;
public void BeamTailDisplacementAcrossRay()
{
    m_beamMagnitud = m_ray.direction.normalized * m_rayReach;
    m_beamPoints[0] = m_beamOriginAtGun.position + m_ray.direction.normalized * m_displacement;
    m_beamPoints[1] = m_hitPoint;//m_beamOriginAtGun.position + m_beamMagnitud;
    m_lineRenderer.SetPositions(m_beamPoints);
    m_displacement += m_displacementSpeed * Time.fixedDeltaTime;

    m_beamHeadIndicator.transform.position = m_beamPoints[1];
    //Debug.Log(displacement);
    if (m_displacement < m_rayReach) return;
    m_beamPoints[0] = m_beamOriginAtGun.position;
    m_beamPoints[1] = m_beamOriginAtGun.position;
    m_lineRenderer.SetPositions(m_beamPoints);
    m_hasShoot = false;
    m_displacement = 0;
}
*/
/*TODO --------- place inside the RayBeam independent GM
[SerializeField] private GameObject m_impactWavesParticlesPrefab;
private GradientColorKey[] m_skrinkColorKeys;
private GradientColorKey[] m_stretchColorKeys;
private GradientAlphaKey[] m_alphaColorKeys;
public void EmitImpactParticles(Vector3 hitPoint, float sign)
{
    if (!m_impactWavesParticlesPrefab) return;

    GameObject impactParticles = Instantiate(m_impactWavesParticlesPrefab, hitPoint, m_impactWavesParticlesPrefab.transform.rotation);

    //* Defining color

    var impactWavesCol = impactParticles.GetComponent<ParticleSystem>().colorOverLifetime;

    impactWavesCol.enabled = true;

    Gradient grad = new Gradient();
    grad.SetKeys(sign > 0 ? m_stretchColorKeys : m_skrinkColorKeys, m_alphaColorKeys);

    impactWavesCol.color = grad;

    Debug.Log(impactParticles);
    Destroy(impactParticles, impactParticles.GetComponent<ParticleSystem>().main.startLifetimeMultiplier);
}

public void BeamHeadDisplacementAcrossRay()
{
    //NOTE: using a particle material yields a very interesting look, as if it were a plasma blast
    m_beamMagnitud = m_ray.direction.normalized * 5f;
    m_beamPoints[0] = m_beamOriginAtGun.position;// + m_ray.direction.normalized;// * displacement;
    m_beamPoints[1] = m_beamOriginAtGun.position + m_beamMagnitud * m_displacement;//m_ray.GetPoint(displacement);//.direction * 20.0f;
    m_lineRenderer.SetPositions(m_beamPoints);
    m_displacement += m_displacementSpeed * Time.deltaTime;

    m_beamHeadIndicator.transform.position = m_beamPoints[1];
    //Debug.Log(displacement);
    if (m_displacement < m_rayReach) return;
    m_beamPoints[0] = m_beamOriginAtGun.position;
    m_beamPoints[1] = m_beamOriginAtGun.position;
    m_lineRenderer.SetPositions(m_beamPoints);
    m_hasShoot = false;
    m_displacement = 0;
}
*/