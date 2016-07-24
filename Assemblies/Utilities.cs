using System.Text.RegularExpressions;

namespace Assemblies
{
    public static class Utilities
    {
        public static string RemoveAllWhitespaceFromAString(string stringInput)
        {
            return stringInput.Replace(" ", "");
        }

        public static string CleansePhoneNumber(string phoneNumberToBeCleansed)
        {
            return phoneNumberToBeCleansed.Replace(" ", "")
                .Replace("(", "")
                .Replace(")", "")
                .Replace("-", "")
                .Trim();
        }

        public static bool ValidateIfAusPhoneNumberIsValid(string phoneNumberToBeValidated)
        {
            var phoneExpression = @"^\({0,1}((0|\+61)(2|4|3|7|8)){0,1}\){0,1}(\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{2}(\ |-){0,1}[0-9]{1}(\ |-){0,1}[0-9]{3}$";

            var regex = new Regex(phoneExpression);

            if (regex.IsMatch(phoneNumberToBeValidated))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateIfEmailIsValid(string emailAddressToBeValidated)
        {
            var emailExpression = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            
            var regex = new Regex(emailExpression);

            if (regex.IsMatch(emailAddressToBeValidated))
            {
                return true;
            }
            return false;
        }

        public static string ValidateAndReturnCleansedAusPhoneNumber(string phoneNumber)
        {
            if (ValidateIfAusPhoneNumberIsValid(phoneNumber))
            {
                return CleansePhoneNumber(phoneNumber);
            }

            return string.Empty;
        }

        public static string ValidateAndReturnCleansedEmail(string email)
        {
            if (ValidateIfEmailIsValid(email))
            {
                return email.Trim();
            }

            return string.Empty;
        }
    }
}
