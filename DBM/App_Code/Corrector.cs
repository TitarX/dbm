using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

/// <summary>
/// Summary description for Corrector
/// </summary>
public class Corrector
{
    public Corrector() { }

    public static String EscapeBackslash(String text)
    {
        char[] chars = new char[text.Length];
        chars = text.ToCharArray();

        StringBuilder resultBuilder = new StringBuilder();
        foreach (char c in chars)
        {
            if (c.Equals('\\'))
            {
                resultBuilder.Append(c);
                resultBuilder.Append(c);
            }
            else
            {
                resultBuilder.Append(c);
            }
        }

        return resultBuilder.ToString();
    }

    public static String EscapeApos(String text)
    {
        char[] chars = new char[text.Length];
        chars = text.ToCharArray();

        StringBuilder resultBuilder = new StringBuilder();
        foreach (char c in chars)
        {
            if (c.Equals('\''))
            {
                resultBuilder.Append("\\");
                resultBuilder.Append(c);
            }
            else
            {
                resultBuilder.Append(c);
            }
        }

        return resultBuilder.ToString();
    }

    public static String EscapeQuot(String text)
    {
        char[] chars = new char[text.Length];
        chars = text.ToCharArray();

        StringBuilder resultBuilder = new StringBuilder();
        foreach (char c in chars)
        {
            if (c.Equals('"'))
            {
                resultBuilder.Append("\\");
                resultBuilder.Append(c);
            }
            else
            {
                resultBuilder.Append(c);
            }
        }

        return resultBuilder.ToString();
    }

    public static String HtmlEncoding(String text)
    {
        char[] chars = new char[text.Length];
        chars = text.ToCharArray();

        StringBuilder resultBuilder = new StringBuilder();
        foreach (char c in chars)
        {
            switch (c)
            {
                case '<':
                    resultBuilder.Append("&lt;");
                    break;
                case '>':
                    resultBuilder.Append("&gt;");
                    break;
                case '\'':
                    resultBuilder.Append("&apos;");
                    break;
                case '"':
                    resultBuilder.Append("&quot;");
                    break;
                case '&':
                    resultBuilder.Append("&amp;");
                    break;
                default:
                    resultBuilder.Append(c);
                    break;
            }
        }

        return resultBuilder.ToString();
    }

    public static String ArgumentHtmlEncoding(String text)
    {
        char[] chars = new char[text.Length];
        chars = text.ToCharArray();

        StringBuilder resultBuilder = new StringBuilder();
        foreach (char c in chars)
        {
            switch (c)
            {
                case '<':
                    resultBuilder.Append("&#60;");
                    break;
                case '>':
                    resultBuilder.Append("&#62;");
                    break;
                case '\'':
                    resultBuilder.Append("\\&#39;");
                    break;
                case '"':
                    resultBuilder.Append("\\&#34;");
                    break;
                case '&':
                    resultBuilder.Append("&#38;");
                    break;
                default:
                    resultBuilder.Append(c);
                    break;
            }
        }

        return resultBuilder.ToString();
    }
}