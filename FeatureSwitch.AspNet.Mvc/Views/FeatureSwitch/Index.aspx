<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<FeatureSwitch.AspNet.Mvc.FeatureSwitchViewModel>" %>
<%@ Import Namespace="FeatureSwitch" %>

<!DOCTYPE html>

<html>
    <head>
        <title>title</title>
        <style>
            body {
                font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;
                font-size: 16px;
            }

            table {
                border-collapse: collapse;
                padding: 3px;
            }

            table th {
                font-weight: bold;
                padding: 5px;
            }

            table td, table th { border: 1px solid gray; }

            table tr td:first-child {
                text-align: center;
                vertical-align: middle;
            }

            table td { padding: 5px; }
        </style>
    </head>
    <body>
        <div>
            Features:
            
            <table>
                <tr>
                    <th>Enabled</th>
                    <th>Name</th>
                    <th>Type</th>
                </tr>
                
                <%
                    foreach (var feature in Model.Features)
                    {
                %>
                    <tr>
                        <td>
                            <input type="checkbox" name="<%= feature.GetType().FullName %>" <%= FeatureContext.IsEnabled(feature) ? "checked=\"checked\"" : string.Empty %> <%= !feature.CanModify ?  "disabled=\"disabled\"" : string.Empty %> />
                        </td>
                        <td><%= feature.Name %></td>
                        <td><%= feature.GetType().FullName %></td>
                    </tr>
                <%
                    }
                %>
            </table>
        </div>

        <script src="//ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
        <script type="text/javascript">
            $(function() {
                $('table').on('click', ':checkbox', function() {
                    $.ajax({
                        type: 'POST',
                        url: '/<%= Model.RouteName %>/Update',
                        data: { name: this.name, state: this.checked }
                    });
                });
            });
        </script>
    </body>
</html>