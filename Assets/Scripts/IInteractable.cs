namespace CameronBonde
{
	public interface IInteractable
	{
		public void Interact();
	}
	
	public interface IInteractableWithState
	{
		public void Interact(bool state);
	}

}