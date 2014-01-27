<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<FeatureSwitch.AspNet.Mvc.FeatureSwitchViewModel>" %>
<%@ Import Namespace="FeatureSwitch" %>

<!DOCTYPE html>
<html>
    <head>
        <style>
            body {
                font: 12px Myriad, Helvetica, Tahoma, Arial, clean, sans-serif;
                background: #fbfbfb;
                color: #333;
            }

            table {
                border-collapse: collapse;
                border-spacing: 0;
                padding: 3px;
                background-color: #ffffff;
                border: 1px solid #bebebe !important;
            }

            table th {
                font-weight: bold;
                padding: 5px;
                color: #000000;
                border-color: #aeaeae;
                text-shadow: #ffffff 0 1px 0;
                background: #f1f1f1;
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

        <script src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.10.2.min.js"></script>
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