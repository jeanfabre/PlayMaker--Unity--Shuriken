using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

public class playMakerShurikenProxy : MonoBehaviour {
	

#if UNITY_5_0
	private ParticleCollisionEvent[] collisionEvents = new ParticleCollisionEvent[16];
#else
	private ParticleSystem.CollisionEvent[] collisionEvents = new ParticleSystem.CollisionEvent[16];
#endif
	
	private PlayMakerFSM _fsm;
	
	void Start()
	{
		_fsm = this.GetComponent<PlayMakerFSM>();
		
		if (_fsm==null)
		{
			Debug.LogError("No Fsm found",this);
		}

	}

	#if UNITY_5_0
	public ParticleCollisionEvent[] GetCollisionEvents()
	{
		return collisionEvents;
	}
	#else
	public ParticleSystem.CollisionEvent[] GetCollisionEvents()
	{
		return collisionEvents;
	}
	#endif
	
    void OnParticleCollision(GameObject other) {
		
        ParticleSystem particleSystem;
		
        particleSystem = other.GetComponent<ParticleSystem>();

		#if UNITY_5_0
        int safeLength = particleSystem.GetSafeCollisionEventSize();
		#else
		int safeLength = particleSystem.safeCollisionEventSize;
		#endif
       // if (collisionEvents.Length < safeLength)
           
		#if UNITY_5_0
		collisionEvents = new ParticleCollisionEvent[safeLength];
		#else
		collisionEvents = new ParticleSystem.CollisionEvent[safeLength];
		#endif
		int numCollisionEvents = particleSystem.GetCollisionEvents(gameObject, collisionEvents);
		
	
		FsmEventData _data = new FsmEventData();
		_data.GameObjectData = other;
		_data.IntData = numCollisionEvents;
		PlayMakerUtils.SendEventToGameObject(_fsm,this.gameObject,"ON PARTICLE COLLISION");
		

	}
}
