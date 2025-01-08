# TestTask_XARlabs
 This project has been developed following the requirements for a Test task for the Unity developer position at XARLabs.

 ## **Setup - Unity Version:  6000.032f1 or above**

### **Running the Project**

#### **Desktop Demo:**
1. Open the project in Unity Editor.
2. Load the scene **[Main]** from `Assets/Scenes`.  
   - Ensure the scene is added to `File > Build Settings > Scenes in Build`.
3. Press **Play** to run the scene.  
   - Use the toggles in the UI to switch between animations.

#### **AR Demo (iOS):**
**Device Requirements:** iOS device running iOS 13 or later.
1. Switch the platform to iOS:
   - Go to `File > Build Settings > iOS > Switch Platform`.
2. Configure settings:
   - Enable **Development Build** under `Platform Settings`.
   - Ensure ARKit is enabled in `Edit > Project Settings > XR Plug-in Management > iOS > Apple ARKit`.
3. Build and run:
   - Click **Build and Run** to generate an Xcode project.
   - Open the project in Xcode, set up a provisioning profile, and build the app on an ARKit-compatible iOS device.
   - Run the app on the device.

---

### **Project Dependencies**

This project uses the following Unity packages:  
- **AR Foundation 6.0.4**  
- **Apple ARKit XR Plugin 6.0.4**  

The packages are already included and can be checked or installed by:
1. Open Unity Editor and go to `Window > Package Manager`.
2. Search for the package name under the `Unity Registry`.
3. Install the required version.

---



