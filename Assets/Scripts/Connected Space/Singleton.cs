using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T instance;

	/**
      Returns the instance of this singleton.
      Note: within the static functions of a singleton class, use the Instance accessor to refer to the instance,
      since this will ensure that an instance is instantiated if there is none.
   */
	public static T Instance
	{
		get
		{
			if(instance == null)
			{
				instance = (T) FindObjectOfType(typeof(T));

				if (instance == null)
				{
					Debug.Log(typeof(T) + 
						" instance is missing.");
				}
			}

			return instance;
		}
	}
}
