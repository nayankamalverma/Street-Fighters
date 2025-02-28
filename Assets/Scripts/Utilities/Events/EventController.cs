using System;

namespace Assets.Scripts.Utilities.Events
{
	
	public class EventController
	{
		private event Action baseEvent;
		public void AddListener(Action listener) => baseEvent += listener;
		public void RemoveListener(Action listener) => baseEvent -= listener;
		public void Invoke() => baseEvent?.Invoke();
	}

	public class EventController<T>
	{
		private event Action<T> baseEvent;
		public void AddListener(Action<T> listener) => baseEvent += listener;
		public void RemoveListener(Action<T> listener) => baseEvent -= listener;
		public void Invoke(T type) => baseEvent?.Invoke(type);
	}

	public class EventController<T1, T2>
	{
		private event Action<T1, T2> baseEvent;
		public void AddListener(Action<T1, T2> listener) => baseEvent += listener;
		public void RemoveListener(Action<T1, T2> listener) => baseEvent -= listener;
		public void Invoke(T1 type1, T2 type2) => baseEvent?.Invoke(type1, type2);
	}
	
}