CustomCodedUI
=============

Summary:
GUI Automation Solution that contains data driven ui testing framework based on MS Coded UI and test data management website.

Dev. Environment:
Visual Studio 2012,SQL Server 2008 R2.

Features:
1.Run as console app without Visual Studio.
2.Custom Attributes to control test running ,like [Scenario],[ReusableTest],[RollbackScenario].
3.Separate reusable components defined as static extension methods from test script.
4.Build MVC Website to easily query,add,delete,update test data from DB.
5.Support both web and windows(even WPF) apps.

Code Structure:
Solution:AxisUIA - UIAuto Framework
Project:
1)TestAgent - Console Application as single thread to launch mstest.exe to custom run coded ui test.
2)Core - Class Library as common module that defined TestModel,Reporter ..etc which are suitable for all applications under this framework.
3)ANH - Class Library as a web application under test,each application need its own project.
Solution:UIADM - UI Automation Data Management Website
