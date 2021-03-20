﻿namespace SUS.HTTP
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class HttpResponse
    {
        public HttpResponse(string ContentType, byte[] body, HttpStatusCode statuscode = HttpStatusCode.Ok)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            this.StatusCode = statuscode;
            this.Body = body;
            this.Headers = new List<Header>
            {
                { new Header("Content-Type", ContentType)},
                { new Header("Content-Length", body.Length.ToString()) },
            };
            this.Cookies = new List<Cookie>();
        }

        public HttpStatusCode StatusCode { get; set; }

        public ICollection<Header> Headers { get; set; }

        public ICollection<Cookie> Cookies { get; set; }

        public byte[] Body { get; set; }

        public override string ToString()
        {
            StringBuilder responseBuilder = new StringBuilder();
            responseBuilder.Append($"HTTP/1.1 {(int)this.StatusCode} {this.StatusCode}" + HttpConstants.NewLine);
            foreach (var header in this.Headers)
            {
                responseBuilder.Append(header.ToString() + HttpConstants.NewLine);
            }

            foreach (var cookie in this.Cookies)
            {
                responseBuilder.Append("Set-Cookie: " + cookie.ToString() + HttpConstants.NewLine);
            }

            responseBuilder.Append(HttpConstants.NewLine);

            return responseBuilder.ToString();
        }
    }
}
