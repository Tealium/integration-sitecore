using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Tealium.Sitecore.TagManagement.Data
{
    /// <summary>
    /// Encodes utag_data keys and values as JavaScript literals that are safe to
    /// write inside a &lt;script&gt; block.
    /// </summary>
    /// <remarks>
    /// Besides quotes and backslashes, the characters &lt; &gt; &amp; and ' are written
    /// as \u escapes, so a value can never close the script block ("&lt;/script&gt;")
    /// or start an HTML comment. Custom <see cref="IUtagDataProvider"/>
    /// implementations should use this to build their values.
    /// </remarks>
    public static class UtagValueEncoder
    {
        // line terminators in JavaScript, although not in JSON
        private const char LineSeparator = (char)0x2028;
        private const char ParagraphSeparator = (char)0x2029;

        /// <summary>
        /// Encodes a value: enumerables (other than strings) become an array of
        /// strings, anything else a string. Null becomes an empty string.
        /// </summary>
        public static string Encode(object value)
        {
            var enumerable = value as IEnumerable;
            if (enumerable != null && !(value is string) && !(value is IEnumerable<char>))
            {
                return "[" + string.Join(",", enumerable.Cast<object>().Select(x => EncodeString(x == null ? string.Empty : x.ToString()))) + "]";
            }

            return EncodeString(value == null ? string.Empty : value.ToString());
        }

        /// <summary>
        /// Encodes a string as a double-quoted JavaScript string literal.
        /// </summary>
        public static string EncodeString(string value)
        {
            var sb = new StringBuilder((value ?? string.Empty).Length + 2);
            sb.Append('"');

            foreach (var c in value ?? string.Empty)
            {
                switch (c)
                {
                    case '"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '<':
                    case '>':
                    case '&':
                    case '\'':
                    case LineSeparator:
                    case ParagraphSeparator:
                        AppendUnicodeEscape(sb, c);
                        break;
                    default:
                        if (c < ' ')
                        {
                            AppendUnicodeEscape(sb, c);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }

            sb.Append('"');
            return sb.ToString();
        }

        /// <summary>
        /// Makes a value returned by a custom <see cref="IUtagDataProvider"/> safe to
        /// place in a script block. Such values are already JavaScript literals, so
        /// they cannot be re-encoded; this only stops them from closing the block.
        /// </summary>
        public static string ProtectLiteral(string literal)
        {
            if (string.IsNullOrEmpty(literal))
            {
                return "\"\"";
            }

            // "<\/" is the same as "</" inside a JavaScript string
            return literal.Replace("</", "<\\/").Replace("<!--", "<\\!--");
        }

        private static void AppendUnicodeEscape(StringBuilder sb, char c)
        {
            sb.Append("\\u");
            sb.Append(((int)c).ToString("x4", CultureInfo.InvariantCulture));
        }
    }
}
