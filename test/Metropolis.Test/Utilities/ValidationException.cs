using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Metropolis.Api.Extensions;

namespace Metropolis.Test.Utilities
{
    public enum ValidationSeverity
    {
        Error,
        Warning
    }

    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        {
        }

        public ValidationException(Exception message)
            : base(message.Message)
        {
        }

        public ValidationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        internal ValidationSeverity Severity { get; set; }

        public bool IsError => Equals(ValidationSeverity.Error, Severity);

        public bool IsWarning => Equals(ValidationSeverity.Warning, Severity);

        public override string Message => InnerException?.Message ?? base.Message;

        public ValidationException Warning()
        {
            Severity = ValidationSeverity.Warning;
            return this;
        }

        public static ValidationException IsRequired(string message)
        {
            return new ValidationException($"{message}");
        }

        public static ValidationException FormattedError(string format, params string[] args)
        {
            return new ValidationException(string.Format(format, args));
        }
    }

    public sealed class MultiException : ValidationException
    {
        private readonly ValidationException[] innerExceptions;

        public MultiException(string message)
            : base(message)
        {
        }

        public MultiException(string message, ValidationException innerException)
            : base(message, innerException)
        {
            innerExceptions = new[] {innerException};
        }

        public MultiException(IEnumerable<ValidationException> innerExceptions)
            : this(null, innerExceptions)
        {
        }

        public MultiException(string message, IEnumerable<ValidationException> innerExceptions)
            : base(message, innerExceptions.FirstOrDefault())
        {
            if (innerExceptions.Any(ex => ex == null))
            {
                throw new ArgumentNullException();
            }

            this.innerExceptions = innerExceptions.ToArray();
        }

        private MultiException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public IEnumerable<Exception> InnerExceptions => innerExceptions;

        public override string Message
        {
            get
            {
                var builder = new StringBuilder();
                innerExceptions.ForEach(each => builder.AppendLine(each.Message));
                return builder.ToString().TrimEnd('\n', '\r');
            }
        }
    }
}