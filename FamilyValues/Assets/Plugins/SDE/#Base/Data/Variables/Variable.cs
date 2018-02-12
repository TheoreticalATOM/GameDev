using UnityEngine;
using UnityEngine.Assertions;

namespace SDE.Data
{
    public abstract class Variable<T> : ScriptableObject
    {
		public System.Action<T> OnValueChangedEvent;
        public T Value;

		public void SetValueEventTrigger(T value)
		{
			Assert.IsNotNull(OnValueChangedEvent, "OnValueChangedEvent is missing an event");
			
			if(!value.Equals(Value))
				OnValueChangedEvent.Invoke(value);
			Value = value;
		}
    }
}