using System;
using System.Runtime.Serialization;

namespace Soloco.Composition.Generation
{
    /// <summary>
    /// Exception thrown when an invalid composition interface type is composed.
    /// </summary>
    [Serializable]
    public class InvalidCompositionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCompositionException"/> class.
        /// </summary>
        public InvalidCompositionException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCompositionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InvalidCompositionException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCompositionException"/> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">
        /// The <paramref name="info"/> parameter is null.
        /// </exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">
        /// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
        /// </exception>
        protected InvalidCompositionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidCompositionException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public InvalidCompositionException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}