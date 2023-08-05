namespace MangasAPI.Validation
{
    /// <summary>
    /// 
    /// </summary>
    public class DomainExceptionValidation : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        public DomainExceptionValidation(string error) : base(error) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hasError"></param>
        /// <param name="error"></param>
        /// <exception cref="DomainExceptionValidation"></exception>
        public static void When(bool hasError, string error)
        {
            if (hasError)
                throw new DomainExceptionValidation(error);
        }
    }
}
