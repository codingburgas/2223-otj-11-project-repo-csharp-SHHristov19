using Microsoft.AspNetCore.Html;
using System.Text.Encodings.Web;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Text.RegularExpressions;

namespace Univers.PL.Utilities
{
    public class Utilities
    {
        /// <summary>
        /// Converts the specified <see cref="IHtmlContent"/> object to a string representation, using the specified <see cref="HtmlEncoder"/> to encode the output.
        /// </summary>
        /// <param name="content">The <see cref="IHtmlContent"/> object to convert to a string.</param>
        /// <param name="encoder">The <see cref="HtmlEncoder"/> to use for encoding the output. If null, the default <see cref="HtmlTestEncoder"/> is used.</param>
        /// <returns>A string representation of the specified <see cref="IHtmlContent"/> object.</returns>
        public string HtmlContentToString(IHtmlContent content, HtmlEncoder encoder = null)
        {
            if (encoder == null)
            {
                encoder = new HtmlTestEncoder();
            }

            using (var writer = new StringWriter())
            {
                content.WriteTo(writer, encoder);
                return writer.ToString();
            }
        }

        /// <summary>
        /// Extracts an error message from an HTML error message string, by parsing the message between the first ">" character and the last "<" character, and returning it as a plain string.
        /// </summary>
        /// <param name="errorMessage">The HTML error message string to extract the message from.</param>
        /// <returns>A plain string containing the error message, or null if no message can be extracted.</returns>
        public string GetTheErrorMessage(string errorMessage)
        {
            int startIndex = errorMessage.IndexOf(">") + 13;
            int endIndex = errorMessage.LastIndexOf("<") - 2;
            string message;
            if (startIndex < errorMessage.Length)
            {
                message = new HtmlString(errorMessage.Substring(startIndex, endIndex - startIndex)).ToString();
            }
            else
            {
                message = null;
            }

            return message;
        }
    }
}
