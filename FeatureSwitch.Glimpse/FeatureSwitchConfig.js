(function ($, pubsub, util, data) {

    var generateAddress = function (name, val) {
        var currentMetadata = data.currentMetadata();
        return util.uriTemplate(currentMetadata.resources.featureswitch_glimpse_featureswitchresource, { 'featurename': name, 'val': val });
    },

        setup = function (args) {
            args.newData.data.featureswitch = { name: 'FeatureSwitch Config', data: '', isPermanent: true };
            args.newData.metadata.plugins.featureswitch = {};
        },

        build = function (args) {

            $.ajax({
                url: generateAddress(),
                type: 'GET',
                contentType: 'application/json',
                success: function (result) {

                    layout(args.panel, result);

                    args.panel.find('.featureswitch-toggle').change(function (value) {
                        var newValue = value.target.checked;
                        $.ajax({
                            url: generateAddress($(value.target).data('fname'), newValue),
                            type: 'GET',
                            contentType: 'application/json',
                            success: function (result) {
                                layout(args.panel, result);
                            }
                        });
                    });
                }
            });
        },

        layout = function (panel, result) {
            var header = '<table><thead><tr class="glimpse-row-header glimpse-row-header-0"><th>Enabled?</th><th>Name</th></tr></thead><tbody class="glimpse-row-holder">',
                        body = '',
                        footer = '</tbody></table>';

            if (result.length) {
                for (var i = 0; i < result.length; i++) {
                    body += '<tr class="glimpse-row"><td><input type="checkbox" ' + (result[i].enabled ? 'checked="true"' : '') + ' data-fname="' + result[i].fullName + '" class="featureswitch-toggle" ' + (!result[i].canModify ? 'disabled="disabled"' : '') + '/></td><td>' + result[i].name + '</td></tr>';
                }
            }

            panel.html(header + body + footer);
        };

    pubsub.subscribe('action.panel.rendered.featureswitch', build);
    pubsub.subscribe('action.data.initial.changed', setup);

})(jQueryGlimpse, glimpse.pubsub, glimpse.util, glimpse.data);