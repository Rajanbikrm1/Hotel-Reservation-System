using System;
using System.Collections.Generic;
using System.Text;

namespace InitialTestApp
{
    class ErrorHandler
    {
        public enum ErrorType
        {
            INVALID_CREDIT_CARD_NUMBER,
            INVALID_INPUT,
        }

        public static void HandleError(ErrorType errorType)
        {
            String errorMessage = "";

            switch(errorType)
            {
                case ErrorType.INVALID_CREDIT_CARD_NUMBER:
                    errorMessage = "Invalid credit card number";
                    break;
                case ErrorType.INVALID_INPUT:
                    errorMessage = "Invalid input value";
                    break;
            }

            Console.WriteLine("Error - " + errorMessage);
        }
    }
}
