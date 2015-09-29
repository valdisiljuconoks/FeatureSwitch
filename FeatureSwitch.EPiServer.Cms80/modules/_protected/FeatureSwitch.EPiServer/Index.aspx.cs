using System;
using System.Web.UI.WebControls;
using EPiServer.Shell.WebForms;

namespace FeatureSwitch.EPiServer.modules._protected.FeatureSwitch.EPiServer
{
    public partial class Index : WebFormsBase
    {
        public Index()
        {
            PreRender += OnPreRender;
        }

        private void OnPreRender(object sender, EventArgs eventArgs)
        {
            var features = FeatureContext.GetFeatures();
            rptFeatures.DataSource = features;

            DataBind();
        }

        protected void OnFeaturesRepeaterItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var stateControl = e.Item.FindControl("chkEnabled") as CheckBox;
            var feature = e.Item.DataItem as BaseFeature;

            if (stateControl != null && feature != null)
            {
                stateControl.Enabled = feature.CanModify;
                stateControl.Checked = FeatureContext.IsEnabled(feature.GetType());
                stateControl.InputAttributes["value"] = feature.GetType().FullName;
            }
        }

        protected void chkEnabled_OnCheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var featureName = checkBox.InputAttributes["value"];
            if (checkBox.Checked)
            {
                FeatureContext.Enable(featureName);
            }
            else
            {
                FeatureContext.Disable(featureName);
            }
        }
    }
}
