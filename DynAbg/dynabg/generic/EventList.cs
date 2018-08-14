using System;
using System.Collections.Generic;

namespace DynAbg.Generic
{
	[Serializable]
	internal class EventList<T> : System.Collections.Generic.List<T>
	{
		#region Events

		public event EventHandler ItemsChanged;

		#endregion Events

		/// <summary>
		/// Fügt am Ende der <c>List</c> ein Objekt des Typs T hinzu.
		/// </summary>
		/// <param name="item">
		/// Das Objekt, das am Ende der List hinzugefügt werden soll.
		/// Der Wert kann für Referenztype <c>null</c> sein.
		/// </param>
		public new void Add(T item)
		{
			base.Add(item);

			EventHandler eventHandler = ItemsChanged;

			if (eventHandler != null)
				eventHandler(this, new AddedEventArgs(item));
		}

		/// <summary>
		/// Fügt die Elemente der angegebenen Auflistung am Ende der List
		/// hinzu.
		/// </summary>
		/// <param name="collection">
		/// Die Auflistung, deren Elemente am Ende von List hinzugefügt 
		/// werden sollen. Die Auflistung an sich kann nicht null sein, sie 
		/// kann jedoch Elemente enthalten, die null sind, wenn Typ T einen 
		/// Referenztyp darstellt.
		/// </param>
		public new void AddRange(IEnumerable<T> collection)
		{
			base.AddRange(collection);

			EventHandler eventHandler = ItemsChanged;

			if (eventHandler != null)
				eventHandler(this, new RangeAddedEventArgs(collection));
		}

		/// <summary>
		/// Entfernt alle Elemente aus der List.
		/// </summary>
		public new void Clear()
		{
			EventList<T> backup = new EventList<T>();
			backup.AddRange(this);

			base.Clear();

			EventHandler eventHandler = ItemsChanged;

			if (eventHandler != null)
				eventHandler(this, new ClearedEventArgs(backup));
		}

		/// <summary>
		/// Entfernt das erste Vorkommen eines angegebenen Objekts aus List.
		/// </summary>
		/// <param name="item">
		/// Das aus dem List zu entfernende Objekt. Der Wert kann für 
		/// Referenztypen null sein.
		/// </param>
		public new bool Remove(T item)
		{
			bool result = base.Remove(item);

			if (result)
			{
				EventHandler eventHandler = ItemsChanged;

				if (eventHandler != null)
					eventHandler(this, new RemovedEventArgs(item));
			}

			return result;
		}

		#region InnerClasses

		internal enum ChangeType
		{
			ADDED, RANGE_ADDED, REMOVED, CLEARED
		}

		internal class ChangedEventArgs : EventArgs
		{
			public ChangeType ChangeType {get; private set;}

			public ChangedEventArgs(ChangeType changeType)
			{
				ChangeType = changeType;
			}
		}

		internal class AddedEventArgs : ChangedEventArgs
		{
			public T Item { get; private set; }

			public AddedEventArgs(T item)
				: base(ChangeType.ADDED)
			{
				this.Item = item;
			}
		}

		internal class RangeAddedEventArgs : ChangedEventArgs
		{
			public IEnumerable<T> Items { get; private set; }

			public RangeAddedEventArgs(IEnumerable<T> items)
				: base(ChangeType.RANGE_ADDED)
			{
				this.Items = items;
			}
		}

		internal class RemovedEventArgs : ChangedEventArgs
		{
			public T Item { get; private set; }

			public RemovedEventArgs(T item)
				: base(ChangeType.REMOVED)
			{
				this.Item = item;
			}
		}

		internal class ClearedEventArgs : ChangedEventArgs
		{
			public IEnumerable<T> Items { get; private set; }

			public ClearedEventArgs(IEnumerable<T> items)
				: base(ChangeType.CLEARED)
			{
				this.Items = items;
			}
		}

		#endregion InnerClasses
	}

}
