# SECOND DESKTOP

## 1 User
### 1.1 Introduction
    Second Desktop is a toolbox, it will make your work more comfortable.  
    The Windows Start Menu is a collection of application shortcuts, it is quick and also stupid.  
    Second Desktop will meet your needs in the easiest way.  
<img src="https://raw.githubusercontent.com/Mingxuel/SecondDesktop/master/BlogResource/SecondDesktop.png"/>  
    
### 1.2 Features
  
  
### 1.3 Configuring
    1. Windows XP/7/vista/8/10.  
    2. .netframework 4.6 or above.  
    
### 1.4 HOW TO USE
#### 1.4.1 Start
    1) Download release version(Please download the new version).  
    2) Double click SecondDesktop.exe.  
#### 1.4.2 Install App  
    1) Click AppStore on the App Area.  
<img src="https://raw.githubusercontent.com/Mingxuel/SecondDesktop/master/BlogResource/AppStore.png"/>
    2) Click "Install" button to install app you want.  
<img src="https://raw.githubusercontent.com/Mingxuel/SecondDesktop/master/BlogResource/AppStoreMainWindow.png"/>
#### 1.4.3 Install SubApp  
    1) Select Desktop page that you want to install.
    2) Click App you want to install.  
    3) Settings then install SubApp, you can see SubApp in the selected Desktop.  

----

## 2 Delevoper
### 2.1 Definitions
    SD -- Second Desktop  
    SDA -- Second Desktop App  

### 2.2 How to create your first SDA?
    1) Download code then extract files.  
    2) Double click SecondDesktop.sln.  
    3) File->New->Project->Class Library(.Net Framework), don`t forget rename your project.  
    4) Ready your project image, size is 36x36, type is png, name is your App name, then add the image to the project root directory.  
    5) Right mouse click on the project->Properties->Build Events->Post-build event command line, add command in the textbox like below:  
        xcopy /Y "$(ProjectDir)*.png" "$(TargetDir)"  
        xcopy /Y "$(TargetDir)*" "$(APPDATA)\$(SolutionName)\Apps\$(TargetName)\"  
    6) Right mouse click on the project->Add->New Item->User Control(WPF), rename to 'MainWindow'.  
    7) Right mouse click on the project->Add->New Item->User Control(WPF), rename to you want(this is your SubApp Window).  
    8) Add SecondDesktop/SecondDesktop/AppDemo/Factory.cs to the project.  
    9) Open Factory.cs, and modify something like below(I believe you are smart enough):  
        -enum AppUID  
        -Factory::AppName  
        -Factory::CreateWindow  
    10) Create a button from MainWindow and add click event, you can create SubApp config file, create SubApp, the parameter is config file.  e.g like below:  
```csharp
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string config = Factory.CreateSubAppConfig();
            SubAppConfigManager manager = new SubAppConfigManager(config);
            manager.SetText(Message.Text);
            Factory.CreateSubApp(AppUID.SubApp, config);
        }
```
*Can refer to SecondDesktop/SecondDesktop/AppDemo.  

### 2.3 Roadmap

