using System;
using EPiServer.Core;
using EPiServer.Framework.DataAnnotations;
using EPiServer.PlugIn;

namespace EpiSample.Models.Properties
{
    /// <summary>
    ///     Property type for storing a list of strings
    /// </summary>
    /// <remarks>
    ///     For an example, see <see cref="EpiSample.Models.Pages.SitePageData" /> where this property type is used for
    ///     the MetaKeywords property
    /// </remarks>
    [EditorHint(Global.SiteUIHints.Strings)]
    [PropertyDefinitionTypePlugIn(Description = "A property for list of strings", DisplayName = "String List")]
    public class PropertyStringList : PropertyLongString
    {
        protected string Separator = "\n";

        public string[] List => (string[]) Value;

        public override Type PropertyValueType => typeof(string[]);

        public override object Value
        {
            get
            {
                var value = base.Value as string;

                if (value == null)
                    return null;

                return value.Split(Separator.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
            set
            {
                if (value is string[])
                {
                    var s = string.Join(Separator, value as string[]);
                    base.Value = s;
                }
                else
                {
                    base.Value = value;
                }
            }
        }

        public override object SaveData(PropertyDataCollection properties)
        {
            return LongString;
        }
    }
}