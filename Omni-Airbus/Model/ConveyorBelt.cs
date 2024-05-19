namespace Omni_Airbus.Model
{
	/// <summary>
	/// Represents a conveyer belt in the Omni Airbus model.
	/// </summary>
	public class ConveyerBelt
	{
		/// <summary>
		/// The maximum size of the luggage queue.
		/// </summary>
		public const int MAX_SIZE = 100;

		/// <summary>
		/// The queue of luggage items on the conveyer belt.
		/// </summary>
		private Queue<Luggage> LuggageItems;

		/// <summary>
		/// An object used for thread synchronization.
		/// </summary>
		private readonly object lockObject = new object();

		/// <summary>
		/// The ID of the conveyer belt.
		/// </summary>
		public readonly int ID;

		/// <summary>
		/// Initializes a new instance of the ConveyerBelt class with a specified ID.
		/// </summary>
		/// <param name="iD">The ID of the conveyer belt.</param>
		public ConveyerBelt(int iD)
		{
			LuggageItems = new Queue<Luggage>();
			ID = iD;
		}

		/// <summary>
		/// Adds a luggage item to the conveyer belt.
		/// </summary>
		/// <param name="item">The luggage item to add.</param>
		public void Enqueue(Luggage item)
		{
			lock (lockObject)
			{
				if (LuggageItems.Count < MAX_SIZE)
				{
					LuggageItems.Enqueue(item);
				}
			}
		}

		/// <summary>
		/// Removes and returns the luggage item at the beginning of the conveyer belt.
		/// </summary>
		/// <returns>The luggage item removed from the conveyer belt.</returns>
		public Luggage Dequeue()
		{
			lock (lockObject)
			{
				return LuggageItems.Count > 0 ? LuggageItems.Dequeue() : null;
			}
		}

		/// <summary>
		/// Gets the number of luggage items on the conveyer belt.
		/// </summary>
		public int Count
		{
			get
			{
				lock (lockObject)
				{
					return LuggageItems.Count;
				}
			}
		}
	}
}
