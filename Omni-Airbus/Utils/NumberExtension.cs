namespace Omni_Airbus.Utils
{
    public static class NumberExtension
    {
        /// <summary>
        /// Converts the represented int value to milliseconds
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToMilliseconds(this int value)
        {
            return value * 1000;
        }

        /// <summary>
        /// Converts the represented float value to milliseconds
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToMilliseconds(this float value)
        {
            return Convert.ToInt32(value * 1000f);
        }

        /// <summary>
        /// Converts the represented int value to minutes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToMinutes(this int value)
        {
            return value / 60;
        }
    }
}
