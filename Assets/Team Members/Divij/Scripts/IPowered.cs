using Divij;using UnityEngine;


namespace Divij
{
    public interface IPowered
    {
        void SetPowered(bool powered);
        bool GetPowered();

        /// <summary>
        ///	Optional
        /// Energy provider will call this every now and then. Return how much energy you actually used.
        /// </summary>
        /// <param name="currentEnergy">total energy you CAN provide</param>
        /// <returns>used energy</returns>
        int UseEnergy(int currentEnergy)
        {
	        return 0;
        }
    }
}
