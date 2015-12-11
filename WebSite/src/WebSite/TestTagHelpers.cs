using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Razor.TagHelpers;

namespace Website.TagHelpers
{
    [HtmlTargetElement("jl-javascript-logging-configuration-code2", Attributes = RequestIdAttributeName)]
    public class TestTagHelpers : TagHelper
    {
        private const string RequestIdAttributeName = "requestid";

        [HtmlAttributeName(RequestIdAttributeName)]
        public string RequestId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            if (output == null)
            {
                throw new ArgumentNullException("output");
            }

            output.Content.SetContent("xxxxxxxxxxxxxxx");
        }
    }
}
