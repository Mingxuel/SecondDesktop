# SECOND DESKTOP

## 1 User
### 1.1 Introduction
    Second Desktop is a toolbox, it will make your work more comfortable.  
    The Windows Start Menu is a collection of application shortcuts, it is quick and also stupid.  
    Second Desktop will meet your needs in the easiest way.  
<img src="https://raw.githubusercontent.com/Mingxuel/SecondDesktop/master/BlogResource/SecondDesktop.png"/>  
    
### 1.2 Features
  
  
### 1.3 Configuring
    1> Windows XP/7/vista/8/10.  
    2> .netframework 4.6 or above.  
    
### 1.4 HOW TO USE
#### 1.4.1 Start
    1> Download release version(Please download the new version).  
    2> Double click SecondDesktop.exe.  
#### 1.4.2 Install App  
    1> Click AppStore on the App Area.  
<img src="https://raw.githubusercontent.com/Mingxuel/SecondDesktop/master/BlogResource/AppStore.png"/>
    
    2> Click "Install" button to install app you want.  
<img src="https://raw.githubusercontent.com/Mingxuel/SecondDesktop/master/BlogResource/AppStoreMainWindow.png"/>
    
#### 1.4.3 Install SubApp  
    1> Select Desktop page that you want to install.  
    2> Click App you want to install.  
    3> Settings then install SubApp, you can see SubApp in the selected Desktop.  
----
## 2 Delevoper
### 2.1 Definitions
    SD -- Second Desktop  
    SDA -- Second Desktop App  
### 2.2 How To Create Your First SDA?  
    1> Open SecondDesktop.sln.  
    2> Set 'AppCreator' Project as Startup Project->Run.  
<img src="https://raw.githubusercontent.com/Mingxuel/SecondDesktop/master/BlogResource/Creator.png"/>    
    3> Input your SDA name , then click 'CREATE' button.  
        If success you can see your project in the path ./SecondDesktop/SecondDesktopApp/.  
<img src="https://raw.githubusercontent.com/Mingxuel/SecondDesktop/master/BlogResource/SetAsStartupProject.png"/>
    3> 'Apps' folder->Add->Existing Project-> Select your *.csproj.  
<img src="https://raw.githubusercontent.com/Mingxuel/SecondDesktop/master/BlogResource/AddSubAppProject.png"/>
### 2.3 Create SubApp UserControl  
    1> Create UserControl.  
    2> Open AppUID.cs, then insert your UserControl name into enum AppUID.  
### 2.4 Create Config File  
    ```csharp
        Factory.CreateSubApp(AppUID.SubApp, config);
    ```
### 2.5 Insert SubApp into Desktop
    ```csharp
        string config = Factory.CreateSubAppConfig();
        Factory.CreateSubApp(AppUID.SubApp, config);
    ```
