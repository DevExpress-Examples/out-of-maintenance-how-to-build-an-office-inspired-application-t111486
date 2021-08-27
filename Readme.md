<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128641800/14.2.3%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T111486)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to: Build an Office-inspired Application


<p>This example demonstrates how to build an Office-inspired application with DevExpress Scaffolding Wizards.<br />Refer to a corresponding tutorial in DevExpress Documentation to learn step-by-step how to build this application: <a href="https://documentation.devexpress.com/#WPF/CustomDocument17148">Building Office-Inspired Applications</a><br /><br /></p>
<p>You can encounter the following exception:</p>
<p><em>A first chance exception of type 'System.Data.SqlClient.SqlException' occurred in System.Data.dll</em></p>
<p><em>Additional information: A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: SQL Network Interfaces, error: 52 - Unable to locate a Local Database Runtime installation. Verify that SQL Server Express is properly installed and that the Local Database Runtime feature is enabled.)</em></p>
<p>This error is caused because the EntityFramework uses the missing LocalDB component.</p>
<p>To fix the issue, locate the <strong>App.config</strong> file within your application and open it. Change the defaultConnectionFactory in the following manner:<br /><br /></p>


```xaml
    <defaultConnectionFactory type="System.Data.Entity.Infrastructure.SqlConnectionFactory, EntityFramework" /> 
```
