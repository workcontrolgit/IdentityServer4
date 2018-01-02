using IdentityServer4.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityServer4.Validation
{
    internal static class AuthorizeValidationLogMessages
    {
        private static readonly Action<ILogger, string, Exception> _start;
        private static readonly Action<ILogger, string, Exception> _success;
        private static readonly Action<ILogger, string, Exception> _callCustomValidator;

        private static readonly Action<ILogger, string, AuthorizeRequestValidationLog, Exception> _missingOrTooLong;

        static AuthorizeValidationLogMessages()
        {
            _start = LoggerMessage.Define<string>(
                eventId: 1,
                logLevel: LogLevel.Debug,
                formatString: "Start validation: {requestType}.");

            _success = LoggerMessage.Define<string>(
                eventId: 2,
                logLevel: LogLevel.Trace,
                formatString: "Success validation: {requestType}.");

            _callCustomValidator = LoggerMessage.Define<string>(
               eventId: 3,
               logLevel: LogLevel.Debug,
               formatString: "Calling custom validator: {customValidatorType}.");

            _missingOrTooLong = LoggerMessage.Define<string, AuthorizeRequestValidationLog>(
               eventId: 4,
               logLevel: LogLevel.Error,
               formatString: "Parameter missing or tool long: {parameterName}.\n{authorizeRequestDetails}");
        }

        public static void Start(this ILogger logger, string requestType)
        {
            _start(logger, requestType, null);
        }

        public static void Success(this ILogger logger, string requestType)
        {
            _success(logger, requestType, null);
        }

        public static void CallCustomValidator(this ILogger logger, string customValidatorType)
        {
            _callCustomValidator(logger, customValidatorType, null);
        }

        public static void MissingOrTooLong(this ILogger logger, string parameterName, ValidatedAuthorizeRequest request)
        {
            var details = new AuthorizeRequestValidationLog(request);
            _missingOrTooLong(logger, parameterName, details, null);
        }
    }
}
