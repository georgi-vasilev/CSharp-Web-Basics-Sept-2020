namespace SUS.MvcFramework
{
    using System;

    using SUS.HTTP;

    public abstract class BaseHttpAttribute : Attribute
    {
        public string Url { get; set; }

        public abstract HttpMethod Method { get; }
    }
}
